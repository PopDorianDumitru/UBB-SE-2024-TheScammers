using ISSLab.View;
using ISSLab.ViewModel;
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
        private IMainWindowViewModel _viewModel;

        public MainWindow(IMainWindowViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            this.DataContext = _viewModel;

            LoadMarketplace();
        }

        private void LoadMarketplace()
        {
            marketPlaceButton.Background = Brushes.LightBlue;
        }

        private void onClickedMarketplace(object sender, RoutedEventArgs e)
        {
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.LightBlue;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
            this.CreatePostControl.Visibility = Visibility.Collapsed;
            this.ShowPostsControl.Visibility = Visibility.Visible;

            _viewModel.ChangeToMarketPlace();
        }

        private void onClickedCreateMarketplacePost(object sender, RoutedEventArgs e)
        {
            createMarketplacePostButton.Background = Brushes.LightBlue;
            marketPlaceButton.Background = Brushes.Transparent;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
            this.CreatePostControl.Visibility = Visibility.Visible;
            this.ShowPostsControl.Visibility = Visibility.Collapsed;
        }

        private void onClickedYourMarketplaceCart(object sender, RoutedEventArgs e)
        {
            yourMarketplaceCartButton.Background = Brushes.LightBlue;
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.Transparent;
            favoritesButton.Background = Brushes.Transparent;
            this.CreatePostControl.Visibility = Visibility.Collapsed;
            this.ShowPostsControl.Visibility = Visibility.Visible;

            _viewModel.ChangeToCart();
        }

        private void onClickedFavoritesButton(object sender, RoutedEventArgs e)
        {
            favoritesButton.Background = Brushes.LightBlue;
            yourMarketplaceCartButton.Background = Brushes.Transparent;
            createMarketplacePostButton.Background = Brushes.Transparent;
            marketPlaceButton.Background = Brushes.Transparent;
            this.CreatePostControl.Visibility = Visibility.Collapsed;
            this.ShowPostsControl.Visibility = Visibility.Visible;

            _viewModel.ChangeToFavorites();
        }
    }
}