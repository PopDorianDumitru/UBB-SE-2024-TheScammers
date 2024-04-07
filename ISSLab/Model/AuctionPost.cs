using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class AuctionPost : FixedPricePost
    {
        private Guid currentPriceLeader;
        private double currentBidPrice;
        private double minimumBidPrice;

        public AuctionPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, float review, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice) : base(media, authorId, groupId, location, description, title, contacts, price, expirationDate, review, delivery, reviews, reviewScore, buyerId)
        {
            this.currentPriceLeader = currentPriceLeader;
            this.currentBidPrice = currentBidPrice;
            this.minimumBidPrice = minimumBidPrice;
        }

        public AuctionPost() : base()
        {
            this.currentPriceLeader = Guid.Empty;
            this.currentBidPrice = 0;
            this.minimumBidPrice = 0;

        }

        public AuctionPost(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string media, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string location, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, double price, DateTime expirationDate, float review, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice) : base(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interestStatuses, contacts, reports, price, expirationDate, review, delivery, reviews, reviewScore, buyerId)
        {
            this.currentPriceLeader = currentPriceLeader;
            this.currentBidPrice = currentBidPrice;
            this.minimumBidPrice = minimumBidPrice;
        }

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
                add30SecondsToExpirationDate();
            }
        }

        public void add30SecondsToExpirationDate()
        {
            DateTime now = DateTime.Now;
            if((this.ExpirationDate - now).TotalSeconds <= 60)
                this.ExpirationDate = this.ExpirationDate.AddSeconds(30);
        }

    }
}
