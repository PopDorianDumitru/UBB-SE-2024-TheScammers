using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ISSLab.Model;
using System.Windows.Controls;

namespace ISSLab.ViewModel
{
    public class ChatViewModel : IChatViewModel
    {
        public ObservableCollection<Message> AllMessages { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public ChatViewModel(User user, Post post)
        {
            AllMessages = new ObservableCollection<Message>();
            this.User = user;
            this.Post = post;
        }

        public void AddMessage(Message message)
        {
            AllMessages.Add(message);
        }

        public void SendMessage(string message, bool isMine, bool isForSellingPost)
        {
            var newMessage = new Message
            {
                Content = message,
                Width = CalculateMessageWidth(message),
                IsMine = isMine,
                BubbleColor = isMine ? Brushes.LightBlue : Brushes.LightGray,
                HorizontalAlignment = isMine ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            if (isForSellingPost)
            {
                newMessage.Content = Constants.SELLING_MESSAGE_DEFAULT_CONTENT;
                newMessage.BubbleColor = Brushes.YellowGreen;
            }

            AllMessages.Add(newMessage);
        }

        public void SendBuyingMessage(string imagePath)
        {
            var newMessage = new Message
            {
                Content = Constants.BUYING_MESSAGE_DEFAULT_CONTENT,
                Width = CalculateMessageWidth(Constants.BUYING_MESSAGE_DEFAULT_CONTENT),
                IsMine = false,
                BubbleColor = Brushes.YellowGreen,
                HorizontalAlignment = HorizontalAlignment.Left,
                ImagePath = imagePath,
                AcceptButtonIsVisible = true,
                RejectButtonIsVisible = true
            };
            AllMessages.Add(newMessage);
        }

        private double CalculateMessageWidth(string message)
        {
            var textBlock = new TextBlock { Text = message, TextWrapping = TextWrapping.Wrap };
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return textBlock.DesiredSize.Width;
        }
    }
}
