namespace GraphicViewer.Events
{
    /// <summary>
    /// The oprn file event.
    /// </summary>
    public class OpenFileEvent
    {
        /// <summary>
        /// Represents the file path.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileEvent"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public OpenFileEvent(string filePath)
        {
            FilePath = filePath;
        }
    }
}