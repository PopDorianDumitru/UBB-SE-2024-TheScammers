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
        private IUserRepository userRepository;
        private IPostRepository postRepository;

        public UserService(IUserRepository users, IPostRepository posts)
        {
            this.userRepository = users;
            this.postRepository = posts;
        }

        public void AddUser(User user)
        {
            userRepository.AddUser(user);
        }

        public void RemoveUser(User user)
        {
            userRepository.DeleteUser(user.Id);
        }

        public User GetUserById(Guid id)
        {
            User? user = userRepository.GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public List<User> GetUsers()
        {
            return userRepository.GetAll();
        }

        public bool IsUserInGroup(Guid userId, Guid groupId)
        {
            User user = GetUserById(userId);
            return user.Groups.Contains(groupId);
        }

        public void UpdateUserUsername(Guid user, string username)
        {
            userRepository.UpdateUserUsername(user, username);
        }

        public void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating)
        {
            Review review = new Review(reviewerId, sellerId, groupId, content, date, rating);
            userRepository.AddReview(review);
        }

        public void AddPostToCart(Guid groupId, Guid postId, Guid userId)
        {
            userRepository.AddPostToCart(groupId, userId, postId);
        }

        public void RemovePostFromCart(Guid groupId, Guid postId, Guid userId)
        {
            userRepository.RemoveFromCart(groupId, userId, postId);
        }

        public void AddPostToFavorites(Guid groupId, Guid postId, Guid userId)
        {
            userRepository.AddToFavorites(groupId, userId, postId);
        }

        public void RemovePostFromFavorites(Guid groupId, Guid postId, Guid userId)
        {
            userRepository.RemoveFromFavorites(groupId, userId, postId);
        }

        public List<Post> GetFavoritePosts(Guid groupId, Guid userId)
        {
            List<Post> favoritePosts = new List<Post>();
            UsersFavoritePosts favorites = userRepository.GetById(userId).Favorites.Find(checkedFavorite => checkedFavorite.GroupId == groupId);
            if (favorites == null)
            {
                userRepository.GetById(userId).Favorites.Add(new UsersFavoritePosts(userId, groupId));
                return new List<Post>();
            }
            foreach (Guid postId in favorites.Posts)
            {
                favoritePosts.Add(postRepository.GetPostById(postId));
            }
            return favoritePosts;
        }

        public List<Post> GetPostsFromCart(Guid userId, Guid groupId)
        {
            Cart cart = userRepository.GetById(userId).Carts.Find(checkedCart => checkedCart.GroupId == groupId);
            List<Post> cartedPosts = new List<Post>();
            if (cart == null)
            {
                userRepository.GetById(userId).Carts.Add(new Cart(groupId, userId));
                return new List<Post>();
            }
            foreach (Guid postId in cart.PostsSavedInCart)
            {
                cartedPosts.Add(postRepository.GetPostById(postId));
            }
            return cartedPosts;
        }
    }
}
