namespace GraphicViewer.Interfaces
{
    /// <summary>
    /// The event aggregator interface.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Provides facility to subscribe the event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="handler">Event handler.</param>
        void Subscribe<TEvent>(Action<TEvent> handler);

        /// <summary>
        /// Provides facility to publish the event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="eventData">Event data.</param>
        void Publish<TEvent>(TEvent eventData);
    }
}