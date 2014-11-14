AutofacEvents
=============
AutofacEvents is an event pub/sub extension for Autofac.  Primary usage is to publish domain events between classes, where subscribing to the events is as easy as implementing an interface in any interested classes and ensuring that they are registered with Autofac.

##Configuration
Configuration is simple, during your normal autofac configuration, register the ContravariantRegistrationSource, and call RegisterEventing().

```csharp
var builder = new ContainerBuilder();
builder.RegisterSource(new ContravariantRegistrationSource());
builder.RegisterEventing();
```

##Publishing
Publishing is simple and there are two options.  Option one is to call the PublishEvent() extension method off of ILifetimeScope.  For those that would prefer not to have to add a dependency to ILifetimeScope to publish, there's also an IEventPublisher interface that has a simple Publish method.  Personally I prefer to use the IEventPublisher because a dependency on the IEventPublisher makes it clear what your class intends to use the dependency for, while a dependency on ILifetimeScope is less clear.

```csharp
public class SomeMessage
{
  public string Text { get; set; }
}

public class WorkDoer
{
  public WorkDoer(IEventPublisher eventPublisher)
  {
    _EventPublisher = eventPublisher;
  }
  private readonly IEventPublisher _EventPublisher;
  
  public void DoWork()
  {
    //... Do something
    _EventPublisher.Publish(new SomeMessage { Text = "We did something" });
  }
}
```

##Subscribing
Subscribing is done by implementing the IHandleEvent<TEvent> interface.  The interface has a single method,
```csharp
void Handle<TEvent>()
```
that you must implement.  You then simply have to register the subscriber with Autofac, ensuring you use AsImplementedInterfaces() and events will be published accordingly.

```csharp
public class WorkListener : IHandleEvent<SomeMessage>
{
  public void Handle(SomeMessage)
  {
    //React to SomeMessage here
  }
}
```

Configuring the ContravariantRegistrationSource initially means that if we implement IHandleEvent<object>, we would get all events of any type.  You can also have your event types implement interfaces and subscribe to the interfaces.