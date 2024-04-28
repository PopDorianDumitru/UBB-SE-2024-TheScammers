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
    /// Interaction logic for CreationPost.xaml
    /// </summary>
    public partial class CreationPost : UserControl
    {
        private readonly DependencyProperty typeProperty = DependencyProperty.Register("Type", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty phoneNumberProperty = DependencyProperty.Register("PhoneNumber", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty descriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty deliveryProperty = DependencyProperty.Register("Delivery", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty conditionPropery = DependencyProperty.Register("Condition", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty availabilityProperty = DependencyProperty.Register("Availability", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty phoneVisibleProperty = DependencyProperty.Register("PhoneVisible", typeof(bool), typeof(CreationPost));
        private readonly DependencyProperty priceVisibleProperty = DependencyProperty.Register("PriceVisible", typeof(bool), typeof(CreationPost));
        private readonly DependencyProperty conditionVisibleProperty = DependencyProperty.Register("ConditionVisible", typeof(bool), typeof(CreationPost));
        private readonly DependencyProperty availabilityVisibleProperty = DependencyProperty.Register("AvailabilityVisible", typeof(bool), typeof(CreationPost));
        private readonly DependencyProperty deliveryVisibleProperty = DependencyProperty.Register("DeliveryVisible", typeof(bool), typeof(CreationPost));
        private readonly DependencyProperty isDonationProperty = DependencyProperty.Register("IsDonation", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty donationLinkProperty = DependencyProperty.Register("DonationLink", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty isAuctionProperty = DependencyProperty.Register("IsAuction", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty minimumBidProperty = DependencyProperty.Register("MinimumBid", typeof(string), typeof(CreationPost));

        public string MinimumBid
        {
            get { return (string)GetValue(minimumBidProperty); }
            set { SetValue(minimumBidProperty, value); }
        }
        public string IsAuction
        {
            get { return (string)GetValue(isAuctionProperty); }
            set { SetValue(isDonationProperty, value); }
        }

        public string IsDonation
        {
            get { return (string)GetValue(isDonationProperty); }
            set { SetValue(isDonationProperty, value); }
        }

        public string DonationLink
        {
            get { return (string)GetValue(donationLinkProperty); }
            set { SetValue(donationLinkProperty, value); }
        }
        public string PhoneVisible
        {
            get { return (string)GetValue(phoneVisibleProperty); }
            set { SetValue(phoneVisibleProperty, value); }
        }
        public string PriceVisible
        {
            get { return (string)GetValue(priceVisibleProperty); }
            set { SetValue(priceVisibleProperty, value); }
        }
        public string ConditionVisible
        {
            get { return (string)GetValue(conditionVisibleProperty); }
            set { SetValue(conditionVisibleProperty, value); }
        }
        public string AvailabilityVisible
        {
            get { return (string)GetValue(availabilityVisibleProperty); }
            set { SetValue(availabilityVisibleProperty, value); }
        }
        public string DeliveryVisible
        {
            get { return (string)GetValue(deliveryVisibleProperty); }
            set { SetValue(deliveryVisibleProperty, value); }
        }

        public string Type
        {
            get { return (string)GetValue(typeProperty); }
            set { SetValue(typeProperty, value); }
        }
        public string PhoneNumber
        {
            get { return (string)GetValue(phoneNumberProperty); }
            set { SetValue(phoneNumberProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(descriptionProperty); }
            set { SetValue(descriptionProperty, value); }
        }
        public string Price
        {
            get { return (string)GetValue(priceProperty); }
            set { SetValue(priceProperty, value); }
        }
        public string Delivery
        {
            get { return (string)GetValue(deliveryProperty); }
            set { SetValue(deliveryProperty, value); }
        }
        public string Condition
        {
            get { return (string)GetValue(conditionPropery); }
            set { SetValue(conditionPropery, value); }
        }
        public string Availability
        {
            get { return (string)GetValue(availabilityProperty); }
            set { SetValue(availabilityProperty, value); }
        }

        public CreationPost()
        {
            InitializeComponent();
        }

        public void CreationButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ICreatePostViewModel;
            viewModel.CreatePost();
        }
    }
}
