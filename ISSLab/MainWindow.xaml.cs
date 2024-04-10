using ISSLab.View;
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


namespace ISSLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            LoadMarketplace();
        }

        private void LoadMarketplace()
        {
            marketPlaceButton.Background = Brushes.LightBlue;
            LoadPosts();
        }

        private void onClickedMarketplace(object sender, RoutedEventArgs e)
        {
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.LightBlue;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
        }

        private void onClickedCreateMarketplacePost(object sender, RoutedEventArgs e)
        {
            createMarketplacePostButton.Background = Brushes.LightBlue;
            marketPlaceButton.Background = Brushes.Transparent;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
        }

        private void onClickedYourMarketplaceCart(object sender, RoutedEventArgs e)
        {
            yourMarketplaceCartButton.Background = Brushes.LightBlue;
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
        }

        private void onClickedFavoritesButton(object sender, RoutedEventArgs e)
        {
            favoritesButton.Background = Brushes.LightBlue;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.Transparent;
        }

        private void LoadPosts()
        {
            int numberOfPostsToAdd = 5;

            for (int i = 0; i < numberOfPostsToAdd; i++)
            {
                PostContent postContent = new PostContent();
                postContent.MoreButtonClicked += (sender, e) => ShowPostDetails(postContent);
                PostsGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid.SetRow(postContent, i);
                PostsGrid.Children.Add(postContent);
            }
        }

        private void ShowPostDetails(PostContent postContent)
        {
            int index = PostsGrid.Children.IndexOf(postContent);
            PostDetails postDetails = new PostDetails();
            Grid.SetRow(postDetails, index);
            Grid.SetColumn(postDetails, 1);
            PostsGrid.Children.Add(postDetails);
        }

    }
}