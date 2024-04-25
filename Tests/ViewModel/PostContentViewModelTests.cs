using ISSLab.Model;
using ISSLab.Services;
using ISSLab.ViewModel;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ViewModel
{
    public class PostContentViewModelTests
    {
        private PostContentViewModel _postViewModel;
        
        [SetUp]
        public void SetUp()
        {
           
            _postViewModel = new PostContentViewModel(new Post(), new User(), Guid.NewGuid(), Guid.NewGuid(), new FakeUserService());
        }

        [Test]
        public void DisplayRemainingTime_ForSeconds_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromSeconds(30);

            string expectedResult = "Available for: 30 seconds";
            string actualResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForMinutes_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromMinutes(30);

            string expectedResult = "Available for: 30 minutes";
            string actualResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForHours_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromHours(3);

            string expectedResult = "Available for: 3 hours";
            string actualResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForDays_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromDays(30);

            string expectedResult = "Available for: 30 days";
            string actualResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForFixedPricePost_ReturnsCorrectString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, 
                string.Empty, string.Empty, string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), "FixedPrice", true);
            _postViewModel.Post = fixedPricePost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(expectedResult, Is.EqualTo(_postViewModel.AvailableFor));
        }

        [Test]
        public void AvailableFor_ForAuctionPost_ReturnsCorrectString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, "Auction", true);
            _postViewModel.Post = auctionPost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(expectedResult, Is.EqualTo(_postViewModel.AvailableFor));
        }

        [Test]
        public void AvailableFor_ForUnknownPostType_ReturnsEmptyString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            
            _postViewModel.Post = noTypePost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = "";

            Assert.That(expectedResult, Is.EqualTo(_postViewModel.AvailableFor));
        }
        //public void AvailableFor_ReturnsCorrectString_ForFixedPricePost()
        //{
        //    // Arrange
        //    DateTime expirationDate = DateTime.Now.AddHours(1);
        //    FixedPricePost post = new FixedPricePost { ExpirationDate = expirationDate };
        //    MyClass myClass = new MyClass(post);

        //    // Act
        //    string result = myClass.AvailableFor;

        //    // Assert
        //    TimeSpan expectedTimeLeft = expirationDate - DateTime.Now;
        //    string expected = DisplayRemainingTime(expectedTimeLeft);
        //    Assert.AreEqual(expected, result);
        //}
    }
}
