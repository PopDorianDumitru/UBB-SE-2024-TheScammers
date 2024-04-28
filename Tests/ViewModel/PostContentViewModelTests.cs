using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using ISSLab.Model;
using ISSLab.Services;
using ISSLab.View;
using ISSLab.ViewModel;

namespace Tests.ViewModel
{
    public class PostContentViewModelTests
    {
        private FakeUserService fakeUserService;
        private PostContentViewModel postViewModel;

        [SetUp]
        public void SetUp()
        {
            fakeUserService = new FakeUserService();
            postViewModel = new PostContentViewModel(new Post(), new User(), Guid.NewGuid(), Guid.NewGuid(), fakeUserService, new FakeChatFactory());
        }

        [Test]
        public void DisplayRemainingTime_ForSeconds_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromSeconds(30);

            string expectedResult = "Available for: 30 seconds";
            string actualResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForMinutes_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromMinutes(30);

            string expectedResult = "Available for: 30 minutes";
            string actualResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForHours_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromHours(3);

            string expectedResult = "Available for: 3 hours";
            string actualResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DisplayRemainingTime_ForDays_ReturnsCorrectString()
        {
            TimeSpan timeLeft = TimeSpan.FromDays(30);

            string expectedResult = "Available for: 30 days";
            string actualResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForFixedPricePost_ReturnsCorrectString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, expirationDate, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            postViewModel.Post = fixedPricePost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForAuctionPost_ReturnsCorrectString()
        {
            DateTime expirationDate = DateTime.Now.AddDays(10);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            TimeSpan timeLeft = expirationDate - DateTime.Now;
            string expectedResult = postViewModel.DisplayRemainingTime(timeLeft);

            Assert.That(postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AvailableFor_ForUnknownPostType_ReturnsEmptyString()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            string expectedResult = Constants.EMPTY_STRING;

            Assert.That(postViewModel.AvailableFor, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Rating_ForFixedPricePost_ReturnsCorrectValue()
        {
            float expectedReviewScore = 30;
            Post fixedPrice = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), expectedReviewScore, Guid.NewGuid(), string.Empty, true);
            postViewModel.Post = fixedPrice;

            Assert.That(postViewModel.Rating, Is.EqualTo(expectedReviewScore));
        }

        [Test]
        public void Rating_ForAuctionPost_ReturnsCorrectValue()
        {
            float expectedReviewScore = 30;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
               string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), expectedReviewScore, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            Assert.That(postViewModel.Rating, Is.EqualTo(expectedReviewScore));
        }

        [Test]
        public void Visible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            Assert.That(postViewModel.Visible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Visible_SetValue_SetsCorrectValue()
        {
            string expectedResult = "Invisible";
            postViewModel.Visible = expectedResult;
            Assert.That(postViewModel.Visible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Description_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                expectedDescription, string.Empty, string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), string.Empty, true);
            postViewModel.Post = fixedPricePost;

            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, expectedDescription, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForUnknownPostType_ReturnsCorrectValue()
        {
            string expectedDescription = "expected description";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, expectedDescription, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Description_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), string.Empty, true);
            postViewModel.Post = fixedPricePost;

            string expectedDescription = "expected description";
            postViewModel.Description = expectedDescription;
            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            string expectedDescription = "expected description";
            postViewModel.Description = expectedDescription;
            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Description_ForUnknownPostType_SetsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            string expectedDescription = "expected description";
            postViewModel.Description = expectedDescription;
            Assert.That(postViewModel.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Contact_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, expectedContacts, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), string.Empty, true);
            postViewModel.Post = fixedPricePost;

            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                expectedContacts, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForUnknownPostType_ReturnsCorrectValue()
        {
            string expectedContacts = "expected contacts";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, expectedContacts, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }

