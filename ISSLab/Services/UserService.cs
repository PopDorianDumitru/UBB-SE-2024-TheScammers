﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.Model.Repositories;
namespace ISSLab.Services
{
    class UserService
    {
        private UserRepository users;
        private PostRepository posts;
        private List<Group> groups;
        public UserService()
        {
            users = new UserRepository();
            posts = new PostRepository();
            groups = new List<Group>();
        }
        public UserService(UserRepository users, PostRepository posts, List<Group> groups)
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
            User? user =  users.findById(id);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
        public List<User> GetUsers()
        {
            return users.findAllUsers();
        }

        public User CreateUser(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password)
        {
            User user = new User(username, realName, dateOfBirth, profilePicture, password);
            return user;
        }



        
    

    }
}
