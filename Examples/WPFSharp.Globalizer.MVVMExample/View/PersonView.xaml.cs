using System.Windows;
using System.Windows.Controls;

namespace WPFSharp.Globalizer.MVVMExample.View
{
    /// <summary>
    /// Interaction logic for PersonView.xaml
    /// </summary>
    public partial class PersonView : UserControl
    {
        public PersonView()
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
