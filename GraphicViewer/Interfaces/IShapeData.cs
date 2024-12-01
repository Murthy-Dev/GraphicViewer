using GraphicViewer.Enum;

namespace GraphicViewer.Interfaces
{
    /// <summary>
    /// The IShapeData interface.
    /// </summary>
    public interface IShapeData
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ShapeType Type { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether filled.
        /// </summary>
        public bool Filled { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        public double Thickness { get; set; }
    }
}