using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using GraphicViewer.Events;
using GraphicViewer.Framework;
using GraphicViewer.Interfaces;
using GraphicViewer.Models;
using GraphicViewer.Enum;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Diagnostics;

namespace GraphicViewer.ViewModels
{
    /// <summary>
    /// The graphic view model.
    /// </summary>
    public class GraphViewModel : ViewModelBase
    {
        /// <summary>
        /// Represents the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Represents the shape reader service.
        /// </summary>
        private readonly IShapeReaderService shapeReaderService;

        /// <summary>
        /// Represents the canvas width.
        /// </summary>
        private double canvasWidth;

        /// <summary>
        /// Represents the canvas height.
        /// </summary>
        private double canvasHeight;

        /// <summary>
        /// Represents the shape data list.
        /// </summary>
        private List<IShapeData> shapeDataList = new List<IShapeData>();

        /// <summary>
        /// Represents the canvas children.
        /// </summary>
        private ObservableCollection<UIElement> canvasChildren = new ObservableCollection<UIElement>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="shapeReaderService">The shape reader service.</param>
        public GraphViewModel(IEventAggregator eventAggregator, IShapeReaderService shapeReaderService)
        {
            this.eventAggregator = eventAggregator;
            this.shapeReaderService = shapeReaderService;
            this.eventAggregator.Subscribe<OpenFileEvent>(HandleOpenFileEvent);

            CanvasWidth = SystemParameters.PrimaryScreenWidth;
            CanvasHeight = SystemParameters.PrimaryScreenHeight;
        }

        /// <summary>
        /// Gets or sets the canvas width.
        /// </summary>
        public double CanvasWidth
        {
            get { return canvasWidth; }
            set { SetProperty(ref canvasWidth, value); }
        }

        /// <summary>
        /// Gets or sets the canvas height.
        /// </summary>
        public double CanvasHeight
        {
            get { return canvasHeight; }
            set { SetProperty(ref canvasHeight, value); }
        }

        /// <summary>
        /// Gets or sets the canvas children.
        /// </summary>
        public ObservableCollection<UIElement> CanvasChildren
        {
            get { return canvasChildren; }
            set { SetProperty(ref canvasChildren, value); }
        }
        

        /// <summary>
        /// Handle open file event.
        /// </summary>
        /// <param name="eventAgrs">The event agrs.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void HandleOpenFileEvent(OpenFileEvent eventAgrs)
        {
            shapeDataList = shapeReaderService.ReadShapesFromFile(eventAgrs.FilePath);

            DrawShapesOnCanvas();
        }

        /// <summary>
        /// Draw the shapes on canvas.
        /// </summary>
        public void DrawShapesOnCanvas()
        {
            try
            {
                CanvasChildren.Clear();

                double scaleX = CanvasWidth / SystemParameters.PrimaryScreenWidth;
                double scaleY = CanvasHeight / SystemParameters.PrimaryScreenHeight;
                double scale = Math.Min(scaleX, scaleY);

                foreach (var shape in shapeDataList)
                {
                    AddShapeToCanvas(shape, scale);
                }

                // Notify that the CanvasChildren has been updated
                OnPropertyChanged(nameof(CanvasChildren));
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shapes: {ex.Message}");
            }
        }

        /// <summary>
        /// Add shape converts to canvas.
        /// </summary>
        /// <param name="shapeData">The shape data.</param>
        /// <param name="scale">The scale.</param>
        private void AddShapeToCanvas(IShapeData shapeData, double scale)
        {
            UIElement? shapeElement = null;

            switch (shapeData.Type)
            {
                case ShapeType.Line:
                    shapeElement = CreateLine(shapeData, scale);
                    break;

                case ShapeType.Circle:
                    shapeElement = CreateCircle(shapeData, scale);
                    break;

                case ShapeType.Triangle:
                    shapeElement = CreateTriangle(shapeData, scale);
                    break;

                case ShapeType.Recatngle:
                    shapeElement = CreateRectangle(shapeData, scale);
                    break;

                default:
                    break;
            }

            if (shapeElement != null)
            {
                CanvasChildren.Add(shapeElement); 
            }
        }

