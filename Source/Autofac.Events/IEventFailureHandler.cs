﻿
using System;

namespace Autofac.Events
{
    /// <summary>
    /// Defines behavior for responding to when an event handler fails.
    /// </summary>
    public interface IEventFailureHandler
    {
        /// <summary>
        /// Handles a failed event handler.
        /// </summary>
        /// <param name="scope">The current lifetime scope.</param>
        /// <param name="exception">The exception that occurred during the handling of the event.</param>
        void HandleFailure(ILifetimeScope scope, Exception exception);
    }
}