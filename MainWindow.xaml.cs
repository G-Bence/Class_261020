using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Class_261020
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StreamingPlatform platform = new StreamingPlatform();
        private string currentSearch = "";
        public MainWindow()
        {
            InitializeComponent();
            platform.loadFromFile((System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Movies.txt")));
            createList();
        }

        private void createList()
        {
            List<IContent> filteredContents = platform.filterContents(currentSearch);
            contentListView.ItemsSource = filteredContents;
            contentCountText.Text = filteredContents.Count + " of " + platform.Contents.Count + " movies shown";
            selectedContentText.Text = "Select a movie";
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            currentSearch = searchTextBox.Text.Trim();
            createList();
        }

        private void showAllButton_Click(object sender, RoutedEventArgs e)
        {
            currentSearch = "";
            searchTextBox.Text = "";
            createList();
        }

        private void longestButton_Click(object sender, RoutedEventArgs e)
        {
            IContent? longest = platform.getLongestContent();
            if (longest == null)
            {
                MessageBox.Show("No content matches the current filter.",
                    "No results", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            contentListView.SelectedItem = longest;
            contentListView.ScrollIntoView(longest);
        }

        private void contentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentListView.SelectedItem is IContent selectedContent)
            {
                selectedContentText.Text = selectedContent.getTitle() + " - " + selectedContent.getLength() + " min";
                if (selectedContent is Series series)
                {
                    selectedContentText.Text += " (" + series.Episodes.Count + " episodes)";
                }
            }
            else
            {
                selectedContentText.Text = "Select a title.";
            }
        }

    }
}