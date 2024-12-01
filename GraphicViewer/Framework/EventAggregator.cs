using GraphicViewer.Interfaces;

namespace GraphicViewer.Framework
{
    /// <summary>
    /// The event aggregator class.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// Represents the list of subscribers.
        /// </summary>
        private readonly Dictionary<Type, List<Delegate>> subscribers = new ();

        /// <summary>
        /// Provides facility to subscribe the event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="handler">Event handler.</param>
        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);
            if (!subscribers.ContainsKey(eventType))
            {
                subscribers[eventType] = new List<Delegate>();
            }
            subscribers[eventType].Add(handler);
        }

        /// <summary>
        /// Provides facility to publish the event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="eventData">Event data.</param>
        public void Publish<TEvent>(TEvent eventData)
        {
            var eventType = typeof(TEvent);
            if (subscribers.ContainsKey(eventType))
            {
                foreach (var handler in subscribers[eventType])
                {
                    ((Action<TEvent>)handler)(eventData);
                }
            }
        }
    }
}