        /// <summary>
        /// Creates the line.
        /// </summary>
        /// <param name="shapeData">The shape data.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>A Line?</returns>
        private Line? CreateLine(IShapeData shapeData, double scale)
        {
            var lineData = shapeData as LineData;
            if (lineData != null)
            {
                try
                {
                    string[] pointA = lineData.A.Split(';');
                    string[] pointB = lineData.B.Split(';');

                    double ax = double.Parse(pointA[0]);
                    double ay = double.Parse(pointA[1]);
                    double bx = double.Parse(pointB[0]);
                    double by = double.Parse(pointB[1]);

                    // Calculate scaled coordinates relative to canvas center
                    double x1 = (CanvasWidth / 2) + ax * scale;
                    double y1 = (CanvasHeight / 2) - ay * scale;
                    double x2 = (CanvasWidth / 2) + bx * scale;
                    double y2 = (CanvasHeight / 2) - by * scale;

                    // Boundary checks
                    x1 = Math.Clamp(x1, 0, CanvasWidth);
                    y1 = Math.Clamp(y1, 0, CanvasHeight);
                    x2 = Math.Clamp(x2, 0, CanvasWidth);
                    y2 = Math.Clamp(y2, 0, CanvasHeight);

                    var line = new Line
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        Stroke = GetColorFromArgb(lineData.Color),
                        StrokeThickness = lineData.Thickness
                    };

                    return line;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while creating line: {ex.Message}");
                }
            }

            return null;
        }

        /// <summary>
        /// Creates the circle.
        /// </summary>
        /// <param name="shapeData">The shape data.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>An Ellipse?</returns>
        private Ellipse? CreateCircle(IShapeData shapeData, double scale)
        {
            var circleData = shapeData as CircleData;
            if (circleData != null)
            {
                try
                {
                    string[] center = circleData.Center.Split(';');
                    double cx = double.Parse(center[0]);
                    double cy = double.Parse(center[1]);

                    double radius = circleData.Radius * scale;
                    double diameter = radius * 2;

                    // Adjust the diameter if it exceeds canvas dimensions
                    if (diameter > CanvasWidth || diameter > CanvasHeight)
                    {
                        diameter = Math.Min(CanvasWidth, CanvasHeight);
                        radius = diameter / 2; 
                    }

                    var circle = new Ellipse
                    {
                        Width = diameter,
                        Height = diameter,
                        Stroke = GetColorFromArgb(circleData.Color),
                        StrokeThickness = circleData.Thickness,
                        Fill = circleData.Filled ? GetColorFromArgb(circleData.Color) : null
                    };

                    // Calculate position and ensure it's within canvas bounds
                    double left = (CanvasWidth / 2) + cx * scale - radius;
                    double top = (CanvasHeight / 2) - cy * scale - radius;

                    left = Math.Clamp(left, 0, CanvasWidth - circle.Width);
                    top = Math.Clamp(top, 0, CanvasHeight - circle.Height);

                    Canvas.SetLeft(circle, left);
                    Canvas.SetTop(circle, top);

                    return circle;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while creating circle: {ex.Message}");
                }
            }

            return null;
        }

