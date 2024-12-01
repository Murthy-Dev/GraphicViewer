using System.Windows;
using System.Windows.Controls;
using GraphicViewer.ViewModels;

namespace GraphicViewer.Views
{
    /// <summary>
    /// Interaction logic for GraphicView.xaml
    /// </summary>
    public partial class GraphicView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private GraphViewModel graphViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicView"/> class.
        /// </summary>
        public GraphicView()
        {
            InitializeComponent();
            graphViewModel = new GraphViewModel(App.EventAggregator, App.ShapeReaderService);
            DataContext = graphViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void GraphicView_SizeChanged(object sender, SizeChangedEventArgs eventArgs)
        {
            // Update ViewModel's canvas size when the canvas size changes
            graphViewModel.CanvasWidth = drawingCanvas.ActualWidth;
            graphViewModel.CanvasHeight = drawingCanvas.ActualHeight;

            // Load shapes after the size change
            graphViewModel.DrawShapesOnCanvas();
        }
    }
}