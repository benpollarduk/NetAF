using System;
using System.Collections.Generic;

namespace NetAF.Events
{
    /// <summary>
    /// Provides an event bus for handling the centralization of events.
    /// </summary>
    public static class EventBus
    {
        #region StaticFields

        private static readonly Dictionary<Type, List<Delegate>> subscribers = new();
        private static readonly object l = new();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Registers a callback to be executed whenever an event of type <typeparamref name="T"/> is published.
        /// </summary>
        /// <typeparam name="T">The specific type of <see cref="Events"/> to listen for.</typeparam>
        /// <param name="handler">The action to execute when the event occurs.</param>
        public static void Subscribe<T>(Action<T> handler) where T : BaseEvent
        {
            lock (l)
            {
                var type = typeof(T);

                if (!subscribers.ContainsKey(type))
                    subscribers[type] = [];

                subscribers[type].Add(handler);
            }
        }

        /// <summary>
        /// Remove a previously registered callback.
        /// </summary>
        /// <typeparam name="T">The specific type of <see cref="Events"/> to remove listening for.</typeparam>
        /// <param name="handler">The action to remove.</param>
        public static void Unsubscribe<T>(Action<T> handler) where T : BaseEvent
        {
            lock (l)
            {
                var type = typeof(T);

                if (subscribers.TryGetValue(type, out var handlers))
                {
                    handlers.Remove(handler);

                    if (handlers.Count == 0)
                        subscribers.Remove(type);
                }
            }
        }

        /// <summary>
        /// Remove all previously registered callbacks.
        /// </summary>
        public static void UnsubscribeAll()
        {
            lock (l)
            {
                subscribers.Clear();
            }
        }

        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <typeparam name="T">The specific type of <see cref="Events"/> to publish.</typeparam>
        /// <param name="eventData">The data to pass when the event is raised.</param>
        public static void Publish<T>(T eventData) where T : BaseEvent
        {
            List<Delegate> handlers;

            lock (l)
            {
                if (!subscribers.TryGetValue(typeof(T), out var h)) 
                    return;
                
                handlers = [.. h];
            }

            foreach (var handler in handlers)
                ((Action<T>)handler).Invoke(eventData);
        }

        #endregion
    }
}