        /// <summary>
        /// Creates the triangle.
        /// </summary>
        /// <param name="shapeData">The shape data.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>A Polygon?</returns>
        private Polygon? CreateTriangle(IShapeData shapeData, double scale)
        {
            var triangleData = shapeData as TriangleData;
            if (triangleData != null)
            {
                try
                {
                    string[] pointA = triangleData.A.Split(';');
                    string[] pointB = triangleData.B.Split(';');
                    string[] pointC = triangleData.C.Split(';');

                    double ax = (CanvasWidth / 2) + double.Parse(pointA[0]) * scale;
                    double ay = (CanvasHeight / 2) - double.Parse(pointA[1]) * scale;

                    double bx = (CanvasWidth / 2) + double.Parse(pointB[0]) * scale;
                    double by = (CanvasHeight / 2) - double.Parse(pointB[1]) * scale;

                    double cx = (CanvasWidth / 2) + double.Parse(pointC[0]) * scale;
                    double cy = (CanvasHeight / 2) - double.Parse(pointC[1]) * scale;

                    // Clamp points within canvas boundaries
                    ax = Math.Clamp(ax, 0, CanvasWidth);
                    ay = Math.Clamp(ay, 0, CanvasHeight);

                    bx = Math.Clamp(bx, 0, CanvasWidth);
                    by = Math.Clamp(by, 0, CanvasHeight);

                    cx = Math.Clamp(cx, 0, CanvasWidth);
                    cy = Math.Clamp(cy, 0, CanvasHeight);

                    return new Polygon
                    {
                        Points = new PointCollection
                        {
                            new Point(ax, ay),
                            new Point(bx, by),
                            new Point(cx, cy)
                        },
                        Stroke = GetColorFromArgb(triangleData.Color),
                        StrokeThickness = triangleData.Thickness,
                        Fill = triangleData.Filled ? GetColorFromArgb(triangleData.Color) : null
                    };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while creating triangle: {ex.Message}");
                }
            }

            return null;
        }

        /// <summary>
        /// Creates the rectangle.
        /// </summary>
        /// <param name="shapeData">The shape data.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>A Rectangle?</returns>
        private Rectangle? CreateRectangle(IShapeData shapeData, double scale)
        {
            var rectangleData = shapeData as RectangleData;
            if (rectangleData != null)
            {
                try
                {
                    // Parse rectangle corner points
                    string[] topLeft = rectangleData.TopLeft.Split(';');
                    string[] bottomRight = rectangleData.BottomRight.Split(';');

                    // Calculate scaled coordinates
                    double x1 = (CanvasWidth / 2) + double.Parse(topLeft[0]) * scale;
                    double y1 = (CanvasHeight / 2) - double.Parse(topLeft[1]) * scale;
                    double x2 = (CanvasWidth / 2) + double.Parse(bottomRight[0]) * scale;
                    double y2 = (CanvasHeight / 2) - double.Parse(bottomRight[1]) * scale;

                    // Ensure the rectangle stays within canvas bounds
                    x1 = Math.Clamp(x1, 0, CanvasWidth);
                    y1 = Math.Clamp(y1, 0, CanvasHeight);
                    x2 = Math.Clamp(x2, 0, CanvasWidth);
                    y2 = Math.Clamp(y2, 0, CanvasHeight);

                    // Calculate the rectangle dimensions
                    double rectWidth = Math.Abs(x2 - x1);
                    double rectHeight = Math.Abs(y2 - y1);

                    // Top-left corner of the rectangle
                    double rectLeft = Math.Min(x1, x2);
                    double rectTop = Math.Min(y1, y2);

                    // Create the rectangle as a Rectangle object
                    var rectangle = new Rectangle
                    {
                        Width = rectWidth,
                        Height = rectHeight,
                        Stroke = GetColorFromArgb(rectangleData.Color),
                        StrokeThickness = rectangleData.Thickness,
                        Fill = rectangleData.Filled ? GetColorFromArgb(rectangleData.Color) : null
                    };

                    // Set the rectangle's position
                    Canvas.SetLeft(rectangle, rectLeft);
                    Canvas.SetTop(rectangle, rectTop);

                    return rectangle;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while creating rectangle: {ex.Message}");
                }
            }

            return null;
        }

        /// <summary>
        /// Get color from argb.
        /// </summary>
        /// <param name="colorString">The color string.</param>
        /// <returns>A Brush</returns>
        private Brush GetColorFromArgb(string colorString)
        {
            string[] argb = colorString.Split(';');
            return new SolidColorBrush(Color.FromArgb(
                byte.Parse(argb[0]),
                byte.Parse(argb[1]),
                byte.Parse(argb[2]),
                byte.Parse(argb[3])));
        }
    }
}