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
        public void ReviewScore_Test()
        {
            DonationPost donationPost = new DonationPost();
            donationPost.ReviewScore = 1.0f;

            Assert.That(donationPost.ReviewScore, Is.EqualTo(1.0f));
        }

        [Test]
        public void DonationAmount_Test()
        {
            DonationPost donationPost = new DonationPost();
            donationPost.DonationAmount = 10.6f;

            Assert.That(donationPost.DonationAmount, Is.EqualTo(10.6f));
        }

        [Test]
        public void DonationPageLink_Test()
        {
            DonationPost donationPost = new DonationPost();
            donationPost.DonationPageLink = "/1";

            Assert.That(donationPost.DonationPageLink, Is.EqualTo("/1"));
        }
    }
}
