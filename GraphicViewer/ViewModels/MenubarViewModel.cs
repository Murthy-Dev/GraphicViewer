using System.IO;
using System.Windows;
using GraphicViewer.Events;
using GraphicViewer.Framework;
using GraphicViewer.Interfaces;
using Microsoft.Win32;

namespace GraphicViewer.ViewModels
{
    /// <summary>
    /// The menubar view model.
    /// </summary>
    public class MenubarViewModel : ViewModelBase
    {
        /// <summary>
        /// Represents the event aggregator.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenubarViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public MenubarViewModel(IEventAggregator eventAggregator) 
        {
            this.eventAggregator = eventAggregator;
            this.OpenCommand = new RelayCommand(HandleOpenCommand);
        }

        /// <summary>
        /// Gets the open command.
        /// </summary>
        public RelayCommand OpenCommand {  get; }

        /// <summary>
        /// Handle open command.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void HandleOpenCommand(object obj)
        {
            try
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Filter = "Json files (*.json)|*.json|Xml files (*.xml)|*.xml",
                    Title = "Open File"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    eventAggregator.Publish(new OpenFileEvent(openFileDialog.FileName));
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have permission to open this file.");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file was not found. Please select a valid file.");
            }
            catch (IOException ex)
            {
                MessageBox.Show($"An error occurred while accessing the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}