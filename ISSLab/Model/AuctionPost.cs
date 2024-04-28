using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class AuctionPost : FixedPricePost
    {
        private Guid _currentPriceLeader;
        private double _currentBidPrice;
        private double _minimumBidPrice;
        private bool _onGoing;

        public AuctionPost(string mediaContent, Guid authorId, Guid groupId, string itemLocation, string description, string title, string phoneNumber, double price, DateTime expirationDate, string deliveryType, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice, bool confirmed) : base(mediaContent, authorId, groupId, itemLocation, description, title, phoneNumber, price, expirationDate, deliveryType, reviews, reviewScore, buyerId, Constants.AUCTION_POST_TYPE, confirmed)
        {
            this._currentPriceLeader = Guid.Empty;
            this._currentBidPrice = currentBidPrice;
            this._minimumBidPrice = minimumBidPrice;
            this._onGoing = true;
        }

        public AuctionPost() : base()
        {
            this.Type = Constants.AUCTION_POST_TYPE;
            this._currentPriceLeader = Guid.Empty;
            this._currentBidPrice = 0;
            this._minimumBidPrice = 0;
        }

        public AuctionPost(Guid postId, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string mediaContent, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string itemLocation, string description, string title, List<InterestStatus> interestStatuses, string phoneNumber, List<Report> reports, double price, DateTime expirationDate, string deliveryType, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice, bool confirmed, int viewCount, bool onGoing) : base(postId, usersThatShared, usersThatLiked, comments, mediaContent, creationDate, authorId, groupId, promoted, usersThatFavorited, itemLocation, description, title, interestStatuses, phoneNumber, reports, price, expirationDate, deliveryType, reviews, reviewScore, buyerId, Constants.AUCTION_POST_TYPE, confirmed, viewCount)
        {
            this._currentPriceLeader = currentPriceLeader;
            this._currentBidPrice = currentBidPrice;
            this._minimumBidPrice = minimumBidPrice;
            this._onGoing = onGoing;
        }

        public bool OnGoing { get => _onGoing; set => _onGoing = value; }

        public double CurrentBidPrice { get => _currentBidPrice; set => _currentBidPrice = value; }

        public double MinimumBidPrice { get => _minimumBidPrice; set => _minimumBidPrice = value; }

        public Guid CurrentPriceLeader { get => _currentPriceLeader; set => _currentPriceLeader = value; }

        public void PlaceBid(Guid userId, double bidPrice)
        {
            if (bidPrice <= _minimumBidPrice)
            {
                throw new Exception("Bid price is lower than minimum bid price");
            }
            if (bidPrice > _currentBidPrice)
            {
                _currentBidPrice = bidPrice;
                _currentPriceLeader = userId;
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
