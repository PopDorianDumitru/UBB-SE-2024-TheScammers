using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class DonationPostTests
    {
        private DonationPost donationPost;

        [SetUp]
        public void SetUp()
        {
            donationPost = new DonationPost();
        }

        [Test]
        public void ReviewScore_AnyDonationPost_ReturnsReviewScore()
        {
            donationPost.ReviewScore = 1.0f;

            Assert.That(donationPost.ReviewScore, Is.EqualTo(1.0f));
        }

        [Test]
        public void DonationAmount_AnyDonationPost_ReturnsDonationAmount()
        {
            donationPost.DonationAmount = 10.6f;

            Assert.That(donationPost.DonationAmount, Is.EqualTo(10.6f));
        }

        [Test]
        public void DonationPageLink_AnyDonationPost_ReturnsDonationPageLink()
        {
            donationPost.DonationPageLink = "/1";

            Assert.That(donationPost.DonationPageLink, Is.EqualTo("/1"));
        }

        [Test]
        public void AddReview_AnyPostOrReview_DoesNotCrash()
        {
            donationPost.AddReview(new Review());

            Assert.Pass();
        }

        [Test]
        public void RemoveReview_AnyPostOrReview_DoesNotCrash()
        {
            donationPost.RemoveReview(new Review());

            Assert.Pass();
        }
    }
}
