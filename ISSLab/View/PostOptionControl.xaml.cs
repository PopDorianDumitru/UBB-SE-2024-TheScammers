using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ISSLab.ViewModel;

namespace ISSLab.View
{
    /// <summary>
    /// Interaction logic for PostOptionControl.xaml
    /// </summary>
    public partial class PostOptionControl : UserControl
    {
        public PostOptionControl()
        {
            InitializeComponent();
            string userID = "1234";
            string postUserId = "1244";
            if (userID != postUserId)
            {
                postOptions.RowDefinitions.Remove(deletePostRow);
            }
        }

        private void AddToFavouritesButton_Click(object sender, RoutedEventArgs e)
        {
            // We can have for each user a list of favourites and add the post to that list
            var viewModel = this.DataContext as IPostContentViewModel;
            viewModel.AddPostToFavorites();
        }

        private void HidePostButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as IPostContentViewModel;
            viewModel.HidePost();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as IPostContentViewModel;
            viewModel.AddPostToCart();
        }

        private void ReportPostButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DeletePostButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
