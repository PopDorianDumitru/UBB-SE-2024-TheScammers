using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ISSLab.Model;
using ISSLab.ViewModel;
namespace Tests.ViewModel
{
    internal class ChatViewModelTest
    {
        private ChatViewModel chatViewModel;
        private ObservableCollection<Message>? otherMessages;
        private User user;
        private Post post;

        [SetUp]
        public void SetUp()
        {
            user = new User();
            post = new Post();
            chatViewModel = new ChatViewModel(user, post);
        }

        [Test]
        public void GetUser_Any_UserIsEqualToLocalUser()
        {
            Assert.AreEqual(chatViewModel.ChatUser, user);
        }

        [Test]
        public void SetUser_Any_UserIsEqualToNewUser()
        {
            User newUser = new User();
            chatViewModel.ChatUser = newUser;
            Assert.AreEqual(chatViewModel.ChatUser, newUser);
        }

        [Test]
        public void GetPost_Any_PostIsEqualToLocalPost()
        {
            Assert.AreEqual(chatViewModel.RefferedPost, post);
        }

        [Test]
        public void SetPost_Any_PostIsEqualToNewPost()
        {
            Post newPost = new Post();
            chatViewModel.RefferedPost = newPost;
            Assert.AreEqual(chatViewModel.RefferedPost, newPost);
        }

        public void AllMessages_GetAllMessages_CollectionIsEmpty()
        {
            Assert.IsEmpty(chatViewModel.AllMessages);
        }

        [Test]
        public void AllMessages_SetAllMessages_CollectionIsEqualToOtherMessages()
        {
            chatViewModel.AllMessages = otherMessages;
            Assert.AreEqual(chatViewModel.AllMessages, otherMessages);
        }

        [Test]
        public void AddMessage_AnyMessage_MessageIsAdded()
        {
            Message message = new Message();
            chatViewModel.AddMessage(message);
            Assert.Contains(message, chatViewModel.AllMessages);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageIsAdded()
        {
            string imagePath = string.Empty;
            chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(chatViewModel.AllMessages.Count, Is.EqualTo(1));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageHasTheHardcodedText()
        {
            string imagePath = string.Empty;
            chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(chatViewModel.AllMessages.Count, Is.EqualTo(1));
            Message addedMessage = chatViewModel.AllMessages[0];
            Assert.AreEqual(Constants.BUYING_MESSAGE_DEFAULT_CONTENT, addedMessage.Content);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageHasTheImagePath()
        {
            string imagePath = string.Empty;
            chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(chatViewModel.AllMessages.Count, Is.EqualTo(1));
            Message addedMessage = chatViewModel.AllMessages[0];
            Assert.AreEqual(imagePath, addedMessage.ImagePath);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendMessageForSelling_MessageIsAdded()
        {
            chatViewModel.SendMessage("message", false, true);
            Assert.That(chatViewModel.AllMessages.Count, Is.EqualTo(1));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendMessageForSelling_MessageHasCorrectContent()
        {
            chatViewModel.SendMessage("message", false, true);
            string expectedContent = Constants.SELLING_MESSAGE_DEFAULT_CONTENT;
            Assert.That(chatViewModel.AllMessages[0].Content, Is.EqualTo(expectedContent));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessage_MessageHasCorrectContent()
        {
            chatViewModel.SendMessage("message", false, false);
            Assert.That(chatViewModel.AllMessages[0].Content, Is.EqualTo("message"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineTrue_MessageIsBlue()
        {
            chatViewModel.SendMessage("message", true, false);
            Assert.That(chatViewModel.AllMessages[0].BubbleColor, Is.EqualTo(Brushes.LightBlue));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineFalse_MessageIsGray()
        {
            chatViewModel.SendMessage("message", false, false);
            Assert.That(chatViewModel.AllMessages[0].BubbleColor, Is.EqualTo(Brushes.LightGray));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineTrue_MessageIsAlignedRight()
        {
            chatViewModel.SendMessage("message", true, false);
            Assert.That(chatViewModel.AllMessages[0].HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Right));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineFalse_MessageIsAlignedLeft()
        {
            chatViewModel.SendMessage("message", false, false);
            Assert.That(chatViewModel.AllMessages[0].HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Left));
        }
    }
}
