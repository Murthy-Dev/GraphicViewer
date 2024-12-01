using System.Windows;
using GraphicViewer.ViewModels;

namespace GraphicViewer.Views
{
    /// <summary>
    /// The main view.
    /// </summary>
    public partial class MainView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel(App.EventAggregator);
        }
    }
}