using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ISSLab.Services;

namespace ISSLab.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> allUsers;

        public UserRepository()
        {
            allUsers = new List<User>();
        }

        public List<User> GetAll()
        {
            return allUsers;
        }

        public User GetById(Guid userId)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    return allUsers[i];
                }
            }
            throw new Exception("User does not exist");
        }

        public void AddUser(User newUser)
        {
            allUsers.Add(newUser);
        }

        public void UpdateUserUsername(Guid userId, string newUsername)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    allUsers[i].Username = newUsername;
                    break;
                }
            }
        }

        public void UpdateUserDateOfBirth(Guid userId, DateOnly newDateOfBirth)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    allUsers[i].DateOfBirth = newDateOfBirth;
                    break;
                }
            }
        }

        public void AddReview(Review review)
        {
            allUsers.Find(user => user.Id == review.SellerId).AddReview(review);
        }

        public void UpdateUserProfilePicture(Guid userId, string newProfilePicture)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    allUsers[i].ProfilePicture = newProfilePicture;
                    break;
                }
            }
        }

        public void UpdateUserPassword(Guid userId, string newPassword)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    allUsers[i].Password = newPassword;
                    break;
                }
            }
        }

        public void UpdateUserNumberOfSells(Guid userId, int newNumberOfSells)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == userId)
                {
                    allUsers[i].NumberOfSales = newNumberOfSells;
                    break;
                }
            }
        }

        public void DeleteUser(Guid id)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].Id == id)
                {
                    allUsers.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddPostToCart(Guid groupId, Guid userId, Guid postId)
        {
            User? user = allUsers.Find(user => user.Id == userId);
            if (user == null)
            {
                throw new Exception("No such user");
            }

            Cart? cart = user.Carts.Find(c => c.GroupId == groupId);

            if (cart == null)
            {
                cart = new Cart(groupId, userId);
                user.Carts.Add(cart);
            }
            if (cart.PostsSavedInCart.Contains(postId))
            {
                return;
            }
            cart.PostsSavedInCart.Add(postId);
        }

        public void RemoveFromCart(Guid groupId, Guid userId, Guid postId)
        {
            allUsers.Find(user => user.Id == userId).Carts.Find(cart => cart.GroupId == groupId).PostsSavedInCart.Remove(postId);
        }

        public void AddToFavorites(Guid groupId, Guid userId, Guid postId)
        {
            User? user = allUsers.Find(user => user.Id == userId);

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
            {
                return;
            }
            favoriteFromGroup.Posts.Add(postId);
        }

        public void RemoveFromFavorites(Guid groupId, Guid userId, Guid postId)
        {
            // bad. this is equivalent with RemoveFromCart
            allUsers.Find(user => user.Id == userId).Carts.Find(cart => cart.GroupId == groupId).PostsSavedInCart.Remove(postId);
        }
    }
}
