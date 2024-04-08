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
            return users.Find(user => user.Id == id);
        }
        public List<User> GetUsers()
        {
            return users;
        }
    

    }
}
