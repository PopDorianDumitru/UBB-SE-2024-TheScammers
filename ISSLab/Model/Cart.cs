using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Cart
    {
        private Guid groupId;
        private Guid userId;
        private List<Guid> postsSavedInCart;

        public Cart(Guid groupId, Guid userId, List<Guid> postsSavedInCart)
        {
            this.groupId = groupId;
            this.userId = userId;
            this.postsSavedInCart = postsSavedInCart;
        }
        public Cart()
        {
            this.groupId = Guid.NewGuid();
            this.userId = Guid.NewGuid();
            this.postsSavedInCart = new List<Guid>();


        }
        public Cart(Guid groupId, Guid userId)
        {
            this.groupId = groupId;
            this.userId = userId;
            this.postsSavedInCart = new List<Guid>();
        }

        public Guid GroupId { get => groupId; }
        public Guid UserId { get => userId; }
        public List<Guid> PostsSavedInCart { get => postsSavedInCart; }

        public void AddPostToCart(Guid postToSave)
        {
            if (this.postsSavedInCart.Contains(postToSave))
                throw new Exception("Post already in cart");
            postsSavedInCart.Add(postToSave);
        }
        public void RemovePostFromCart(Guid postToSave)
        {
            if (!this.postsSavedInCart.Contains(postToSave))
                throw new Exception("Post not in cart");
            postsSavedInCart.Remove(postToSave);
        }


    }
}
