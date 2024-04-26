using ISSLab.Model;
using ISSLab.Services;
using ISSLab.View;
using ISSLab.ViewModel;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ViewModel
{
    public class PostContentViewModelTests
    {
        private FakeUserService _fakeUserService;
        private PostContentViewModel _postViewModel;

        [SetUp]
        public void SetUp()
        {
            _fakeUserService = new FakeUserService();
            _postViewModel = new PostContentViewModel(new Post(), new User(), Guid.NewGuid(), Guid.NewGuid(), _fakeUserService, new FakeChatFactory());
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
                string.Empty, string.Empty, string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            _postViewModel.Post = fixedPricePost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(_postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForAuctionPost_ReturnsCorrectString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = _postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(_postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForUnknownPostType_ReturnsEmptyString()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            _postViewModel.Post = noTypePost;

            string expectedResult = Constants.EMPTY_STRING;

            Assert.That(_postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Rating_ForFixedPricePost_ReturnsCorrectValue()
        {
            float expectedReviewScore = 30;
            Post fixedPrice = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, [], expectedReviewScore, Guid.NewGuid(), string.Empty, true);
            _postViewModel.Post = fixedPrice;

            Assert.That(_postViewModel.Rating, Is.EqualTo(expectedReviewScore));
        }

        [Test]
        public void Rating_ForAuctionPost_ReturnsCorrectValue()
        {
            float expectedReviewScore = 30;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
               string.Empty, 0, DateTime.Now, string.Empty, [], expectedReviewScore, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            Assert.That(_postViewModel.Rating, Is.EqualTo(expectedReviewScore));
        }

        [Test]
        public void Visible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            Assert.That(_postViewModel.Visible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Visible_SetValue_SetsCorrectValue()
        {
            string expectedResult = "Invisible";
            _postViewModel.Visible = expectedResult;
            Assert.That(_postViewModel.Visible, Is.EqualTo(expectedResult));
        }


        [Test]
        public void Description_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                expectedDescription, string.Empty, string.Empty, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), string.Empty, true);
            _postViewModel.Post = fixedPricePost;

            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, expectedDescription, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForUnknownPostType_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, expectedDescription, string.Empty, string.Empty, string.Empty, true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Description_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                "", string.Empty, string.Empty, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), string.Empty, true);
            _postViewModel.Post = fixedPricePost;

            string expectedDescription = "expected description";
            _postViewModel.Description = expectedDescription;
            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, "", string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            string expectedDescription = "expected description";
            _postViewModel.Description = expectedDescription;
            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForUnknownPostType_SetsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, "", string.Empty, string.Empty, string.Empty, true);
            _postViewModel.Post = noTypePost;

            string expectedDescription = "expected description";
            _postViewModel.Description = expectedDescription;
            Assert.That(_postViewModel.Description, Is.EqualTo(expectedDescription));
        }


        [Test]
        public void Contact_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, expectedContacts, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), string.Empty, true);
            _postViewModel.Post = fixedPricePost;

            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                expectedContacts, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForUnknownPostType_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, expectedContacts, string.Empty, true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }

        [Test]
        public void Contact_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, "", 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), string.Empty, true);
            _postViewModel.Post = fixedPricePost;

            string expectedContacts = "expected contacts";
            _postViewModel.Contact = expectedContacts;
            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty,
                string.Empty, "", 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            string expectedContacts = "expected contacts";
            _postViewModel.Contact = expectedContacts;
            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForUnknownPostType_SetsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, "", string.Empty, string.Empty, string.Empty, true);
            _postViewModel.Post = noTypePost;

            string expectedContacts = "expected contacts";
            _postViewModel.Contact = expectedContacts;
            Assert.That(_postViewModel.Contact, Is.EqualTo(expectedContacts));
        }



        [Test]
        public void Delivery_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedDelivery = "expected delivery";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, expectedDelivery, [], 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            _postViewModel.Post = fixedPricePost;

            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedDelivery = "expected delivery";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, expectedDelivery, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForDonationOrUnknownPostType_ReturnsEmptyString()
        {
            string expectedDelivery = Constants.EMPTY_STRING;
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            _postViewModel.Post = donationPost;

            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));

            expectedDelivery = Constants.EMPTY_STRING;
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }

        [Test]
        public void Delivery_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, "", [], 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            _postViewModel.Post = fixedPricePost;

            string expectedDelivery = "expected delivery";
            _postViewModel.Delivery = expectedDelivery;
            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, "", [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            string expectedDelivery = "expected delivery";
            _postViewModel.Delivery = expectedDelivery;
            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForDonationOrUnknownPost_ReturnsEmptyString()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            _postViewModel.Post = donationPost;

            string expectedDelivery = Constants.EMPTY_STRING;
            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }


        [Test]
        public void DonationButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(_postViewModel.DonationButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DonationButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            _postViewModel.DonationButtonVisible = expectedResult;
            Assert.That(_postViewModel.DonationButtonVisible, Is.EqualTo(expectedResult));
        }


        [Test]
        public void BuyButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(_postViewModel.BuyButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BuyButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            _postViewModel.BuyButtonVisible = expectedResult;
            Assert.That(_postViewModel.BuyButtonVisible, Is.EqualTo(expectedResult));
        }


        [Test]
        public void BidButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(_postViewModel.BidButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            _postViewModel.BidButtonVisible = expectedResult;
            Assert.That(_postViewModel.BidButtonVisible, Is.EqualTo(expectedResult));
        }


        [Test]
        public void BidPriceVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(_postViewModel.BidPriceVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidPriceVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            _postViewModel.BidPriceVisible = expectedResult;
            Assert.That(_postViewModel.BidPriceVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Username_AnyUser_ReturnsCorrectValue()
        {
            string expectedUsername = "expected Username";
            User user = new User(expectedUsername, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.user = user;

            Assert.That(_postViewModel.Username, Is.EqualTo(expectedUsername));
        }

        [Test]
        public void Media_PostOfUnknownType_ReturnsCorrectValue()
        {
            string expectedMedia = "expected Media";
            Post noTypePost = new Post(expectedMedia, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Media, Is.EqualTo(expectedMedia));
        }

        [Test]
        public void Location_PostOfUnknownType_ReturnsCorrectValue()
        {
            string expectedLocation = "expected Location";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), expectedLocation, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Location, Is.EqualTo(expectedLocation));
        }

        [Test]
        public void ProfilePicture_AnyUser_ReturnsCorrectValue()
        {
            string expectedProfilePicture = "expected Profile Picture Path";
            User user = new User(string.Empty, string.Empty, DateOnly.MaxValue, expectedProfilePicture, string.Empty);
            _postViewModel.user = user;

            Assert.That(_postViewModel.ProfilePicture, Is.EqualTo(expectedProfilePicture));
        }

        [Test]
        public void TimePosted_AnyPost_ReturnsCorrectString()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            TimeSpan timePassed = DateTime.Now - noTypePost.CreationDate;
            string expectedResult = Math.Ceiling(timePassed.TotalSeconds).ToString() + PostContentViewModel.SECONDS_AGO;
            string actualResult = _postViewModel.TimePosted;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetPost_AnyPost_PostIsCorrectlyReturned()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.GetPost, Is.EqualTo(noTypePost));
        }


        [Test]
        public void Price_ForFixedPricePost_ReturnsCorrectValue()
        {
            double expectedPrice = 1234;
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, expectedPrice, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            _postViewModel.Post = fixedPricePost;

            string expectedResult = Constants.DOLLAR_SIGN + expectedPrice;

            Assert.That(_postViewModel.Price, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Price_ForAuctionPost_ReturnsCorrectValue()
        {
            double expectedPrice = 1234;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, expectedPrice, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            _postViewModel.Post = auctionPost;

            string expectedResult = Constants.DOLLAR_SIGN + expectedPrice;

            Assert.That(_postViewModel.Price, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Price_ForDonationOrUnknownPostType_ReturnsEmptyString()
        {
            string expectedPrice = Constants.EMPTY_STRING;
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            _postViewModel.Post = donationPost;

            Assert.That(_postViewModel.Price, Is.EqualTo(expectedPrice));

            expectedPrice = Constants.EMPTY_STRING;
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.Price, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void UpdateBidPrice_ForAuctionPostLessThanThirtySeconds_UpdatesCorrectly()
        {
            double currentBidPrice = 1234;
            double currentMinimumBidPrice = 1000;
            DateTime expirationDate = DateTime.Now.AddSeconds(10);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, currentMinimumBidPrice, true);
            _postViewModel.Post = auctionPost;

            _postViewModel.UpdateBidPrice();
            double expectedBidPrice = currentBidPrice + 5;
            double expectedMinimumBidPrice = currentMinimumBidPrice + 5;
            Assert.That(((FixedPricePost)_postViewModel.Post).ExpirationDate, Is.EqualTo(expirationDate.AddSeconds(30)));
            Assert.That(((AuctionPost)_postViewModel.Post).CurrentBidPrice, Is.EqualTo(expectedBidPrice));
            Assert.That(((AuctionPost)_postViewModel.Post).MinimumBidPrice, Is.EqualTo(expectedMinimumBidPrice));
        }
        [Test]
        public void UpdateBidPrice_ForAuctionPostMoreThanThirtySeconds_UpdatesCorrectly()
        {
            double currentBidPrice = 1234;
            double currentMinimumBidPrice = 1000;
            DateTime expirationDate = DateTime.Now.AddSeconds(100);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, currentMinimumBidPrice, true);
            _postViewModel.Post = auctionPost;

            _postViewModel.UpdateBidPrice();
            double expectedBidPrice = currentBidPrice + 5;
            double expectedMinimumBidPrice = currentMinimumBidPrice + 5;
            Assert.That(((FixedPricePost)_postViewModel.Post).ExpirationDate, Is.EqualTo(expirationDate));
            Assert.That(((AuctionPost)_postViewModel.Post).CurrentBidPrice, Is.EqualTo(expectedBidPrice));
            Assert.That(((AuctionPost)_postViewModel.Post).MinimumBidPrice, Is.EqualTo(expectedMinimumBidPrice));
        }
        [Test]
        public void UpdateBidPrice_ForNotAuctionPost_ThrowsException()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            _postViewModel.Post = donationPost;

            var exceptionMessage = Assert.Throws<Exception>(() => { _postViewModel.UpdateBidPrice(); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post is not of type AuctionPost!"));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            exceptionMessage = Assert.Throws<Exception>(() => { _postViewModel.UpdateBidPrice(); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post is not of type AuctionPost!"));
        }

        [Test]
        public void AddPostToFavorites_AnyValues_ShouldCallAddItemToFavorites()
        {
            _postViewModel.AddPostToFavorites();

            Assert.That(_fakeUserService.AddItemToFavoritesCalled, Is.EqualTo(true));
            Assert.That(_fakeUserService.GroupId, Is.EqualTo(_postViewModel._groupId));
            Assert.That(_fakeUserService.PostId, Is.EqualTo(_postViewModel.Post.Id));
            Assert.That(_fakeUserService.AccountId, Is.EqualTo(_postViewModel._accountId));
        }

        [Test]
        public void AddPostToCart_AnyValues_ShouldCallAddItemToCart()
        {
            _postViewModel.AddPostToCart();

            Assert.That(_fakeUserService.AddItemToCartCalled, Is.EqualTo(true));
            Assert.That(_fakeUserService.GroupId, Is.EqualTo(_postViewModel._groupId));
            Assert.That(_fakeUserService.PostId, Is.EqualTo(_postViewModel.Post.Id));
            Assert.That(_fakeUserService.AccountId, Is.EqualTo(_postViewModel._accountId));
        }

        [Test]
        public void BidPrice_ForAuctionPost_ReturnsCorrectValue()
        {
            double currentBidPrice = 1234;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, [], 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, 0, true);
            _postViewModel.Post = auctionPost;


            string expectedResult = Constants.DOLLAR_SIGN + currentBidPrice;
            Assert.That(_postViewModel.BidPrice, Is.EqualTo(expectedResult));
        }
        [Test]
        public void BidPrice_ForNotAuctionPost_ReturnsEmptyString()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            _postViewModel.Post = donationPost;

            string expectedResult = Constants.EMPTY_STRING;
            Assert.That(_postViewModel.BidPrice, Is.EqualTo(expectedResult));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, "", true);
            _postViewModel.Post = noTypePost;

            Assert.That(_postViewModel.BidPrice, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Interests_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            _postViewModel.Post = noTypePost;

            int expectedCount = 1;
            string expectedResult = expectedCount.ToString() + " interested";
            Assert.That(_postViewModel.Interests, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddInterests_NoPreviousInterest_AddsInterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.Post = noTypePost;
            _postViewModel.user = newUser;

            _postViewModel.AddInterests();

            Assert.That(_postViewModel.Post.InterestStatuses.Count, Is.EqualTo(1));
            Assert.That(_postViewModel.Post.InterestStatuses[0].PostId, Is.EqualTo(noTypePost.Id));
            Assert.That(_postViewModel.Post.InterestStatuses[0].InterestedUserId, Is.EqualTo(newUser.Id));
        }

        [Test]
        public void AddInterests_InterestAlreadyExists_RemovesInterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.Post = noTypePost;
            _postViewModel.user = newUser;

            _postViewModel.AddInterests();
            _postViewModel.AddInterests();

            Assert.That(_postViewModel.Post.InterestStatuses.Count, Is.EqualTo(0));

        }

        [Test]
        public void Uninterests_AnyPost_ReturnsUninterestedNumber()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.Post = noTypePost;
            _postViewModel.user = newUser;

            int expectedCount = 0;
            string expectedResult = expectedCount.ToString() + " uninterested";
            Assert.That(_postViewModel.Uninterests, Is.EqualTo(expectedResult));

            _postViewModel.AddUninterests();
            expectedCount = 1;
            expectedResult = expectedCount.ToString() + " uninterested";
            Assert.That(_postViewModel.Uninterests, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddUninterests_NoPreviousUninterest_AddsUninterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.Post = noTypePost;
            _postViewModel.user = newUser;

            _postViewModel.AddUninterests();

            Assert.That(_postViewModel.Post.InterestStatuses.Count, Is.EqualTo(1));
            Assert.That(_postViewModel.Post.InterestStatuses[0].PostId, Is.EqualTo(noTypePost.Id));
            Assert.That(_postViewModel.Post.InterestStatuses[0].InterestedUserId, Is.EqualTo(newUser.Id));
        }

        [Test]
        public void AddUninterests_UninterestAlreadyExists_RemovesUninterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            _postViewModel.Post = noTypePost;
            _postViewModel.user = newUser;

            _postViewModel.AddUninterests();
            _postViewModel.AddUninterests();

            Assert.That(_postViewModel.Post.InterestStatuses.Count, Is.EqualTo(0));
        }

        [Test]
        public void Comments_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            _postViewModel.Post = noTypePost;

            string expectedResult = noTypePost.Comments.Count + " comments";
            Assert.That(_postViewModel.Comments, Is.EqualTo(expectedResult));
        }
        //public void SendBuyingMessage()
        //{
        //    Chat chat = new Chat(new ChatViewModel(user, _post));
        //    chat.SendBuyingMessage(Media);
        //    chat.Show();
        //}

        //public void Donate()
        //{
        //    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        //    {
        //        FileName = ((DonationPost)_post).DonationPageLink,
        //        UseShellExecute = true
        //    });
        //}

        //public void HidePost()
        //{
        //    Visible = "Collapsed";
        //}
        [Test]
        public void HidePost_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            _postViewModel.Post = noTypePost;

            _postViewModel.HidePost();
            Assert.That(_postViewModel.Visible, Is.EqualTo(Constants.COLLAPSED_VISIBILITY));
        }
    }
}
