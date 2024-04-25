using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Review
    {
        private Guid id;
        private Guid reviewerId;
        private Guid sellerId;
        private Guid groupId;
        private string content;
        private DateTime dateOfReview;
        private int rating;

        public Review(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime dateOfReview, int rating)
        {
            this.id = new Guid(reviewerId.ToString() + sellerId.ToString() + groupId.ToString());
            this.reviewerId = reviewerId;
            this.groupId = groupId;
            this.sellerId = sellerId;
            this.content = content;
            this.dateOfReview = dateOfReview;
            this.rating = rating;
        }

        public Review(Guid id, Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime dateOfReview, int rating)
        {
            this.id = id;
            this.reviewerId = reviewerId;
            this.sellerId = sellerId;
            this.groupId = groupId;
            this.content = content;
            this.dateOfReview = dateOfReview;
            this.rating = rating;

        }

        public Review()
        {
            this.id = Guid.NewGuid();
            this.groupId = Guid.NewGuid();
            this.reviewerId = Guid.NewGuid();
            this.sellerId = Guid.NewGuid();
            this.content = "";
            this.dateOfReview = DateTime.Now;
            this.rating = 0;
        }

        public Guid Id { get => id; }
        public Guid GroupId { get => groupId; set => groupId = value; }
        public Guid SellerId { get => sellerId; set => sellerId = value; }
        public Guid ReviewerId { get => reviewerId; set => reviewerId = value; }
        public string Content { get => content; set => content = value; }
        public DateTime DateOfReview { get => dateOfReview; set => dateOfReview = value; }
        public int Rating { get => rating; set => rating = value; }
    }
}
