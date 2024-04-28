using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class FixedPricePostTests
    {
        private FixedPricePost fixedPricePost;

        [SetUp]
        public void SetUp()
        {
            fixedPricePost = new FixedPricePost();
        }

        [Test]
        public void Price_Any_UpdatesPrice()
        {
            fixedPricePost.Price = 11.5f;

            Assert.That(fixedPricePost.Price, Is.EqualTo(11.5f));
        }

        [Test]
        public void ExpirationDate_Any_UpdatesExpirationDate()
        {
            fixedPricePost.ExpirationDate = new DateTime(2015, 12, 31);

            Assert.That(fixedPricePost.ExpirationDate.ToString(), Is.EqualTo("12/31/2015 12:00:00 AM"));
        }

        [Test]
        public void ReviewScore_Any_UpdatesReviewScore()
        {
            fixedPricePost.ReviewScore = 53.9f;

            Assert.That(fixedPricePost.ReviewScore, Is.EqualTo(53.9f));
        }

        [Test]
        public void Delivery_Any_UpdatesDelivery()
        {
            fixedPricePost.Delivery = "A";

            Assert.That(fixedPricePost.Delivery, Is.EqualTo("A"));
        }

        [Test]
        public void AddReview_NotAlreadyExistentReview_ReviewIsAdded()
        {
            Review firstReview = new Review();
            Review secondReview = new Review();
            fixedPricePost.AddReview(firstReview);
            fixedPricePost.AddReview(secondReview);

            List<Review> expectedReviews = new List<Review>()
            {
                firstReview,
                secondReview,
            };
            List<Review> actualReviews = fixedPricePost.Reviews;

            Assert.That(actualReviews, Is.EqualTo(expectedReviews));
        }

        [Test]
        public void AddReview_AlreadyExistentReview_ExceptionThrown()
        {
            Review reviewToAdd = new Review();
            fixedPricePost.AddReview(reviewToAdd);

            var exceptionMessage = Assert.Throws<Exception>(() => { fixedPricePost.AddReview(reviewToAdd); });

            Assert.That(exceptionMessage.Message, Is.EqualTo("Review already exists. Edit the existing one if you want"));
        }

        [Test]
        public void RemoveReview_ExistingReview_ReviewRemoved()
        {
            Review firstReview = new Review();
            Review secondReview = new Review();
            fixedPricePost.AddReview(firstReview);
            fixedPricePost.AddReview(secondReview);

            List<Review> listOfReviewsToVerifyRemoval = new List<Review>()
            {
                secondReview,
            };

            fixedPricePost.RemoveReview(firstReview);
            List<Review> actualReviews = fixedPricePost.Reviews;

            Assert.That(actualReviews, Is.EqualTo(listOfReviewsToVerifyRemoval));
        }

        [Test]
        public void RemoveReview_NotAlreadyExistingReview_ExceptionThrown()
        {
            Review reviewNeverAdded = new Review();
            Review reviewAdded = new Review();
            fixedPricePost.AddReview(reviewAdded);

            var exceptionMessage = Assert.Throws<Exception>(() => { fixedPricePost.RemoveReview(reviewNeverAdded); });

            Assert.That(exceptionMessage.Message, Is.EqualTo("Review does not exist"));
        }

        [Test]
        public void BuyProduct_ProductAlreadyBought_ExceptionThrown()
        {
            Guid guidOfBuyer = Guid.NewGuid();
            fixedPricePost.BuyerId = guidOfBuyer;

            var exceptionMessage = Assert.Throws<Exception>(() => { fixedPricePost.BuyProduct(guidOfBuyer); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Product already bought"));
        }

        [Test]
        public void BuyProduct_ProductNotAlreadyBought_BuyerIdIsUpdated()
        {
            Guid guidOfBuyer = Guid.Empty;
            fixedPricePost.BuyProduct(guidOfBuyer);

            Assert.That(fixedPricePost.BuyerId, Is.EqualTo(guidOfBuyer));
        }
    }
}
