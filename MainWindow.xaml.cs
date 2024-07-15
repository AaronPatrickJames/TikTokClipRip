using System.Diagnostics;
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

namespace TikTokClipRip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ActivateFunction(object sender, RoutedEventArgs e)
        {
                var youtube = new YouTube();

                // Replace with your YouTube video URL
                var videoUrl = "https://www.youtube.com/watch?v=916K_fnMQqU"; //This is temp hardcoded for testing

                Debug.WriteLine("Starting Download Task");
                youtube.DownloadVideoAsync(videoUrl);
        }
    }
}