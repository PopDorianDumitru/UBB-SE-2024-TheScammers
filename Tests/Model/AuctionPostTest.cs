using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class AuctionPostTest
    {
        public AuctionPost postEmpty;
        public AuctionPost postWithId;
        public AuctionPost postWithoutId;

        public Guid currentPriceLeader;
        public double currentBidPrice;
        public double minimumBidPrice;
        private bool onGoing;

        [SetUp]
        public void SetUp()
        {
            currentPriceLeader = Guid.NewGuid();
            currentBidPrice = 200;
            minimumBidPrice = 100;
            onGoing = true;

            postEmpty = new AuctionPost();
            postWithId = new AuctionPost(Guid.NewGuid(), new List<Guid>(), new List<Guid>(), new List<Comment>(), "", DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), true, new List<Guid>(), "", "", "", new List<InterestStatus>(), "", new List<Report>(), 0, DateTime.Now, "", new List<Review>(), 0, Guid.NewGuid(), currentPriceLeader, currentBidPrice, minimumBidPrice, false, 100, onGoing);
            postWithoutId = new AuctionPost("", Guid.NewGuid(), Guid.NewGuid(), "", "", "", "", 100, DateTime.Now, "", new List<Review>(), 0, Guid.NewGuid(), currentPriceLeader, currentBidPrice, minimumBidPrice, true);
        }

        [Test]
        public void OnGoingGet_FromPostInstantiatedWithoutId_ShouldBeTrue()
        {
            Assert.That(postWithoutId.OnGoing, Is.True);
        }

        [Test]
        public void OnGoingSet_ForPostEmpty_OnGoingBecomesNewValue()
        {
            bool newOnGoing = true;
            postEmpty.OnGoing = newOnGoing;
            Assert.That(postEmpty.OnGoing, Is.EqualTo(newOnGoing));
        }

        [Test]
        public void OnGoingSet_ForPostWithoutId_OnGoingBecomesNewValue()
        {
            bool newOnGoing = false;
            postWithoutId.OnGoing = newOnGoing;
            Assert.That(postWithoutId.OnGoing, Is.EqualTo(newOnGoing));
        }

        [Test]
        public void OnGoingSet_ForPostWithId_OnGoingBecomesNewValue()
        {
            bool newOnGoing = false;
            postWithId.OnGoing = newOnGoing;
            Assert.That(postWithId.OnGoing, Is.EqualTo(newOnGoing));
        }

        [Test]
        public void CurrentBidPrice_ForPostEmpty_ShouldBeZero()
        {
            Assert.That(postEmpty.CurrentBidPrice, Is.EqualTo(0));
        }

        [Test]
        public void CurrentBidPrice_ForPostWithId_ShouldBeEqualToCurrentBidPrice()
        {
            Assert.That(postWithId.CurrentBidPrice, Is.EqualTo(currentBidPrice));
        }

        [Test]
        public void CurrentBidPrice_ForPostWithoutId_ShouldBeEqualToCurrentBidPrice()
        {
            Assert.That(postWithId.CurrentBidPrice, Is.EqualTo(currentBidPrice));
        }

        [Test]
        public void CurrentBidPrice_ChangeForPostEmpty_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postEmpty.CurrentBidPrice = newValue;
            Assert.That(postEmpty.CurrentBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void CurrentBidPrice_ChangeForPostWithId_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postWithId.CurrentBidPrice = newValue;
            Assert.That(postWithId.CurrentBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void CurrentBidPrice_ChangeForPostWithoutId_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postWithoutId.CurrentBidPrice = newValue;
            Assert.That(postWithoutId.CurrentBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void MinimumBidPrice_ChangeForPostEmpty_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postEmpty.MinimumBidPrice = newValue;
            Assert.That(postEmpty.MinimumBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void MinimumBidPrice_ChangeForPostWithId_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postWithId.MinimumBidPrice = newValue;
            Assert.That(postWithId.MinimumBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void MinimumBidPrice_ChangeForPostWithoutId_ShouldBeEqualToNewValue()
        {
            double newValue = 140;
            postWithoutId.MinimumBidPrice = newValue;
            Assert.That(postWithoutId.MinimumBidPrice, Is.EqualTo(newValue));
        }

        [Test]
        public void MinimumBidPrice_ForPostEmpty_ShouldBeZero()
        {
            Assert.That(postEmpty.MinimumBidPrice, Is.EqualTo(0));
        }
        [Test]
        public void MinimumBidPrice_ForPostWithId_ShouldBeEqualToMinimumBidPrice()
        {
            Assert.That(minimumBidPrice, Is.EqualTo(postWithId.MinimumBidPrice));
        }

        [Test]
        public void MinimumBidPrice_ForPostWithoutId_ShouldBeEqualToMinimumBidPrice()
        {
            Assert.That(minimumBidPrice, Is.EqualTo(postWithoutId.MinimumBidPrice));
        }

        [Test]
        public void CurrentPriceLeader_ForPostEmpty_ShouldBeEmpty()
        {
            Assert.That(postEmpty.CurrentPriceLeader, Is.EqualTo(Guid.Empty));
        }
        [Test]
        public void CurrentPriceLeader_ForPostWithoutId_ShouldBeEmpty()
        {
            Assert.That(postWithoutId.CurrentPriceLeader, Is.EqualTo(Guid.Empty));
        }
        [Test]
        public void CurrentPriceLeader_ForPostWithId_ShouldBeEqualToCurrentPriceLeader()
        {
            Assert.That(postWithId.CurrentPriceLeader, Is.EqualTo(currentPriceLeader));
        }

        [Test]
        public void CurrentPriceLeader_ChangePriceLeaderForPostEmpty_ShouldBeEqualToNewValue()
        {
            Guid newValue = Guid.NewGuid();
            postEmpty.CurrentPriceLeader = newValue;
            Assert.That(postEmpty.CurrentPriceLeader, Is.EqualTo(newValue));
        }

        [Test]
        public void CurrentPriceLeader_ChangePriceLeaderForPostWithoutId_ShouldBeEqualToNewValue()
        {
            Guid newValue = Guid.NewGuid();
            postWithoutId.CurrentPriceLeader = newValue;
            Assert.That(postWithoutId.CurrentPriceLeader, Is.EqualTo(newValue));
        }

        [Test]
        public void CurrentPriceLeader_ChangePriceLeaderForPostWithId_ShouldBeEqualToNewValue()
        {
            Guid newValue = Guid.NewGuid();
            postWithId.CurrentPriceLeader = newValue;
            Assert.That(postWithId.CurrentPriceLeader, Is.EqualTo(newValue));
        }

        [Test]
        public void Type_ForPostEmpty_ShouldBeAuction()
        {
            Assert.That(postEmpty.Type, Is.EqualTo(Constants.AUCTION_POST_TYPE));
        }

        [Test]
        public void Type_ForPostWithoutId_ShouldBeAuction()
        {
            Assert.That(postWithoutId.Type, Is.EqualTo(Constants.AUCTION_POST_TYPE));
        }

        [Test]
        public void Type_ForPostWithId_ShouldBeAuction()
        {
            Assert.That(postWithId.Type, Is.EqualTo(Constants.AUCTION_POST_TYPE));
        }

        [Test]
        public void Add30SecondsToExpirationDate_ForAnyPost_ShouldAdd30Seconds()
        {
            DateTime expectedExpirationDate = DateTime.Now.AddSeconds(30);

            postEmpty.Add30SecondsToExpirationDate();
            DateTime actualExpirationDate = postEmpty.ExpirationDate;
            TimeSpan difference = actualExpirationDate - expectedExpirationDate;
            Assert.Less(difference.TotalSeconds, 1);
        }

        [Test]
        public void PlaceBid_ForAnyPostBidSmallerThanMinimum_ShouldThrowException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { postEmpty.PlaceBid(Guid.NewGuid(), -1000); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Bid price is lower than minimum bid price"));
        }

        [Test]
        public void PlaceBid_ForAnyPostBidIsHigherThanMinimumButLowerThanCurrent_CurrentBidPriceShouldRemainTheSame()
        {
            double initialPrice = postWithoutId.CurrentBidPrice;
            postWithoutId.PlaceBid(Guid.NewGuid(), 150);
            Assert.That(postWithoutId.CurrentBidPrice, Is.EqualTo(initialPrice));
        }

        [Test]
        public void PlaceBid_ForAnyPostBidIsHigherThanCurrentPrice_CurrentBidPriceChangesToNewValue()
        {
            postWithoutId.PlaceBid(new Guid(), 900);
            Assert.That(postWithoutId.CurrentBidPrice, Is.EqualTo(900));
        }
        [Test]
        public void PlaceBid_ForAnyPostBidIsHigherThanCurrentPrice_PriceLeaderChangesToNewValue()
        {
            Guid newPriceLeader = Guid.NewGuid();
            postWithoutId.PlaceBid(newPriceLeader, 900);
            Assert.That(postWithoutId.CurrentPriceLeader, Is.EqualTo(newPriceLeader));
        }

        [Test]
        public void PlaceBid_ForAnyPostBidIsHigherThanCurrentPrice_ExpirationDateIsExtended()
        {
            Guid newPriceLeader = Guid.NewGuid();

            DateTime expectedExpirationDate = DateTime.Now.AddSeconds(30);

            postWithoutId.PlaceBid(newPriceLeader, 900);
            DateTime actualExpirationDate = postEmpty.ExpirationDate;
            TimeSpan difference = actualExpirationDate - expectedExpirationDate;
            Assert.Less(difference.TotalSeconds, 1);
        }
    }
}
