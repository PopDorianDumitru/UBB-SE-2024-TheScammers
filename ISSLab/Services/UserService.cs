using System;
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
            Group group = groups.Find(g=>g.Id == groupId);
            if(group == null)
            {
                throw new Exception("Group not found");
            }
            return group.Admins.Contains(userId);
        }
        public bool UserIsSellerInGroup(Guid userId, Guid groupId)
        {
            Group group = groups.Find(g => g.Id == groupId);
            if(group == null)
            {
                throw new Exception("Group not found");
            }
            return group.SellingUsers.Contains(userId);
        }

        public bool UserIsMemberInGroup(User userId, Guid groupId)
        {
            Group group = groups.Find(g => g.Id == groupId);
            if(group == null)
            {
                throw new Exception("Group not found");
            }
            return group.Members.Contains(userId);
        }

        public void UpdateUserUsername(Guid user, string username)
        {
            users.updateUserUsername(user, username);
           
        }

        public void UpdateUserRealName(Guid user, string realName)
        {
            users.updateUserRealName(user, realName);
        }

        public void UpdateUserDateOfBirth(Guid user, DateOnly dateOfBirth)
        {
            users.updateUserDateOfBirth(user, dateOfBirth);
        }

        public void UpdateUserProfilePicture(Guid user, string profilePicture)
        {
            users.updateUserProfilePicture(user, profilePicture);
        }

        public void UpdateUserPassword(Guid user, string password)
        {
            users.updateUserPassword(user, password);
        }
       
        public void RequestAccessToSell(Guid userId, Guid GroupId)
        {
            User usr = users.findById(userId);
            if(usr == null)
            {
                throw new Exception("User not found");
            }
            if(!UserIsMemberInGroup(usr, GroupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if(UserIsSellerInGroup(userId, GroupId))
            {
                throw new Exception("User is already a seller in the group");
            }
            users.updateGroupsWithSellingRequest(userId, GroupId);
        }
        public void DenyAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.findById(userId);
            if(usr == null)
            {
                throw new Exception("User not found");
            }
            if(!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if(!UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is not a seller in the group");
            }
            users.updateGroupsWithRemovingSellingRequest(userId, groupId);
        }

        public void AcceptAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.findById(userId);
            if(usr == null)
            {
                throw new Exception("User not found");
            }
            if(!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if(UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is already a seller in the group");
            }
            users.updateGroupsWithRemovingSellingRequest(userId, groupId);
            users.updateGroupsWithSellingPrivelage(userId, groupId);
        }

        public void RemoveAccessToSell(Guid userId, Guid groupId)
        {
            User usr = users.findById(userId);
            if(usr == null)
            {
                throw new Exception("User not found");
            }
            if(!UserIsMemberInGroup(usr, groupId))
            {
                throw new Exception("User is not a member of the group");
            }
            if(!UserIsSellerInGroup(userId, groupId))
            {
                throw new Exception("User is not a seller in the group");
            }
            users.updateGroupsWithRemovingSellingPrivelage(userId, groupId);
        }

        public void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating) 
        {
            Review review = new Review(reviewerId ,sellerId, groupId, content,date, rating);
            users.AddReview(review);
        }

 


    }
}
