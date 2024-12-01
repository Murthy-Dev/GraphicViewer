using System.Windows;
using GraphicViewer.Framework;
using GraphicViewer.Interfaces;
using GraphicViewer.Services;
using GraphicViewer.Views;

namespace GraphicViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        public static IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Gets or sets the shape reader service.
        /// </summary>
        public static IShapeReaderService ShapeReaderService {  get; set; }

        /// <summary>
        /// On startup.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            EventAggregator = new EventAggregator();

            ShapeReaderService = new ShapeReaderService();

            var mainView = new MainView();
            mainView.Show(); 
        }
    }
}