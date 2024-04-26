using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Review
    {
        private Guid _reviewId;
        private Guid _reviewerId;
        private Guid _sellerId;
        private Guid _groupId;
        private string _content;
        private DateTime _dateOfReview;
        private int _rating;

        public Review(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime dateOfReview, int rating)
        {
            this._reviewId = Guid.NewGuid();
            this._reviewerId = reviewerId;
            this._groupId = groupId;
            this._sellerId = sellerId;
            this._content = content;
            this._dateOfReview = dateOfReview;
            this._rating = rating;
        }

        public Review(Guid id, Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime dateOfReview, int rating)
        {
            this._reviewId = id;
            this._reviewerId = reviewerId;
            this._sellerId = sellerId;
            this._groupId = groupId;
            this._content = content;
            this._dateOfReview = dateOfReview;
            this._rating = rating;
        }
        public Review()
        {
            this._reviewId = Guid.NewGuid();
            this._groupId = Guid.NewGuid();
            this._reviewerId = Guid.NewGuid();
            this._sellerId = Guid.NewGuid();
            this._content = Constants.EMPTY_STRING;
            this._dateOfReview = DateTime.Now;
            this._rating = 0;
        }

        public Guid Id { get => _reviewId; }
        public Guid GroupId { get => _groupId; set => _groupId = value; }
        public Guid SellerId { get => _sellerId; set => _sellerId = value; }
        public Guid ReviewerId { get => _reviewerId; set => _reviewerId = value; }
        public string Content { get => _content; set => _content = value; }
        public DateTime DateOfReview { get => _dateOfReview; set => _dateOfReview = value; }
        public int Rating { get => _rating; set => _rating = value; }
    }
}
