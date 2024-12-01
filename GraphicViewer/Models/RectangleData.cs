using GraphicViewer.Enum;
using GraphicViewer.Interfaces;

namespace GraphicViewer.Models
{
    /// <summary>
    /// The rectangle data class.
    /// </summary>
    public class RectangleData : IShapeData
    {
        /// <summary>
        /// Gets or sets the top left.
        /// </summary>
        public string TopLeft { get; set; }

        /// <summary>
        /// Gets or sets the bottom right.
        /// </summary>
        public string BottomRight { get; set; }

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
        public double Thickness { get; set; } = 1;
    }
}