using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class FixedPricePostTests
    {
        private FixedPricePost _fixedPricePost;

        [SetUp]
        public void SetUp()
        {
            _fixedPricePost = new FixedPricePost();
        }

        [Test]
        public void Price_Test()
        {
            FixedPricePost fixedPricePost = new FixedPricePost();
            fixedPricePost.Price = 11.5f;

            Assert.That(fixedPricePost.Price, Is.EqualTo(11.5f));
        }

        [Test]
        public void ExpirationDate_Test()
        {
            FixedPricePost fixedPricePost = new FixedPricePost();
            fixedPricePost.ExpirationDate = new DateTime(2015, 12, 31);

            Assert.That(fixedPricePost.ExpirationDate.ToString(), Is.EqualTo("31.12.2015 00:00:00"));
        }

        [Test]
        public void ReviewScore_Test()
        {
            FixedPricePost fixedPricePost = new FixedPricePost();
            fixedPricePost.ReviewScore = 53.9f;

            Assert.That(fixedPricePost.ReviewScore, Is.EqualTo(53.9f));
        }

        [Test]
        public void Delivery()
        {
            FixedPricePost fixedPricePost = new FixedPricePost();
            fixedPricePost.Delivery = "A";

            Assert.That(fixedPricePost.Delivery, Is.EqualTo("A"));
        }

        [Test]
        public void AddReview_AnyReview_ReviewAdded()
        {
            Review firstReview = new Review();
            Review secondReview = new Review();
            _fixedPricePost.AddReview(firstReview);
            _fixedPricePost.AddReview(secondReview);

            List<Review> expectedReviews = new List<Review>()
            {
                firstReview,
                secondReview,
            };
            List<Review> actualReviews = _fixedPricePost.Reviews;

            Assert.That(actualReviews, Is.EqualTo(expectedReviews));
            Assert.That(actualReviews.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddReview_AnyReview_ExceptionThrown()
        {
            Review firstReview = new Review();
            Review secondReview = firstReview;
            _fixedPricePost.AddReview(firstReview);

            var exceptionMessage = Assert.Throws<Exception>(() => { _fixedPricePost.AddReview(secondReview); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Review already exists. Edit the existing one if you want"));
        }

        [Test]
        public void RemoveReview_AnyReview_ReviewRemoved()
        {
            Review firstReview = new Review();
            Review secondReview = new Review();
            _fixedPricePost.AddReview(firstReview);
            _fixedPricePost.AddReview(secondReview);

            List<Review> listOfReviewsToVerifyRemoval = new List<Review>()
            {
                secondReview,
            };

            _fixedPricePost.RemoveReview(firstReview);
            List<Review> actualReviews = _fixedPricePost.Reviews;

            Assert.That(actualReviews, Is.EqualTo(listOfReviewsToVerifyRemoval));
            Assert.That(actualReviews.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveReview_AnyReview_ExceptionThrown()
        {
            Review firstReview = new Review();
            Review secondReview = new Review();
            _fixedPricePost.AddReview(firstReview);
            _fixedPricePost.AddReview(secondReview);

            _fixedPricePost.RemoveReview(firstReview);

            var exceptionMessage = Assert.Throws<Exception>(() => { _fixedPricePost.RemoveReview(firstReview); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Review does not exist"));
        }

        [Test]
        public void BuyProduct_AnyProduct_Successful()
        {
            Guid guidOfBuyer = Guid.NewGuid();
            _fixedPricePost.BuyerId = guidOfBuyer;

            var exceptionMessage = Assert.Throws<Exception>(() => { _fixedPricePost.BuyProduct(guidOfBuyer); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Product already bought"));
        }

        [Test]
        public void BuyProduct_AnyProduct_ExceptionThrown()
        {
            Guid guidOfBuyer = Guid.Empty;
            _fixedPricePost.BuyProduct(guidOfBuyer);

            Assert.That(_fixedPricePost.BuyerId, Is.EqualTo(guidOfBuyer));
        }
    }
}
