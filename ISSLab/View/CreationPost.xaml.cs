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

namespace ISSLab.View
{
    /// <summary>
    /// Interaction logic for CreationPost.xaml
    /// </summary>
    public partial class CreationPost : UserControl
    {
        private readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty PhoneNumberProperty = DependencyProperty.Register("PhoneNumber", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty ConditionPropery = DependencyProperty.Register("Condition", typeof(string), typeof(CreationPost));
        private readonly DependencyProperty AvailabilityProperty = DependencyProperty.Register("Availability", typeof(string), typeof(CreationPost));

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public string PhoneNumber
        {
            get { return (string)GetValue(PhoneNumberProperty); }
            set { SetValue(PhoneNumberProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public string Delivery
        {
            get { return (string)GetValue(DeliveryProperty); }
            set { SetValue(DeliveryProperty, value); }
        }
        public string Condition
        {
            get { return (string)GetValue(ConditionPropery); }
            set { SetValue(ConditionPropery, value); }
        }
        public string Availability
        {
            get { return (string)GetValue(AvailabilityProperty); }
            set { SetValue(AvailabilityProperty, value); }
        }

        public CreationPost()
        {
            InitializeComponent();
        }

        public void CreationButtonClick(Object sender, RoutedEventArgs e)
        {

        }
    }
}
