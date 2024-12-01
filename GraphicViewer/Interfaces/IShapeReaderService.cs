namespace GraphicViewer.Interfaces
{
    /// <summary>
    /// The IShapeReaderService interface.
    /// </summary>
    public interface IShapeReaderService
    {
        /// <summary>
        /// Reads shapes from file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public List<IShapeData> ReadShapesFromFile(string filePath);
    }
}