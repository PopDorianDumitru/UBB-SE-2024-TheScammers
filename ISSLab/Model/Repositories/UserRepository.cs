using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ISSLab.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _allUsers;

        public UserRepository()
        {
            _allUsers = new List<User>();
        }

        public List<User> GetAll()
        {
            return _allUsers;
        }

        public User GetById(Guid userId)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    return _allUsers[i];
                }
            }
            throw new Exception("User does not exist");
        }

        public void AddUser(User newUser)
        {
            _allUsers.Add(newUser);
        }

        public void UpdateUserUsername(Guid userId, string newUsername)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    _allUsers[i].Username = newUsername;
                    break;
                }
            }
        }

        public void UpdateUserDateOfBirth(Guid userId, DateOnly newDateOfBirth)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    _allUsers[i].DateOfBirth = newDateOfBirth;
                    break;
                }
            }
        }

        public void AddReview(Review review)
        {
            _allUsers.Find(user => user.Id == review.SellerId).AddReview(review);
        }

        public void UpdateUserProfilePicture(Guid userId, string newProfilePicture)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    _allUsers[i].ProfilePicture = newProfilePicture;
                    break;
                }
            }
        }

        public void UpdateUserPassword(Guid userId, string newPassword)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    _allUsers[i].Password = newPassword;
                    break;
                }
            }
        }

        public void UpdateUserNumberOfSells(Guid userId, int newNumberOfSells)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == userId)
                {
                    _allUsers[i].NrOfSells = newNumberOfSells;
                    break;
                }
            }
        }

        public void DeleteUser(Guid id)
        {
            for (int i = 0; i < _allUsers.Count; i++)
            {
                if (_allUsers[i].Id == id)
                {
                    _allUsers.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddPostToCart(Guid groupId, Guid userId, Guid postId)
        {
            User? user = _allUsers.Find(user => user.Id == userId);
            if (user == null)
                throw new Exception("No such user");

            Cart? cart = user.Carts.Find(c => c.GroupId == groupId);

            if (cart == null)
            {
                cart = new Cart(groupId, userId);
                user.Carts.Add(cart);
            }
            if (cart.PostsSavedInCart.Contains(postId))
                return;
            cart.PostsSavedInCart.Add(postId);
        }

        public void RemoveFromCart(Guid groupId, Guid userId, Guid postId)
        {
            _allUsers.Find(user => user.Id == userId).Carts.Find(cart => cart.GroupId == groupId).PostsSavedInCart.Remove(postId);
        }

        public void AddToFavorites(Guid groupId, Guid userId, Guid postId)
        {
            User? user = _allUsers.Find(user => user.Id == userId);

            if (user == null)
            {
                throw new Exception("No such user");
            }

            UsersFavoritePosts? favoriteFromGroup = user.Favorites.Find(c => c.GroupId == groupId);

            if (favoriteFromGroup == null)
            {
                favoriteFromGroup = new UsersFavoritePosts(userId, groupId);
                user.Favorites.Add(favoriteFromGroup);
            }
            if (favoriteFromGroup.Posts.Contains(postId))
                return;
            favoriteFromGroup.Posts.Add(postId);
        }

        public void RemoveFromFavorites(Guid groupId, Guid userId, Guid postId)
        {
            // bad. this is equivalent with RemoveFromCart
            _allUsers.Find(user => user.Id == userId).Carts.Find(cart => cart.GroupId == groupId).PostsSavedInCart.Remove(postId);
        }
    }
}
