using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class Group
    {
        private Guid id;
        private string name;
        private int memberCount;
        private List<User> members;
        private List<Post> posts;
        private List<Guid> admins;
        private List<Guid> sellingUsers;
        private string description;
        private string type;
        private string banner;
        private DateTime creationDate;

        public Group(string name, string description, string type, string banner)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.memberCount = 0;
            this.members = new List<User>();
            this.posts = new List<Post>();
            this.admins = new List<Guid>();
            this.sellingUsers = new List<Guid>();
            this.description = description;
            this.type = type;
            this.banner = banner;
            this.creationDate = DateTime.Now;
        }
        public Group()
        {
            this.id = Guid.NewGuid();
            this.name = "";
            this.memberCount = 0;
            this.members = new List<User>();
            this.posts = new List<Post>();
            this.admins = new List<Guid>();
            this.sellingUsers = new List<Guid>();
            this.description = "";
            this.type = "";
            this.banner = "";
            this.creationDate = DateTime.Now;
        }
        public Group(Guid id, string name, int memberCount, List<User> members, List<Post> posts, List<Guid> admins, List<Guid> sellingUsers, string description, string type, string banner, DateTime creationDate)
        {
            this.id = id;
            this.name = name;
            this.memberCount = memberCount;
            this.members = members;
            this.posts = posts;
            this.admins = admins;
            this.sellingUsers = sellingUsers;
            this.description = description;
            this.type = type;
            this.banner = banner;
            this.creationDate = creationDate;
        }

        public Guid Id { get => id; }
        public string Name { get => name; set => name = value; }
        public int MemberCount { get => memberCount; }
        public List<User> Members { get => members; }
        public List<Post> Posts { get => posts; }
        public List<Guid> Admins { get => admins; }
        public List<Guid> SellingUsers { get => sellingUsers; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public string Banner { get => banner; set => banner = value; }
        public DateTime CreationDate { get => creationDate; }

        public void AddMember(User user)
        {
            if(!members.Contains(user))
            {
                members.Add(user);
                memberCount++;
            }
            else
                throw new Exception("User is already a member of this group");
        }
        public void RemoveMember(User user)
        {
            if(members.Contains(user))
            {
                members.Remove(user);
                memberCount--;
            }
            else
                throw new Exception("User is not a member of this group");
        }
        public void AddPost(Post post)
        {
            posts.Add(post);
        }
        public void RemovePost(Post post)
        {
            posts.Remove(post);
        }
        public void AddAdmin(User user)
        {
            if(!members.Contains(user))
                throw new Exception("User is not a member of this group");
            if(admins.Contains(user.Id))
                throw new Exception("User is already an admin of this group");
            admins.Add(user.Id);
        }
        public void removeAdmin(User user)
        {
            if(!members.Contains(user))
                throw new Exception("User is not a member of this group");
            if(!admins.Contains(user.Id))
                throw new Exception("User is not an admin of this group");
            admins.Remove(user.Id);
        }
        public void AddSellingUser(User user)
        {
            if(!members.Contains(user))
                throw new Exception("User is not a member of this group");
            if(sellingUsers.Contains(user.Id))
                throw new Exception("User is already a selling user of this group");
            sellingUsers.Add(user.Id);
        }
        public void RemoveSellingUser(User user)
        {
            if(!members.Contains(user))
                throw new Exception("User is not a member of this group");
            if(!sellingUsers.Contains(user.Id))
                throw new Exception("User is not a selling user of this group");
            sellingUsers.Remove(user.Id);
        }

    }
}
