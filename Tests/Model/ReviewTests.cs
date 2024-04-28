using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class ReviewTests
    {
        private Review reviewToTest1;
        private Review reviewToTest2;
        private Review reviewToTest3;

        [SetUp]
        public void SettingUp()
        {
            reviewToTest1 = new Review(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "content1", DateTime.Parse("Jan 11,2024"), 10);
            reviewToTest2 = new Review(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "content2", DateTime.Parse("Jan 11,2024"), 10);
            reviewToTest3 = new Review();
        }

        [Test]
        public void IdGet_ReviewFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest1.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void IdGet_ReviewSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest2.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void IdGet_ReviewThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest3.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GroupIdGet_ReviewFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest1.GroupId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GroupIdGet_ReviewSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest2.GroupId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GroupIdGet_ReviewThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest3.GroupId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GroupIdSet_ReportThirdConstructor_ShouldBeEqualWithTheNewId()
        {
            Guid newUserId = Guid.NewGuid();
            reviewToTest3.GroupId = newUserId;
            Assert.That(reviewToTest3.GroupId == newUserId);
        }

        [Test]
        public void SellerIdGet_GetTheSellerIdReviewFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest1.SellerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void SellerIdGet_GetTheSellerIdOfReviewSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest2.SellerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void SellerIdGet_GetTheSellerIdOfReviewThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest3.SellerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void SellerIdSet_SetTheSellerIdReportThirdConstructor_ShouldBeEqualWithTheNewGeneratedId()
        {
            Guid newSellerId = Guid.NewGuid();
            reviewToTest3.SellerId = newSellerId;
            Assert.That(reviewToTest3.SellerId == newSellerId);
        }

        public void ReviewerIdGet_GetTheReviewerIdReviewFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest1.ReviewerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReviewerGet_GetTheReviewerIdOfReviewSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest2.ReviewerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReviewerIdGet_GetThReviewerIdOfReviewThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.That(reviewToTest3.ReviewerId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ReviewerIdSet_SetTheReviewerIdReportThirdConstructor_ShouldBeEqualWithTheNewGeneratedId()
        {
            Guid newReviewerId = Guid.NewGuid();
            reviewToTest3.ReviewerId = newReviewerId;
            Assert.That(reviewToTest3.ReviewerId == newReviewerId);
        }

        [Test]
        public void DateOfReviewGet_GetDateOfReviewForReportSecondConstructor_ShouldBeJan112024()
        {
            Assert.True(reviewToTest2.DateOfReview == DateTime.Parse("Jan 11,2024"));
        }

        [Test]
        public void DateOfReviewGet_GetDateOfReviewForReportFirstConstructor_ShouldBeInstanceOdDateTime()
        {
            Assert.IsInstanceOf<DateTime>(reviewToTest2.DateOfReview);
        }

        [Test]
        public void DateOfReviewGet_GetDateOfReviewForReportThirdConstructor_ShouldBeInstanceOdDateTime()
        {
            Assert.IsInstanceOf<DateTime>(reviewToTest3.DateOfReview);
        }

        [Test]
        public void DateOfReviewSet_SetDateOfReviewForReportFirstConstructor_ShouldBeJan112024()
        {
            reviewToTest1.DateOfReview = DateTime.Parse("Jan 11,2024");
            Assert.True(reviewToTest1.DateOfReview == DateTime.Parse("Jan 11,2024"));
        }

        [Test]
        public void ContentGet_GetContentForReportFirstConstructor_ShouldBeEqualWithcontent1()
        {
            Assert.True(reviewToTest1.Content == "content1");
        }

        [Test]
        public void ContentGet_GetContentForReportSecondConstructor_ShouldBeEqualWithcontent2()
        {
            Assert.True(reviewToTest2.Content == "content2");
        }

        [Test]
        public void ContentGet_GetContentForReportThirdConstructor_ShouldBeEmpty()
        {
            Assert.True(reviewToTest3.Content == string.Empty);
        }

        [Test]
        public void ContentSet_SetContentForReportSecondConstructor_ShouldBeEqualWithNewContent()
        {
            reviewToTest2.Content = "NewContent";
            Assert.True(reviewToTest2.Content == "NewContent");
        }

        [Test]
        public void RatingGet_GetRatingForReportFirstConstructor_ShouldBeEqualWith10()
        {
            Assert.True(reviewToTest1.Rating == 10);
        }

        [Test]
        public void RatingGet_GetRatingForReportSecondConstructor_ShouldBeEqualWith10()
        {
            Assert.True(reviewToTest2.Rating == 10);
        }

        [Test]
        public void RatingGet_GetRatingForReportThirdConstructor_ShouldBeEqualWith0()
        {
            Assert.True(reviewToTest3.Rating == 0);
        }

        [Test]
        public void RatingSet_SetRatingForReportThirdConstructor_ShouldBeEqual8()
        {
            reviewToTest3.Rating = 8;
            Assert.True(reviewToTest3.Rating == 8);
        }
    }
}

