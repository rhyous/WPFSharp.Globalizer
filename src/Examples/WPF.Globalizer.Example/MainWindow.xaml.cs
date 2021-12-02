using System.Windows;

namespace WPFSharp.Globalizer.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = LastNameTextBox.Text = AgeTextBox.Text = string.Empty;
        }

    }
}
