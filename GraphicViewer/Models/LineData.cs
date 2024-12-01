using GraphicViewer.Enum;
using GraphicViewer.Interfaces;

namespace GraphicViewer.Models
{
    /// <summary>
    /// The line data class.
    /// </summary>
    public class LineData : IShapeData
    {
        /// <summary>
        /// Gets or sets the A.
        /// </summary>
        public string A { get; set; }  

        /// <summary>
        /// Gets or sets the B.
        /// </summary>
        public string B { get; set; }

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