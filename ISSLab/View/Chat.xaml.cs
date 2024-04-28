using ISSLab.Model;
using ISSLab.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISSLab.View
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window, IChat
    {
        private IChatViewModel _viewModel;

        internal Chat(IChatViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Username.Content = viewModel.User.Username.ToString();
            this._viewModel = viewModel;
        }


        public void SendMessage(string message, bool isMine, bool isSellingPost)
        {
            _viewModel.SendMessage(message, isMine, isSellingPost);
            MessageTextBox.Text = "";
        }

        public void SendBuyingMessage(string media)
        {
            _viewModel.SendBuyingMessage(media);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MessageTextBox.Text, true, false);
        }

        private Button FindVisualChild<Button>(DependencyObject parent, string name) where Button : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is Button && (child as FrameworkElement).Name == name)
                {
                    return child as Button;
                }
                else
                {
                    Button result = FindVisualChild<Button>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Popup popup = new Popup();
            TextBlock popupContent = new TextBlock();
            popupContent.Text = "This is a Popup!";
            popup.Child = popupContent;
            popup.IsOpen = true;
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
        private void OnAcceptButtonClicked(object sender, RoutedEventArgs e)
        {
            Button acceptButton = FindVisualChild<Button>((sender as Button).Parent as Grid, "AcceptButton");
            Button rejectButton = FindVisualChild<Button>((sender as Button).Parent as Grid, "RejectButton");
            Button sendButton = FindVisualChild<Button>((sender as Button).Parent as Grid, "SendButton");


            if (acceptButton != null && rejectButton != null)
            {
                MessageBox.Show("Buying Accepted", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                acceptButton.Visibility = Visibility.Collapsed;
                rejectButton.Visibility = Visibility.Collapsed;
                MessageTextBox.Visibility = Visibility.Collapsed;
                SendButton.Visibility = Visibility.Collapsed;
            }
        }

        private void OnRejectButtonClicked(object sender, RoutedEventArgs e)
        {
            Button acceptButton = FindVisualChild<Button>((sender as Button).Parent as Grid, "AcceptButton");
            Button rejectButton = FindVisualChild<Button>((sender as Button).Parent as Grid, "RejectButton");

            if (acceptButton != null && rejectButton != null)
            {
                MessageBox.Show("Buying Rejected", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                acceptButton.Visibility = Visibility.Collapsed;
                rejectButton.Visibility = Visibility.Collapsed;
                MessageTextBox.Visibility = Visibility.Collapsed;
                SendButton.Visibility = Visibility.Collapsed;

            }
        }
    }
}
