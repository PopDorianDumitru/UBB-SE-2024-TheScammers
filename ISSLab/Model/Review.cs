using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class Review
    {
        private Guid id;
        private Guid userId;
        private Guid postId;
        private string content;
        private DateTime date;
        private int rating;

        public Review(Guid userId, Guid postId, string content, DateTime date, int rating)
        {
            this.id = new Guid(userId.ToString() + postId.ToString());
            this.userId = userId;
            this.postId = postId;
            this.content = content;
            this.date = date;
            this.rating = rating;
        }

        public Review(Guid id, Guid userId, Guid postId, string content, DateTime date, int rating)
        {
            this.id = id;
            this.userId = userId;
            this.postId = postId;
            this.content = content;
            this.date = date;
            this.rating = rating;

        }

        public Review()
        {
            this.id = Guid.NewGuid();
            this.userId = Guid.NewGuid();
            this.postId = Guid.NewGuid();
            this.content = "";
            this.date = DateTime.Now;
            this.rating = 0;
        }

        public Guid Id { get => id; }
        public Guid UserId { get => userId; }
        public Guid PostId { get => postId; }
        public string Content { get => content; set => content = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Rating { get => rating; set => rating = value; }
    }
}
