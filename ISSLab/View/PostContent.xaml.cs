using ISSLab.Model;
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
    /// Interaction logic for PostContent.xaml
    /// </summary>
    public partial class PostContent : UserControl
    {


        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(PostContent));

        public String Title
        {
            get { return (String)GetValue(TitleProperty); } 
            set { SetValue(TitleProperty, value);}
        }

        public event EventHandler MoreButtonClicked;
        public PostContent()

        {
            InitializeComponent();
        }

        private void OnMoreButtonClick(object sender, RoutedEventArgs e)
        {
            if (GridDetails.Children.Count > 0 && GridDetails.Children[0] is PostDetails)
            {
                GridDetails.Children.RemoveAt(0);
            }
            else
            {
                GridDetails.Children.Add(new PostDetails());
            }
        }

    }
}
