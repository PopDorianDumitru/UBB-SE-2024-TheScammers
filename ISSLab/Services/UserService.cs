using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ISSLab.Model;
using ISSLab.Model.Repositories;
namespace ISSLab.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _users;
        private IPostRepository _posts;

        public UserService(IUserRepository users, IPostRepository posts)
        {
            this._users = users;
            this._posts = posts;
        }

        public void AddUser(User user)
        {
            _users.AddUser(user);
        }

        public void RemoveUser(User user)
        {
            _users.DeleteUser(user.Id);
        }

        public User GetUserById(Guid id)
        {
            User? user = _users.GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public List<User> GetUsers()
        {
            return _users.GetAll();
        }

        public bool IsUserInGroup(Guid userId, Guid groupId)
        {
            User user = GetUserById(userId);
            return user.Groups.Contains(groupId);
        }

        public void UpdateUserUsername(Guid user, string username)
        {
            _users.UpdateUserUsername(user, username);
        }

        public void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating)
        {
            Review review = new Review(reviewerId, sellerId, groupId, content, date, rating);
            _users.AddReview(review);
        }

        public void AddPostToCart(Guid groupId, Guid postId, Guid userId)
        {
            _users.AddPostToCart(groupId, userId, postId);
        }

        public void RemovePostFromCart(Guid groupId, Guid postId, Guid userId)
        {
            _users.RemoveFromCart(groupId, userId, postId);
        }

        public void AddPostToFavorites(Guid groupId, Guid postId, Guid userId)
        {
            _users.AddToFavorites(groupId, userId, postId);
        }

        public void RemovePostFromFavorites(Guid groupId, Guid postId, Guid userId)
        {
            _users.RemoveFromFavorites(groupId, userId, postId);
        }

        public List<Post> GetFavoritePosts(Guid groupId, Guid userId)
        {
            List<Post> favoritePosts = new List<Post>();
            UsersFavoritePosts favorites = _users.GetById(userId).Favorites.Find(f => f.GroupId == groupId);
            if (favorites == null)
            {
                _users.GetById(userId).Favorites.Add(new UsersFavoritePosts(userId, groupId));
                return new List<Post>();
            }
            foreach (Guid postId in favorites.Posts)
            {
                favoritePosts.Add(_posts.GetPostById(postId));
            }
            return favoritePosts;
        }

        public List<Post> GetItemsFromCart(Guid userId, Guid groupId)
        {
            Cart cart = _users.GetById(userId).Carts.Find(c => c.GroupId == groupId);
            List<Post> cartedPosts = new List<Post>();
            if (cart == null)
            {
                _users.GetById(userId).Carts.Add(new Cart(groupId, userId));
                return new List<Post>();
            }
            foreach (Guid postId in cart.PostsSavedInCart)
            {
                cartedPosts.Add(_posts.GetPostById(postId));
            }
            return cartedPosts;
        }
    }
}
