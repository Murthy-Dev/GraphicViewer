using System.Windows.Controls;
using GraphicViewer.ViewModels;

namespace GraphicViewer.Views
{
    /// <summary>
    /// Interaction logic for MenubarView.xaml
    /// </summary>
    public partial class MenubarView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenubarView"/> class.
        /// </summary>
        public MenubarView()
        {
            InitializeComponent();
            DataContext = new MenubarViewModel(App.EventAggregator);
        }
    }
}