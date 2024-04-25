using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class AuctionPost : FixedPricePost
    {
        private Guid currentPriceLeader;
        private double currentBidPrice;
        private double minimumBidPrice;
        private bool onGoing;

        public AuctionPost(string mediaContent, Guid authorId, Guid groupId, string itemLocation, string description, string title, string phoneNumber, double price, DateTime expirationDate, string deliveryType, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice, bool confirmed) : base(mediaContent, authorId, groupId, itemLocation, description, title, phoneNumber, price, expirationDate, deliveryType, reviews, reviewScore, buyerId, "Auction", confirmed)
        {
            this.currentPriceLeader =Guid.Empty;
            this.currentBidPrice = currentBidPrice;
            this.minimumBidPrice = minimumBidPrice;
            this.onGoing = true;
        }

        public AuctionPost() : base()
        {
            this.Type = "Auction";
            this.currentPriceLeader = Guid.Empty;
            this.currentBidPrice = 0;
            this.minimumBidPrice = 0;

        }

        public AuctionPost(Guid postId, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string mediaContent, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string itemLocation, string description, string title, List<InterestStatus> interestStatuses, string phoneNumber, List<Report> reports, double price, DateTime expirationDate, string deliveryType, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice, bool confirmed, int viewCount,bool onGoing) : base(postId, usersThatShared, usersThatLiked, comments, mediaContent, creationDate, authorId, groupId, promoted, usersThatFavorited, itemLocation, description, title, interestStatuses, phoneNumber, reports, price, expirationDate, deliveryType, reviews, reviewScore, buyerId, "Auction", confirmed, viewCount)
        {
            this.currentPriceLeader = currentPriceLeader;
            this.currentBidPrice = currentBidPrice;
            this.minimumBidPrice = minimumBidPrice;
            this.onGoing = onGoing;
        }

        public bool OnGoing { get => onGoing; set => onGoing = value; }

        public double CurrentBidPrice{ get => currentBidPrice; set => currentBidPrice = value; }

        public double MinimumBidPrice { get => minimumBidPrice; set => minimumBidPrice = value; }

        public Guid CurrentPriceLeader { get => currentPriceLeader; set => currentPriceLeader = value; }

        public void PlaceBid(Guid userId, double bidPrice)
        {
            if(bidPrice <= minimumBidPrice)
            {
                throw new Exception("Bid price is lower than minimum bid price");
            }
            if (bidPrice > currentBidPrice)
            {
                currentBidPrice = bidPrice;
                currentPriceLeader = userId;
                Add30SecondsToExpirationDate();
            }
        }

        public void Add30SecondsToExpirationDate()
        {
            DateTime now = DateTime.Now;
            this.ExpirationDate = this.ExpirationDate.AddSeconds(30);
        }

    }
}
