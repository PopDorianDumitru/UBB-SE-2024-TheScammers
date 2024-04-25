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
        private IUserRepository users;
        private IPostRepository posts;
        private IGroupRepository groups;

        public UserService(IUserRepository users, IPostRepository posts, IGroupRepository groups)
        {
            this.users = users;
            this.posts = posts;
            this.groups = groups;
        }

        public void AddUser(User user)
        {
            users.AddUser(user);
        }
        public void RemoveUser(User user)
        {
            users.DeleteUser(user.Id);
        }
        public User GetUserById(Guid id)
        {
            User? user = users.GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        public List<User> GetUsers()
        {
            return users.GetAll();
        }

        public bool IsUserInGroup(Guid userId, Guid groupId)
        {
            User user = GetUserById(userId);
            return user.Groups.Contains(groupId);
        }

        public void AddUserToGroup(User user, Group group)
        {
            user.Groups.Add(group.Id);
        }

        public bool UserIsSellerInGroup(Guid userId, Guid groupId)
        {
            Group group = groups.FindById(groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group.SellingUsers.Contains(userId);
        }

        public bool UserIsMemberInGroup(User userId, Guid groupId)
        {
            Group group = groups.FindById(groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group.Members.Contains(userId.Id);
        }

        public void UpdateUserUsername(Guid user, string username)
        {
            users.UpdateUserUsername(user, username);

        }

        public void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating)
        {
            Review review = new Review(reviewerId, sellerId, groupId, content, date, rating);
            users.AddReview(review);
        }

        public void AddItemToCart(Guid groupId, Guid postId, Guid userId)
        {
            users.AddPostToCart(groupId, userId, postId);

        }
        public void RemoveFromCart(Guid groupId, Guid postId, Guid userId)
        {
            users.RemoveFromCart(groupId, userId, postId);
        }

        public void AddItemToFavorites(Guid groupId, Guid postId, Guid userId)
        {
            users.AddToFavorites(groupId, userId, postId);
        }
        public void RemoveFromFavorites(Guid groupId, Guid postId, Guid userId)
        {
            users.RemoveFromFavorites(groupId, userId, postId);
        }

        public List<Post> GetFavoritePosts(Guid groupId, Guid userId)
        {
            List<Post> favoritePosts = new List<Post>();
            UsersFavoritePosts favorites = users.GetById(userId).Favorites.Find(f => f.GroupId == groupId);
            if (favorites == null)
            {
                users.GetById(userId).Favorites.Add(new UsersFavoritePosts(userId, groupId));
                return new List<Post>();
            }
            foreach (Guid postId in favorites.Posts)
            {
                favoritePosts.Add(posts.GetPostById(postId));
            }
            return favoritePosts;
        }

        public List<Post> GetItemsFromCart(Guid userId, Guid groupId)
        {
            Cart cart = users.GetById(userId).Carts.Find(c => c.GroupId == groupId);
            List<Post> cartedPosts = new List<Post>();
            if (cart == null)
            {
                users.GetById(userId).Carts.Add(new Cart(groupId, userId));
                return new List<Post>();
            }
            foreach (Guid postId in cart.PostsSavedInCart)
            {
                cartedPosts.Add(posts.GetPostById(postId));
            }
            return cartedPosts;
        }
    }
}
