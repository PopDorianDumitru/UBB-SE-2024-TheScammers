using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class DonationPostTests
    {
        private DonationPost _donationPost;

        [SetUp]
        public void SetUp()
        {
            _donationPost = new DonationPost();
        }

        [Test]
        public void ReviewScore_AnyDonationPost_ReturnsReviewScore()
        {
            _donationPost.ReviewScore = 1.0f;

            Assert.That(_donationPost.ReviewScore, Is.EqualTo(1.0f));
        }

        [Test]
        public void DonationAmount_AnyDonationPost_ReturnsDonationAmount()
        {
            _donationPost.DonationAmount = 10.6f;

            Assert.That(_donationPost.DonationAmount, Is.EqualTo(10.6f));
        }

        [Test]
        public void DonationPageLink_AnyDonationPost_ReturnsDonationPageLink()
        {
            _donationPost.DonationPageLink = "/1";

            Assert.That(_donationPost.DonationPageLink, Is.EqualTo("/1"));
        }

        [Test]
        public void AddReview_AnyPostOrReview_DoesNotCrash()
        {
            _donationPost.AddReview(new Review());

            Assert.Pass();
        }
        
        [Test]
        public void RemoveReview_AnyPostOrReview_DoesNotCrash()
        {
            _donationPost.RemoveReview(new Review());

            Assert.Pass();
        }
    }
}
