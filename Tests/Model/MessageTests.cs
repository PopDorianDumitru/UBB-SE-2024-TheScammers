using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tests.Model
{
    internal class MessageTests
    {
        public Message messageToTest;

        [SetUp]
        public void SetUp()
        {
            messageToTest = new Message();
        }

        [Test]
        public void ContentGet_ContentOfNewMessage_ContentShouldBeNull()
        {
            Assert.That(messageToTest.Content, Is.Null);
        }
        [Test]
        public void ContentSet_ChangeContentToOtherString_ContentShouldBeOtherString()
        {
            string otherString = "message";
            messageToTest.Content = otherString;
            Assert.That(messageToTest.Content, Is.EqualTo(otherString));
        }

        [Test]
        public void WidthGet_WidthOfNewMessage_WidthShouldBeZero()
        {
            Assert.That(messageToTest.Width, Is.Zero);
        }

        [Test]
        public void WidthSet_ChangeWidthToOtherWidth_WidthShouldBeOtherWidth()
        {
            double otherWidth = 100.0;
            messageToTest.Width = otherWidth;
            Assert.That(messageToTest.Width, Is.EqualTo(otherWidth));
        }

        [Test]
        public void IsMineGet_IsMineValueOfNewMessage_IsMineShouldBeFalse()
        {
            Assert.That(messageToTest.IsMine, Is.False);
        }

        [Test]
        public void IsMineSet_ChangeIsMineValueToTrue_IsMineShouldBeTrue()
        {
            messageToTest.IsMine = true;
            Assert.That(messageToTest.IsMine, Is.True);
        }

        [Test]
        public void IsMineSet_ChangeIsMineValueToFalse_IsMineShouldBeFalse()
        {
            messageToTest.IsMine = false;
            Assert.That(messageToTest.IsMine, Is.False);
        }

        [Test]
        public void BubbleColorGet_BubbleColorOfNewMessage_BubbleColorShouldBeNull()
        {
            Assert.That(messageToTest.BubbleColor, Is.Null);
        }

        [Test]
        public void BubbleColorSet_ChangeBubbleColorToOtherColor_BubbleColorShouldBeOtherColor()
        {
            SolidColorBrush otherColor = new SolidColorBrush();
            otherColor.Color = Colors.AliceBlue;
            messageToTest.BubbleColor = otherColor;
            Assert.That(messageToTest.BubbleColor.Color, Is.EqualTo(Colors.AliceBlue));
        }

        [Test]
        public void HorizontalAlignmentGet_HorizontalAlignmentOfNewMessage_HorizontalAlignmentShouldBeLeft()
        {
            Assert.That(messageToTest.HorizontalAlignment, Is.EqualTo(System.Windows.HorizontalAlignment.Left));
        }

        [Test]
        public void HorizontalAlignmentSet_ChangeHorizontalAlignmentToOtherAlignment_HorizontalAlignmentShouldBeEqualToOtherAlignment()
        {
            HorizontalAlignment otherAlignment = HorizontalAlignment.Center;
            messageToTest.HorizontalAlignment = otherAlignment;
            Assert.That(messageToTest.HorizontalAlignment, Is.EqualTo(otherAlignment));
        }

        [Test]
        public void ImagePathGet_ImagePathOfNewMessage_ImagePathShouldBeNull()
        {
            Assert.That(messageToTest.ImagePath, Is.Null);
        }
        [Test]
        public void ImagePathSet_ChangeImagePathToOtherPath_ImagePathShouldBeOtherPath()
        {
            string otherPath = "path";
            messageToTest.ImagePath = otherPath;
            Assert.That(messageToTest.ImagePath, Is.EqualTo(otherPath));
        }

        [Test]
        public void AcceptButtonIsVisibleGet_AcceptButtonIsVisibleOfNewMessage_AcceptButtonIsVisibleShouldBeFalse()
        {
            Assert.That(messageToTest.AcceptButtonIsVisible, Is.False);
        }

        [Test]
        public void AcceptButtonIsVisibleSet_ChangeAcceptButtonIsVisibleToTrue_AcceptButtonIsVisibleShouldBeTrue()
        {
            messageToTest.AcceptButtonIsVisible = true;
            Assert.That(messageToTest.AcceptButtonIsVisible, Is.True);
        }

        [Test]
        public void AcceptButtonIsVisibleSet_ChangeAcceptButtonIsVisibleToFalse_AcceptButtonIsVisibleShouldBeFalse()
        {
            messageToTest.AcceptButtonIsVisible = false;
            Assert.That(messageToTest.AcceptButtonIsVisible, Is.False);
        }




        [Test]
        public void RejectButtonIsVisibleGet_RejectButtonIsVisibleOfNewMessage_RejectButtonIsVisibleShouldBeFalse()
        {
            Assert.That(messageToTest.RejectButtonIsVisible, Is.False);
        }

        [Test]
        public void RejectButtonIsVisibleSet_ChangeRejectButtonIsVisibleToTrue_RejectButtonIsVisibleShouldBeTrue()
        {
            messageToTest.RejectButtonIsVisible = true;
            Assert.That(messageToTest.RejectButtonIsVisible, Is.True);
        }

        [Test]
        public void RejectButtonIsVisibleSet_ChangeRejectButtonIsVisibleToFalse_RejectButtonIsVisibleShouldBeFalse()
        {
            messageToTest.RejectButtonIsVisible = false;
            Assert.That(messageToTest.RejectButtonIsVisible, Is.False);
        }

        [Test]
        public void AcceptButtonClickedGet_AcceptButtonClickedOfNewMessage_AcceptButtonClickedShouldBeNull()
        {
            Assert.That(messageToTest.AcceptButtonClicked, Is.Null);
        }

        [Test]
        public void AcceptButtonClickedSet_ChangeAcceptButtonClickedToOtherRoutedEventHandler_AcceptButtonClickedShouldBeOtherRoutedEventHandler()
        {
            RoutedEventHandler otherRoutedEventHandler = null;
            messageToTest.AcceptButtonClicked = otherRoutedEventHandler;
            Assert.That(messageToTest.AcceptButtonClicked, Is.EqualTo(otherRoutedEventHandler));
        }

        [Test]
        public void RejectButtonClickedGet_RejectButtonClickedOfNewMessage_RejectButtonClickedShouldBeNull()
        {
            Assert.That(messageToTest.RejectButtonClicked, Is.Null);
        }

        [Test]
        public void RejectButtonClickedSet_ChangeRejectButtonClickedToOtherRoutedEventHandler_RejectButtonClickedShouldBeOtherRoutedEventHandler()
        {
            RoutedEventHandler otherRoutedEventHandler = null;
            messageToTest.RejectButtonClicked = otherRoutedEventHandler;
            Assert.That(messageToTest.RejectButtonClicked, Is.EqualTo(otherRoutedEventHandler));
        }
    }

}
