using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_lab4_post_options
{
    /// <summary>
    /// Interaction logic for PostOptions.xaml
    /// </summary>
    public partial class PostOptions : Window
    {
        public PostOptions()
        {
            InitializeComponent();
            string userID = "1234";
            string postUserId = "1244";

            if (userID != postUserId)
            {
                postOptions.RowDefinitions.Remove(deletePostRow);
            }
        }

        private void addToFavouritesButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void hidePostButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void reportPostButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void deletePostButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}