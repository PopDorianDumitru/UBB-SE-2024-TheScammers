using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
namespace ISSLab.Services
{
    class UserService
    {
        private List<User> users;
        public UserService()
        {
            users = new List<User>();
        }
        public UserService(List<User> users)
        {
            this.users = users;
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }
        public void RemoveUser(User user)
        {
            users.Remove(user);
        }
        public User GetUserById(Guid id)
        {
            User? user =  users.Find(user => user.Id == id);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        public List<User> GetUsers()
        {
            return users;
        }

        public User CreateUser(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password)
        {
            User user = new User(username, realName, dateOfBirth, profilePicture, password);
            return user;
        }
    

    }
}
