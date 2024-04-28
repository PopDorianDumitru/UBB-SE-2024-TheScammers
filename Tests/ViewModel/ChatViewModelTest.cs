using ISSLab.Model;
using ISSLab.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tests.ViewModel
{
    internal class ChatViewModelTest
    {
        private ChatViewModel _chatViewModel;
        private ObservableCollection<Message>? _otherMessages;
        private User _user;
        private Post _post;

        [SetUp]
        public void SetUp()
        {
            _user = new User();
            _post = new Post();
            _chatViewModel = new ChatViewModel(_user, _post);
        }

        [Test]
        public void GetUser_Any_UserIsEqualToLocalUser()
        {
            Assert.AreEqual(_chatViewModel.User, _user);
        }

        [Test]
        public void SetUser_Any_UserIsEqualToNewUser()
        {
            User newUser = new User();
            _chatViewModel.User = newUser;
            Assert.AreEqual(_chatViewModel.User, newUser);
        }

        [Test]
        public void GetPost_Any_PostIsEqualToLocalPost()
        {
            Assert.AreEqual(_chatViewModel.Post, _post);
        }

        [Test]
        public void SetPost_Any_PostIsEqualToNewPost()
        {
            Post newPost = new Post();
            _chatViewModel.Post = newPost;
            Assert.AreEqual(_chatViewModel.Post, newPost);
        }

        public void AllMessages_GetAllMessages_CollectionIsEmpty()
        {
            Assert.IsEmpty(_chatViewModel.AllMessages);
        }

        [Test]
        public void AllMessages_SetAllMessages_CollectionIsEqualToOtherMessages()
        {
            _chatViewModel.AllMessages = _otherMessages;
            Assert.AreEqual(_chatViewModel.AllMessages, _otherMessages);
        }

        [Test]
        public void AddMessage_AnyMessage_MessageIsAdded()
        {
            Message message = new Message();
            _chatViewModel.AddMessage(message);
            Assert.Contains(message, _chatViewModel.AllMessages);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageIsAdded()
        {
            string imagePath = "";
            _chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(_chatViewModel.AllMessages.Count, Is.EqualTo(1));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageHasTheHardcodedText()
        {
            string imagePath = "";
            _chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(_chatViewModel.AllMessages.Count, Is.EqualTo(1));
            Message addedMessage = _chatViewModel.AllMessages[0];
            Assert.AreEqual(Constants.BUYING_MESSAGE_DEFAULT_CONTENT, addedMessage.Content);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendBuyingMessage_AddingHardcodedSellingMessage_MessageHasTheImagePath()
        {
            string imagePath = "";
            _chatViewModel.SendBuyingMessage(imagePath);
            Assert.That(_chatViewModel.AllMessages.Count, Is.EqualTo(1));
            Message addedMessage = _chatViewModel.AllMessages[0];
            Assert.AreEqual(imagePath, addedMessage.ImagePath);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendMessageForSelling_MessageIsAdded()
        {
            _chatViewModel.SendMessage("message", false, true);
            Assert.That(_chatViewModel.AllMessages.Count, Is.EqualTo(1));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendMessageForSelling_MessageHasCorrectContent()
        {
            _chatViewModel.SendMessage("message", false, true);
            string expectedContent = Constants.SELLING_MESSAGE_DEFAULT_CONTENT;
            Assert.That(_chatViewModel.AllMessages[0].Content, Is.EqualTo(expectedContent));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessage_MessageHasCorrectContent()
        {
            _chatViewModel.SendMessage("message", false, false);
            Assert.That(_chatViewModel.AllMessages[0].Content, Is.EqualTo("message"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineTrue_MessageIsBlue()
        {
            _chatViewModel.SendMessage("message", true, false);
            Assert.That(_chatViewModel.AllMessages[0].BubbleColor, Is.EqualTo(Brushes.LightBlue));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineFalse_MessageIsGray()
        {
            _chatViewModel.SendMessage("message", false, false);
            Assert.That(_chatViewModel.AllMessages[0].BubbleColor, Is.EqualTo(Brushes.LightGray));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineTrue_MessageIsAlignedRight()
        {
            _chatViewModel.SendMessage("message", true, false);
            Assert.That(_chatViewModel.AllMessages[0].HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Right));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SendMessage_SendOtherMessageIsMineFalse_MessageIsAlignedLeft()
        {
            _chatViewModel.SendMessage("message", false, false);
            Assert.That(_chatViewModel.AllMessages[0].HorizontalAlignment, Is.EqualTo(HorizontalAlignment.Left));
        }
    }

}