        [Test]
        public void Contact_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), string.Empty, true);
            postViewModel.Post = fixedPricePost;

            string expectedContacts = "expected contacts";
            postViewModel.Contact = expectedContacts;
            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty,
                string.Empty, string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            string expectedContacts = "expected contacts";
            postViewModel.Contact = expectedContacts;
            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }
        [Test]
        public void Contact_ForUnknownPostType_SetsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            string expectedContacts = "expected contacts";
            postViewModel.Contact = expectedContacts;
            Assert.That(postViewModel.Contact, Is.EqualTo(expectedContacts));
        }

        [Test]
        public void Delivery_ForFixedPricePost_ReturnsCorrectValue()
        {
            string expectedDelivery = "expected delivery";
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, expectedDelivery, new List<Review>(), 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            postViewModel.Post = fixedPricePost;

            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForAuctionPost_ReturnsCorrectValue()
        {
            string expectedDelivery = "expected delivery";
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, expectedDelivery, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForDonationOrUnknownPostType_ReturnsEmptyString()
        {
            string expectedDelivery = Constants.EMPTY_STRING;
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            postViewModel.Post = donationPost;

            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));

            expectedDelivery = Constants.EMPTY_STRING;
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }

        [Test]
        public void Delivery_ForFixedPricePost_SetsCorrectValue()
        {
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            postViewModel.Post = fixedPricePost;

            string expectedDelivery = "expected delivery";
            postViewModel.Delivery = expectedDelivery;
            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForAuctionPost_SetsCorrectValue()
        {
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            string expectedDelivery = "expected delivery";
            postViewModel.Delivery = expectedDelivery;
            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }
        [Test]
        public void Delivery_ForDonationOrUnknownPost_ReturnsEmptyString()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            postViewModel.Post = donationPost;

            string expectedDelivery = Constants.EMPTY_STRING;
            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Delivery, Is.EqualTo(expectedDelivery));
        }

        [Test]
        public void DonationButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(postViewModel.DonationButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DonationButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            postViewModel.DonationButtonVisible = expectedResult;
            Assert.That(postViewModel.DonationButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BuyButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(postViewModel.BuyButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BuyButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            postViewModel.BuyButtonVisible = expectedResult;
            Assert.That(postViewModel.BuyButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidButtonVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(postViewModel.BidButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidButtonVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            postViewModel.BidButtonVisible = expectedResult;
            Assert.That(postViewModel.BidButtonVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidPriceVisible_GetDefaultValue_ReturnsCorrectValue()
        {
            string expectedResult = Constants.COLLAPSED_VISIBILITY;
            Assert.That(postViewModel.BidPriceVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BidPriceVisible_SetValue_SetsCorrectValue()
        {
            string expectedResult = Constants.VISIBLE_VISIBILITY;
            postViewModel.BidPriceVisible = expectedResult;
            Assert.That(postViewModel.BidPriceVisible, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Username_AnyUser_ReturnsCorrectValue()
        {
            string expectedUsername = "expected Username";
            User user = new User(expectedUsername, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.OurUser = user;

            Assert.That(postViewModel.Username, Is.EqualTo(expectedUsername));
        }

        [Test]
        public void Media_PostOfUnknownType_ReturnsCorrectValue()
        {
            string expectedMedia = "expected Media";
            Post noTypePost = new Post(expectedMedia, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Media, Is.EqualTo(expectedMedia));
        }

        [Test]
        public void Location_PostOfUnknownType_ReturnsCorrectValue()
        {
            string expectedLocation = "expected Location";
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), expectedLocation, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Location, Is.EqualTo(expectedLocation));
        }

        [Test]
        public void ProfilePicture_AnyUser_ReturnsCorrectValue()
        {
            string expectedProfilePicture = "expected Profile Picture Path";
            User user = new User(string.Empty, string.Empty, DateOnly.MaxValue, expectedProfilePicture, string.Empty);
            postViewModel.OurUser = user;

            Assert.That(postViewModel.ProfilePicture, Is.EqualTo(expectedProfilePicture));
        }

        [Test]
        public void TimePosted_AnyPost_ReturnsCorrectString()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            TimeSpan timePassed = DateTime.Now - noTypePost.CreationDate;
            string expectedResult = Math.Ceiling(timePassed.TotalSeconds).ToString() + PostContentViewModel.SECONDS_AGO;
            string actualResult = postViewModel.TimePosted;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetPost_AnyPost_PostIsCorrectlyReturned()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.GetPost, Is.EqualTo(noTypePost));
        }

        [Test]
        public void Price_ForFixedPricePost_ReturnsCorrectValue()
        {
            double expectedPrice = 1234;
            Post fixedPricePost = new FixedPricePost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty,
                string.Empty, string.Empty, string.Empty, expectedPrice, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Constants.FIXED_PRICE_POST_TYPE, true);
            postViewModel.Post = fixedPricePost;

            string expectedResult = Constants.DOLLAR_SIGN + expectedPrice;

            Assert.That(postViewModel.Price, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Price_ForAuctionPost_ReturnsCorrectValue()
        {
            double expectedPrice = 1234;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, expectedPrice, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), 0, 0, true);
            postViewModel.Post = auctionPost;

            string expectedResult = Constants.DOLLAR_SIGN + expectedPrice;

            Assert.That(postViewModel.Price, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Price_ForDonationOrUnknownPostType_ReturnsEmptyString()
        {
            string expectedPrice = Constants.EMPTY_STRING;
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            postViewModel.Post = donationPost;

            Assert.That(postViewModel.Price, Is.EqualTo(expectedPrice));

            expectedPrice = Constants.EMPTY_STRING;
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.Price, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void UpdateBidPrice_ForAuctionPostLessThanThirtySeconds_UpdatesCorrectly()
        {
            double currentBidPrice = 1234;
            double currentMinimumBidPrice = 1000;
            DateTime expirationDate = DateTime.Now.AddSeconds(10);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, currentMinimumBidPrice, true);
            postViewModel.Post = auctionPost;

            postViewModel.UpdateBidPrice();
            double expectedBidPrice = currentBidPrice + 5;
            double expectedMinimumBidPrice = currentMinimumBidPrice + 5;
            Assert.That(((FixedPricePost)postViewModel.Post).ExpirationDate, Is.EqualTo(expirationDate.AddSeconds(30)));
            Assert.That(((AuctionPost)postViewModel.Post).CurrentBidPrice, Is.EqualTo(expectedBidPrice));
            Assert.That(((AuctionPost)postViewModel.Post).MinimumBidPrice, Is.EqualTo(expectedMinimumBidPrice));
        }
        [Test]
        public void UpdateBidPrice_ForAuctionPostMoreThanThirtySeconds_UpdatesCorrectly()
        {
            double currentBidPrice = 1234;
            double currentMinimumBidPrice = 1000;
            DateTime expirationDate = DateTime.Now.AddSeconds(100);
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, expirationDate, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, currentMinimumBidPrice, true);
            postViewModel.Post = auctionPost;

            postViewModel.UpdateBidPrice();
            double expectedBidPrice = currentBidPrice + 5;
            double expectedMinimumBidPrice = currentMinimumBidPrice + 5;
            Assert.That(((FixedPricePost)postViewModel.Post).ExpirationDate, Is.EqualTo(expirationDate));
            Assert.That(((AuctionPost)postViewModel.Post).CurrentBidPrice, Is.EqualTo(expectedBidPrice));
            Assert.That(((AuctionPost)postViewModel.Post).MinimumBidPrice, Is.EqualTo(expectedMinimumBidPrice));
        }
        [Test]
        public void UpdateBidPrice_ForNotAuctionPost_ThrowsException()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            postViewModel.Post = donationPost;

            var exceptionMessage = Assert.Throws<Exception>(() => { postViewModel.UpdateBidPrice(); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post is not of type AuctionPost!"));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            exceptionMessage = Assert.Throws<Exception>(() => { postViewModel.UpdateBidPrice(); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post is not of type AuctionPost!"));
        }

        [Test]
        public void AddPostToFavorites_AnyValues_ShouldCallAddItemToFavorites()
        {
            postViewModel.AddPostToFavorites();

            Assert.That(fakeUserService.AddItemToFavoritesCalled, Is.EqualTo(true));
            Assert.That(fakeUserService.GroupId, Is.EqualTo(postViewModel.GroupId));
            Assert.That(fakeUserService.PostId, Is.EqualTo(postViewModel.Post.Id));
            Assert.That(fakeUserService.AccountId, Is.EqualTo(postViewModel.AccountId));
        }

        [Test]
        public void AddPostToCart_AnyValues_ShouldCallAddItemToCart()
        {
            postViewModel.AddPostToCart();

            Assert.That(fakeUserService.AddItemToCartCalled, Is.EqualTo(true));
            Assert.That(fakeUserService.GroupId, Is.EqualTo(postViewModel.GroupId));
            Assert.That(fakeUserService.PostId, Is.EqualTo(postViewModel.Post.Id));
            Assert.That(fakeUserService.AccountId, Is.EqualTo(postViewModel.AccountId));
        }

        [Test]
        public void BidPrice_ForAuctionPost_ReturnsCorrectValue()
        {
            double currentBidPrice = 1234;
            Post auctionPost = new AuctionPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty,
                string.Empty, 0, DateTime.Now, string.Empty, new List<Review>(), 0, Guid.NewGuid(), Guid.NewGuid(), currentBidPrice, 0, true);
            postViewModel.Post = auctionPost;

            string expectedResult = Constants.DOLLAR_SIGN + currentBidPrice;
            Assert.That(postViewModel.BidPrice, Is.EqualTo(expectedResult));
        }
        [Test]
        public void BidPrice_ForNotAuctionPost_ReturnsEmptyString()
        {
            Post donationPost = new DonationPost(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Constants.DONATION_POST_TYPE, true);
            postViewModel.Post = donationPost;

            string expectedResult = Constants.EMPTY_STRING;
            Assert.That(postViewModel.BidPrice, Is.EqualTo(expectedResult));

            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            postViewModel.Post = noTypePost;

            Assert.That(postViewModel.BidPrice, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Interests_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            postViewModel.Post = noTypePost;

            int expectedCount = 1;
            string expectedResult = expectedCount.ToString() + " interested";
            Assert.That(postViewModel.Interests, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddInterests_NoPreviousInterest_AddsInterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.Post = noTypePost;
            postViewModel.OurUser = newUser;

            postViewModel.AddInterests();

            Assert.That(postViewModel.Post.InterestStatuses.Count, Is.EqualTo(1));
            Assert.That(postViewModel.Post.InterestStatuses[0].PostId, Is.EqualTo(noTypePost.Id));
            Assert.That(postViewModel.Post.InterestStatuses[0].InterestedUserId, Is.EqualTo(newUser.Id));
        }

        [Test]
        public void AddInterests_InterestAlreadyExists_RemovesInterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.Post = noTypePost;
            postViewModel.OurUser = newUser;

            postViewModel.AddInterests();
            postViewModel.AddInterests();

            Assert.That(postViewModel.Post.InterestStatuses.Count, Is.EqualTo(0));
        }

        [Test]
        public void Uninterests_AnyPost_ReturnsUninterestedNumber()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.Post = noTypePost;
            postViewModel.OurUser = newUser;

            int expectedCount = 0;
            string expectedResult = expectedCount.ToString() + " uninterested";
            Assert.That(postViewModel.Uninterests, Is.EqualTo(expectedResult));

            postViewModel.AddUninterests();
            expectedCount = 1;
            expectedResult = expectedCount.ToString() + " uninterested";
            Assert.That(postViewModel.Uninterests, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddUninterests_NoPreviousUninterest_AddsUninterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.Post = noTypePost;
            postViewModel.OurUser = newUser;

            postViewModel.AddUninterests();

            Assert.That(postViewModel.Post.InterestStatuses.Count, Is.EqualTo(1));
            Assert.That(postViewModel.Post.InterestStatuses[0].PostId, Is.EqualTo(noTypePost.Id));
            Assert.That(postViewModel.Post.InterestStatuses[0].InterestedUserId, Is.EqualTo(newUser.Id));
        }

        [Test]
        public void AddUninterests_UninterestAlreadyExists_RemovesUninterest()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            User newUser = new User(string.Empty, string.Empty, DateOnly.MaxValue, string.Empty, string.Empty);
            postViewModel.Post = noTypePost;
            postViewModel.OurUser = newUser;

            postViewModel.AddUninterests();
            postViewModel.AddUninterests();

            Assert.That(postViewModel.Post.InterestStatuses.Count, Is.EqualTo(0));
        }

        [Test]
        public void Comments_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            postViewModel.Post = noTypePost;

            string expectedResult = noTypePost.Comments.Count + " comments";
            Assert.That(postViewModel.Comments, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SendBuyingMessage_FakeChatFactory_ShouldCallChatSendBuyingMessage()
        {
            postViewModel.SendBuyingMessage();
            FakeChat fakeChat = (FakeChat)postViewModel.OurChatFactory.OurChat;
            Assert.That(fakeChat.SendBuyingMessageCalled, Is.EqualTo(true));
        }

        [Test]
        public void HidePost_ForAnyPost_ReturnsCorrectValue()
        {
            Post noTypePost = new Post(string.Empty, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            noTypePost.AddInterestStatus(new InterestStatus(Guid.NewGuid(), Guid.NewGuid(), true));
            postViewModel.Post = noTypePost;

            postViewModel.HidePost();
            Assert.That(postViewModel.Visible, Is.EqualTo(Constants.COLLAPSED_VISIBILITY));
        }
    }
}
