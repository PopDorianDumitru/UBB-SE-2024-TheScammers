using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using Moq;

namespace Tests.Model
{
    internal class DonationPostTests
    {
        private DonationPost donationPostSimpleConstructor;
        private DonationPost donationPost;
        private Guid id;
        private List<Guid> usersThatShared;
        private List<Guid> usersThatLiked;
        private List<Comment> comments;
        private string media;
        private DateTime creationDate;
        private Guid authorId;
        private Guid groupId;
        private bool promoted;
        private List<Guid> usersThatFavorited;
        private string location;
        private string description;
        private string title;
        private List<InterestStatus> interestStatuses;
        private string contacts;
        private List<Report> reports;
        private float reviewScore;
        private double currentDonationAmount;
        private string donationPageLink;
        private string type;
        private bool confirmed;
        private int views;

        [SetUp]
        public void SetUp()
        {
            donationPost = new DonationPost();
            donationPostSimpleConstructor = new DonationPost(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interestStatuses, contacts, reports, reviewScore, currentDonationAmount, donationPageLink, type, confirmed, views);
            id = Guid.NewGuid();
            usersThatShared = new List<Guid>();
            usersThatLiked = new List<Guid>();
            comments = new List<Comment>();
            media = " ";
            creationDate = new DateTime();
            authorId = Guid.NewGuid();
            groupId = Guid.NewGuid();
            promoted = true;
            usersThatFavorited = new List<Guid>();
            location = " ";
            description = " ";
            title = " ";
            interestStatuses = new List<InterestStatus>();
            contacts = " ";
            reports = new List<Report>();
            reviewScore = 0f;
            currentDonationAmount = 0f;
            donationPageLink = " ";
            type = " ";
            confirmed = true;
            views = 0;
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
