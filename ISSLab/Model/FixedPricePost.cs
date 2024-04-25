using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class FixedPricePost : Post
    {
        private double _price;
        private DateTime _expirationDate;
        private float _reviewScore;
        private List<Review> _reviews;
        private string _delivery;
        private Guid _buyerId;

        public FixedPricePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, string type, bool confirmed) : base(media, authorId, groupId, location, description, title, contacts, type, confirmed)
        {
            this._price = price;
            this._expirationDate = expirationDate;
            this._reviews = reviews;
            this._delivery = delivery;
            this._reviewScore = reviewScore;
            this._buyerId = buyerId;
        }

        public FixedPricePost() : base()
        {
            this._price = 0;
            this._expirationDate = DateTime.Now;
            this._reviewScore = 0;
            this._reviews = new List<Review>();
            this._delivery = "";
            this._buyerId = Guid.Empty;
        }

        public FixedPricePost(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string media, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string location, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, string type, bool confirmed, int views) : base(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interestStatuses, contacts, reports, type, confirmed, views)
        {
            this._price = price;
            this._expirationDate = expirationDate;
            this._reviewScore = reviewScore;
            this._delivery = delivery;
            this._reviews = reviews;
            this._buyerId = buyerId;
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }
        public float ReviewScore
        {
            get { return _reviewScore; }
            set { _reviewScore = value; }
        }
        public string Delivery
        {
            get { return _delivery; }
            set { _delivery = value; }
        }
        public Guid BuyerId
        {
            get { return _buyerId; }
            set { _buyerId = value; }
        }
        public List<Review> Reviews
        {
            get { return _reviews; }
        }

        public void AddReview(Review review)
        {
            if (_reviews.Contains(review))
            {
                throw new Exception("Review already exists. Edit the existing one if you want");
            }
            _reviews.Add(review);
        }

        public void RemoveReview(Review review)
        {
            if (!_reviews.Contains(review))
            {
                throw new Exception("Review does not exist");
            }
            _reviews.Remove(review);
        }

        public void BuyProduct(Guid buyerId)
        {
            if (this._buyerId != Guid.Empty)
            {
                throw new Exception("Product already bought");
            }
            this._buyerId = buyerId;
        }
    }
}
