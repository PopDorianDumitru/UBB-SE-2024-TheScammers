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
            User? user = users.FindById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        public List<User> GetUsers()
        {
            return users.FindAllUsers();
        }

        public User CreateUser(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password)
        {
            User user = new User(username, realName, dateOfBirth, profilePicture, password);
            return user;
        }



        public bool IsUserInGroup(User user, Group group)
        {
            return user.Groups.Contains(group.Id);
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

        public bool UserIsAdminInGroup(Guid userId, Guid groupId)
        {
            Group group = groups.FindById(groupId);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            return group.Admins.Contains(userId);
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

        public void UpdateUserRealName(Guid user, string realName)
        {
            users.UpdateUserRealName(user, realName);
        }

        public void UpdateUserDateOfBirth(Guid user, DateOnly dateOfBirth)
        {
            users.UpdateUserDateOfBirth(user, dateOfBirth);
        }

        public void UpdateUserProfilePicture(Guid user, string profilePicture)
        {
            users.UpdateUserProfilePicture(user, profilePicture);
        }

        public void UpdateUserPassword(Guid user, string password)
        {
            users.UpdateUserPassword(user, password);
        }

        public void RequestAccessToSell(Guid userId, Guid GroupId)
        {
            User usr = users.FindById(userId);
            if (usr == null)
            {
                throw new Exception("User not found");
            }
            if (!UserIsMemberInGroup(usr, GroupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if (UserIsSellerInGroup(userId, GroupId))
            {
                throw new Exception("User is already a seller in the group");
            }
            users.UpdateGroupsWithSellingRequest(userId, GroupId);
        }
        public void DenyAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.FindById(userId);
            if (usr == null)
            {
                throw new Exception("User not found");
            }
            if (!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if (!UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is not a seller in the group");
            }
            users.UpdateGroupsWithRemovingSellingRequest(userId, groupId);
        }

        public void AcceptAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.FindById(userId);
            if (usr == null)
            {
                throw new Exception("User not found");
            }
            if (!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if (UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is already a seller in the group");
            }
            users.UpdateGroupsWithRemovingSellingRequest(userId, groupId);
            users.UpdateGroupsWithSellingPrivelage(userId, groupId);
        }

        public void RemoveAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.FindById(userId);
            if (usr == null)
            {
                throw new Exception("User not found");
            }
            if (!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if (!UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is not a seller in the group");
            }
            users.UpdateGroupsWithRemovingSellingPrivelage(userId, groupId);
        }

        public void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating)
        {
            Review review = new Review(reviewerId, sellerId, groupId, content, date, rating);
            users.AddReview(review);
        }

        public void AddItemToCart(Guid groupId, Guid postId, Guid userId)
        {
            users.AddToCart(groupId, userId, postId);

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
            UsersFavoritePosts favorites = users.FindById(userId).Favorites.Find(f => f.GroupId == groupId);
            if (favorites == null)
            {
                users.FindById(userId).Favorites.Add(new UsersFavoritePosts(userId, groupId));
                return new List<Post>();
            }
            foreach (Guid postId in favorites.Posts)
            {
                favoritePosts.Add(posts.GetPostById(postId));
            }
            return favoritePosts;
        }

        public double GetUserScore(Guid userId, Guid groupId)
        {
            List<Review> reviews = users.FindById(userId).Reviews.FindAll(r => r.GroupId == groupId);
            double score = 0;
            foreach (Review review in reviews)
            {
                score = score + review.Rating;
            }
            return score / reviews.Count;
        }

        public List<Post> GetItemsFromCart(Guid userId, Guid groupId)
        {
            Cart cart = users.FindById(userId).Carts.Find(c => c.GroupId == groupId);
            List<Post> cartedPosts = new List<Post>();
            if (cart == null)
            {
                users.FindById(userId).Carts.Add(new Cart(groupId, userId));
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
