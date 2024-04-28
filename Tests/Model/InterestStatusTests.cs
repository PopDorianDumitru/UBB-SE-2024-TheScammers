using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class InterestStatusTests
    {
        private Guid interestedUserId;
        private Guid postId;
        private Guid interestedStatusId;
        private bool interested;

        private InterestStatus emptyContructorInterestStatus;
        private InterestStatus otherConstructorInterestStatus;

        [SetUp]
        public void SetUp()
        {
            interestedUserId = Guid.NewGuid();
            postId = Guid.NewGuid();
            interestedStatusId = Guid.NewGuid();
            interested = true;

            emptyContructorInterestStatus = new InterestStatus();
            otherConstructorInterestStatus = new InterestStatus(interestedUserId, postId, interested);
        }

        [Test]
        public void InterestedUserIdGet_GetUserIdFromOtherConstructorInterestStatus_IdShouldBeEqualToInterestedUserId()
        {
            Assert.That(otherConstructorInterestStatus.InterestedUserId, Is.EqualTo(interestedUserId));
        }

        [Test]
        public void PostIdGet_GetPostIdOfFromOtherConstructorInterestStatus_IdShouldBeEqualToPostId()
        {
            Assert.That(otherConstructorInterestStatus.PostId, Is.EqualTo(postId));
        }

        [Test]
        public void InterestStatusIdGet_GetInterestStatusIdFromOtherConstructorInterestStatus_ShouldBeEqualToInterested()
        {
            Assert.That(otherConstructorInterestStatus.Interested, Is.EqualTo(interested));
        }

        [Test]
        public void ToggleInterested_ToggleOtherConstructorInterestStatus_InterestedShouldBeEqualToFalse()
        {
            otherConstructorInterestStatus.ToggleInterested();
            Assert.That(otherConstructorInterestStatus.Interested, Is.False);
        }
        [Test]
        public void ToggleInterested_ToggleOtherConstructorInterestStatus_ReturnedInterestValueShouldBeFalse()
        {
            bool returnedInterestValue = otherConstructorInterestStatus.ToggleInterested();
            Assert.That(returnedInterestValue, Is.False);
        }

        [Test]
        public void InterestedUserIdGet_GetUserIdFromEmptyConstructorInterestStatus_ShouldNotBeEmpty()
        {
            Assert.That(emptyContructorInterestStatus.InterestedUserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void InterestedPostIdGet_GetPostIdFromEmptyConstructorInterestStatus_ShouldNotBeEmpty()
        {
            Assert.That(emptyContructorInterestStatus.PostId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void InterestedStatusIdGet_GetInterestedStatusIdFromEmptyConstructorInterestStatus_ShouldNotBeEmpty()
        {
            Assert.That(emptyContructorInterestStatus.InterestStatusId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ToggleInterested_ToggleEmptyConstructorInterestStatus_InterestedShouldBeEqualToTrue()
        {
            emptyContructorInterestStatus.ToggleInterested();
            Assert.That(emptyContructorInterestStatus.Interested, Is.True);
        }
        [Test]
        public void ToggleInterested_ToggleEmptyConstructorInterestStatus_ReturnedInterestValueShouldBeTrue()
        {
            bool returnedInterestValue = emptyContructorInterestStatus.ToggleInterested();
            Assert.That(returnedInterestValue, Is.True);
        }

        [Test]
        public void InterestedGet_GetInterestedFromOtherConstructorInterestStatus_ShouldBeEqualToInterest()
        {
            Assert.That(otherConstructorInterestStatus.Interested, Is.EqualTo(interested));
        }

        [Test]
        public void InterestedGet_GetInterestedFromEmptyConstructorInterestStatus_ShouldBeTrue()
        {
            Assert.That(otherConstructorInterestStatus.Interested, Is.True);
        }
    }
}
