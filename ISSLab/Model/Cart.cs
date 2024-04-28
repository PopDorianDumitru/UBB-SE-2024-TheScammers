using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Cart
    {
        private Guid _groupId;
        private Guid _userId;
        private List<Guid> _postsSavedInCart;

        public Cart(Guid groupId, Guid userId, List<Guid> postsSavedInCart)
        {
            this._groupId = groupId;
            this._userId = userId;
            this._postsSavedInCart = postsSavedInCart;
        }

        public Cart()
        {
            this._groupId = Guid.NewGuid();
            this._userId = Guid.NewGuid();
            this._postsSavedInCart = new List<Guid>();
        }

        public Cart(Guid groupId, Guid userId)
        {
            this._groupId = groupId;
            this._userId = userId;
            this._postsSavedInCart = new List<Guid>();
        }

        public Guid GroupId { get => _groupId; }
        public Guid UserId { get => _userId; }
        public List<Guid> PostsSavedInCart { get => _postsSavedInCart; }

        public void AddPostToCart(Guid postToSave)
        {
            if (this._postsSavedInCart.Contains(postToSave))
                throw new Exception("Post already in cart");
            _postsSavedInCart.Add(postToSave);
        }

        public void RemovePostFromCart(Guid postToSave)
        {
            if (!this._postsSavedInCart.Contains(postToSave))
                throw new Exception("Post not in cart");
            _postsSavedInCart.Remove(postToSave);
        }
    }
}
