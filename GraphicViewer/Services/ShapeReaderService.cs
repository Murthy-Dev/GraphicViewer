using System.IO;
using System.Windows;
using System.Xml.Linq;
using GraphicViewer.Enum;
using GraphicViewer.Interfaces;
using GraphicViewer.Models;
using Newtonsoft.Json;

namespace GraphicViewer.Services
{
    /// <summary>
    /// The shape reader service.
    /// </summary>
    public class ShapeReaderService : IShapeReaderService
    {
        /// <summary>
        /// Reads shapes from file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns><![CDATA[List<VectorShape>]]></returns>
        public List<IShapeData> ReadShapesFromFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            var shapes = extension switch
            {
                ".json" => ReadFromJsonFile(filePath),
                ".xml" => ReadFromXmlFile(filePath),
                _ => throw new NotSupportedException("Unsupported file format: " + extension)
            };

            return shapes;
        }

        /// <summary>
        /// Reads from json file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><![CDATA[List<VectorShape>]]></returns>
        private List<IShapeData> ReadFromJsonFile(string filePath)
        {
            var vectorShapes = new List<IShapeData>();

            try
            {
                var json = File.ReadAllText(filePath);
                var shapes = JsonConvert.DeserializeObject<List<dynamic>>(json);

                foreach (var shape in shapes)
                {
                    IShapeData vectorShape = null;

                    switch ((string)shape.type)
                    {
                        case "line":
                            vectorShape = new LineData
                            {
                                Type = ShapeType.Line,
                                A = shape.a,
                                B = shape.b,
                                Color = shape.color,
                                Filled = shape.filled != null ? (bool)shape.filled : false
                            };
                            break;

                        case "circle":
                            vectorShape = new CircleData
                            {
                                Type = ShapeType.Circle,
                                Center = shape.center,
                                Radius = shape.radius,
                                Color = shape.color,
                                Filled = shape.filled != null ? (bool)shape.filled : false
                            };
                            break;

                        case "triangle":
                            vectorShape = new TriangleData
                            {
                                Type = ShapeType.Triangle,
                                A = shape.a,
                                B = shape.b,
                                C = shape.c,
                                Color = shape.color,
                                Filled = shape.filled != null ? (bool)shape.filled : false
                            };
                            break;

                        case "rectangle": 
                            vectorShape = new RectangleData
                            {
                                Type = ShapeType.Recatngle,
                                TopLeft = shape.topleft,
                                BottomRight = shape.bottomright,
                                Color = shape.color,
                                Filled = shape.filled != null ? (bool)shape.filled : false
                            };
                            break;
                    }

                    if (vectorShape != null)
                    {
                        vectorShapes.Add(vectorShape);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return vectorShapes;
        }

        /// <summary>
        /// Reads from xml file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><![CDATA[List<VectorShape>]]></returns>
        private List<IShapeData> ReadFromXmlFile(string filePath)
        {
            var shapesList = new List<IShapeData>();
            try
            {
                var xmlDoc = XDocument.Load(filePath);

                foreach (var shapeElement in xmlDoc.Descendants("Shape"))
                {
                    IShapeData vectorShape = null;
                    var shapeType = shapeElement.Attribute("type")?.Value;

                    switch (shapeType)
                    {
                        case "line":
                            vectorShape = new LineData
                            {
                                Type = ShapeType.Line,
                                A = shapeElement.Element("A")?.Value,
                                B = shapeElement.Element("B")?.Value,
                                Color = shapeElement.Element("Color")?.Value,
                                Filled = bool.TryParse(shapeElement.Element("Filled")?.Value, out var filled) ? filled : false
                            };
                            break;

                        case "circle":
                            vectorShape = new CircleData
                            {
                                Type = ShapeType.Circle,
                                Center = shapeElement.Element("Center")?.Value,
                                Radius = double.TryParse(shapeElement.Element("Radius")?.Value, out var radius) ? radius : 0,
                                Color = shapeElement.Element("Color")?.Value,
                                Filled = bool.TryParse(shapeElement.Element("Filled")?.Value, out filled) ? filled : false
                            };
                            break;

                        case "triangle":
                            vectorShape = new TriangleData
                            {
                                Type = ShapeType.Triangle,
                                A = shapeElement.Element("A")?.Value,
                                B = shapeElement.Element("B")?.Value,
                                C = shapeElement.Element("C")?.Value,
                                Color = shapeElement.Element("Color")?.Value,
                                Filled = bool.TryParse(shapeElement.Element("Filled")?.Value, out filled) ? filled : false
                            };
                            break;

                        case "rectangle":
                            vectorShape = new RectangleData
                            {
                                Type = ShapeType.Recatngle,
                                TopLeft = shapeElement.Element("TopLeft")?.Value,
                                BottomRight = shapeElement.Element("BottomRight")?.Value,
                                Color = shapeElement.Element("Color")?.Value,
                                Filled = bool.TryParse(shapeElement.Element("Filled")?.Value, out filled) ? filled : false
                            };
                            break;
                    }

                    if (vectorShape != null)
                    {
                        shapesList.Add(vectorShape);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return shapesList;
        }
    }
}