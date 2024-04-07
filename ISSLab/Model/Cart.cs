using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class Cart
    {
        private Guid groupId;
        private Guid userId;
        private List<FixedPricePost> posts;

        public Cart(Guid groupId, Guid userId, List<FixedPricePost> posts)
        {
            this.groupId = groupId;
            this.userId = userId;
            this.posts = posts;
        }
        public Cart()
        {
            this.groupId = Guid.NewGuid();
            this.userId = Guid.NewGuid();
            this.posts = new List<FixedPricePost>();


        }
        public Cart(Guid groupId, Guid userId)
        {
            this.groupId = groupId;
            this.userId = userId;
            this.posts = new List<FixedPricePost>();
        }

        public Guid GroupId { get => groupId; }
        public Guid UserId { get => userId; }
        public List<FixedPricePost> Posts { get => posts; }

        void addPostToCart(FixedPricePost post)
        {
            if (this.posts.Contains(post))
                throw new Exception("Post already in cart");
            posts.Add(post);
        }
        void removePostFromCart(FixedPricePost post)
        {
            if (!this.posts.Contains(post))
                throw new Exception("Post not in cart");
            posts.Remove(post);
        }


    }
}
