using GraphicViewer.Enum;
using GraphicViewer.Interfaces;

namespace GraphicViewer.Models
{
    /// <summary>
    /// The circle data class.
    /// </summary>
    public class CircleData : IShapeData
    {
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public double Radius { get; set; }

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