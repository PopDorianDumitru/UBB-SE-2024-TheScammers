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
using ISSLab.Model;
using ISSLab.ViewModel;

namespace ISSLab.View
{
    /// <summary>
    /// Interaction logic for PostContent.xaml
    /// </summary>
    public partial class PostContent : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty MediaProperty = DependencyProperty.Register("Media", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty ProfilePictureProperty = DependencyProperty.Register("ProfilePicture", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty TimePostedProperty = DependencyProperty.Register("TimePosted", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty AvailableForProperty = DependencyProperty.Register("AvailableFor", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty InterestsProperty = DependencyProperty.Register("Interests", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty UninterestsProperty = DependencyProperty.Register("Uninterests", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty CommentsProperty = DependencyProperty.Register("Comments", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty ContactProperty = DependencyProperty.Register("Contact", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(string), typeof(PostContent));
        // public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register("Visible", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty DonationButtonVisibleProperty = DependencyProperty.Register("DonationButtonVisible", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty BuyButtonVisibleProperty = DependencyProperty.Register("BuyButtonVisible", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty BidButtonVisibleProperty = DependencyProperty.Register("BidButtonVisible", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty BidPriceVisibleProperty = DependencyProperty.Register("BidPriceVisible", typeof(string), typeof(PostContent));
        public static readonly DependencyProperty BidPriceProperty = DependencyProperty.Register("BidPrice", typeof(string), typeof(PostContent));
        public string DonationButtonVisible
        {
            get { return (string)GetValue(DonationButtonVisibleProperty); }
            set { SetValue(DonationButtonVisibleProperty, value); }
        }

        public string BuyButtonVisible
        {
            get { return (string)GetValue(BuyButtonVisibleProperty); }
            set { SetValue(BuyButtonVisibleProperty, value); }
        }

        public string BidButtonVisible
        {
            get { return (string)GetValue(BidButtonVisibleProperty); }
            set { SetValue(BidButtonVisibleProperty, value); }
        }

        public string BidPriceVisible
        {
            get { return (string)GetValue(BidPriceVisibleProperty); }
            set { SetValue(BidPriceVisibleProperty, value); }
        }

        public string BidPrice
        {
            get { return (string)GetValue(BidPriceProperty); }
            set { SetValue(BidPriceProperty, value); }
        }
        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Rating", typeof(float), typeof(PostContent));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public float Rating
        {
            get { return (float)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }
        public string Media
        {
            get { return (string)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }
        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        public string ProfilePicture
        {
            get { return (string)GetValue(ProfilePictureProperty); }
            set { SetValue(ProfilePictureProperty, value); }
        }
        public string TimePosted
        {
            get { return (string)GetValue(TimePostedProperty); }
            set { SetValue(TimePostedProperty, value); }
        }
        public string AvailableFor
        {
            get { return (string)GetValue(AvailableForProperty); }
            set { SetValue(AvailableForProperty, value); }
        }
        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public string Interests
        {
            get { return (string)GetValue(InterestsProperty); }
            set { SetValue(InterestsProperty, value); }
        }
        public string Uninterests
        {
            get { return (string)GetValue(UninterestsProperty); }
            set { SetValue(UninterestsProperty, value); }
        }
        public string Comments
        {
            get { return (string)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string Contact
        {
            get { return (string)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        public string Delivery
        {
            get { return (string)GetValue(DeliveryProperty); }
            set { SetValue(DeliveryProperty, value); }
        }

        public event EventHandler MoreButtonClicked;
        public event EventHandler OptionsButtonClicked;
        public PostContent()
        {
            InitializeComponent();

            if (this.Rating < 5)
            {
                this.star5.Visibility = Visibility.Collapsed;
            }
            if (this.Rating < 4)
            {
                this.star4.Visibility = Visibility.Collapsed;
            }
            if (this.Rating < 3)
            {
                this.star3.Visibility = Visibility.Collapsed;
            }
            if (this.Rating < 2)
            {
                this.star2.Visibility = Visibility.Collapsed;
            }
        }

        private void OnMoreButtonClick(object sender, RoutedEventArgs e)
        {
            if (GridOptions.IsVisible)
            {
                GridOptions.Visibility = Visibility.Collapsed;
            }

            if (GridDetails.IsVisible)
            {
                GridDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                GridDetails.Visibility = Visibility.Visible;
            }
        }

        private void DisplayOptions(object sender, RoutedEventArgs e)
        {
            if (GridDetails.IsVisible)
            {
                GridDetails.Visibility = Visibility.Collapsed;
            }

            if (GridOptions.IsVisible)
            {
                GridOptions.Visibility = Visibility.Collapsed;
            }
            else
            {
                GridOptions.Visibility = Visibility.Visible;
            }
        }

        private void OnInterestedClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPostContentViewModel;
            viewModel.AddInterests();
        }

        private void OnUninterestedClickedOn(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPostContentViewModel;
            viewModel.AddUninterests();
        }

        private void OnBuyButtonClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPostContentViewModel;
            viewModel.SendBuyingMessage();
        }

        private void OnBidButtonClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPostContentViewModel;
            viewModel.UpdateBidPrice();
        }

        private void OnDonationButtonClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPostContentViewModel;
            viewModel.Donate();
        }
    }
}
