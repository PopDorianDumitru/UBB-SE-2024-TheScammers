using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Group
    {
        private Guid _groupId;
        private string _name;
        private int _memberCount;
        private List<Guid> _members;
        private List<Guid> _posts;
        private List<Guid> _topSellers;
        private List<Guid> _admins;
        private List<Guid> _sellingUsers;
        private List<Guid> _usersRequestingToSell;

        private string _description;
        private string _type;
        private string _bannerPath;
        private DateTime _creationDate;

        public Group(string name, string description, string type, string bannerPath)
        {
            this._groupId = Guid.NewGuid();
            this._name = name;
            this._memberCount = 0;
            this._members = new List<Guid>();
            this._posts = new List<Guid>();
            this._admins = new List<Guid>();
            this._sellingUsers = new List<Guid>();
            this._description = description;
            this._type = type;
            this._bannerPath = bannerPath;
            this._creationDate = DateTime.Now;
            this._topSellers = new List<Guid>();
            this._usersRequestingToSell = new List<Guid>();


        }
        public Group()
        {
            this._groupId = Guid.NewGuid();
            this._name = "";
            this._memberCount = 0;
            this._members = new List<Guid>();
            this._posts = new List<Guid>();
            this._admins = new List<Guid>();
            this._sellingUsers = new List<Guid>();
            this._description = "";
            this._type = "";
            this._bannerPath = "";
            this._creationDate = DateTime.Now;
            this._topSellers = new List<Guid>();
            this._sellingUsers = new List<Guid>();
            this._usersRequestingToSell = new List<Guid>();
        }
        public Group(Guid id, string name, int memberCount, List<Guid> members, List<Guid> posts, List<Guid> admins, List<Guid> sellingUsers, string description, string type, string banner, DateTime creationDate, List<Guid> topSellers, List<Guid> usersRequestingToSell)

        {
            this._groupId = id;
            this._name = name;
            this._memberCount = memberCount;
            this._members = members;
            this._posts = posts;
            this._admins = admins;
            this._sellingUsers = sellingUsers;
            this._description = description;
            this._type = type;
            this._bannerPath = banner;
            this._creationDate = creationDate;
            this._topSellers = topSellers;
            this._usersRequestingToSell = usersRequestingToSell;

        }

        public List<Guid> UsersWithSellRequests { get => this._sellingUsers; set => this._sellingUsers = value; }
        public void AddUserWithSellRequest(Guid userID)
        {
            this._sellingUsers.Add(userID);
        }
        public void RemoveUserWithSellRequest(Guid userID)
        {
            this._sellingUsers.Remove(userID);
        }

        public List<Guid> TopSellers { get => this._topSellers; set => this._topSellers = value; }
        public void AddTopSeller(Guid userID)
        {
            this._topSellers.Add(userID);
        }
        public void RemoveTopSeller(Guid userID)
        {
            this._topSellers.Remove(userID);
        }


        public Guid GroupId { get => _groupId; }
        public string Name { get => _name; set => _name = value; }
        public int MemberCount { get => _memberCount; }
        public List<Guid> Members { get => _members; }
        public List<Guid> Posts { get => _posts; }
        public List<Guid> Admins { get => _admins; }
        public List<Guid> SellingUsers { get => _sellingUsers; }

        public List<Guid> UsersRequestingToSell { get => _usersRequestingToSell; }
        public string Description { get => _description; set => _description = value; }
        public string Type { get => _type; set => _type = value; }
        public string BannerPath { get => _bannerPath; set => _bannerPath = value; }
        public DateTime CreationDate { get => _creationDate; }

        public void AddMember(Guid user)
        {
            if (!_members.Contains(user))
            {
                _members.Add(user);
                _memberCount++;
            }
            else
                throw new Exception("User is already a member of this group");
        }
        public void RemoveMember(Guid user)
        {
            if (_members.Contains(user))
            {
                _members.Remove(user);
                _memberCount--;
            }
            else
                throw new Exception("User is not a member of this group");
        }
        public void AddPost(Guid post)
        {
            _posts.Add(post);
        }
        public void RemovePost(Guid post)
        {
            _posts.Remove(post);
        }
        public void AddAdmin(Guid user)
        {
            if (!_members.Contains(user))
                throw new Exception("User is not a member of this group");
            if (_admins.Contains(user))
                throw new Exception("User is already an admin of this group");
            _admins.Add(user);
        }
        public void RemoveAdmin(Guid user)
        {
            if (!_members.Contains(user))
                throw new Exception("User is not a member of this group");
            if (!_admins.Contains(user))
                throw new Exception("User is not an admin of this group");
            _admins.Remove(user);
        }
        public void AddSellingUser(Guid user)
        {
            if (!_members.Contains(user))
                throw new Exception("User is not a member of this group");
            if (_sellingUsers.Contains(user))
                throw new Exception("User is already a selling user of this group");
            _sellingUsers.Add(user);
        }
        public void RemoveSellingUser(Guid user)
        {
            if (!_members.Contains(user))
                throw new Exception("User is not a member of this group");
            if (!_sellingUsers.Contains(user))
                throw new Exception("User is not a selling user of this group");
            _sellingUsers.Remove(user);
        }

        public void AddRequestingToSellUser(Guid user)
        {
            if (!_members.Contains(user))
                throw new Exception("User is not a member of this group");
            if (_sellingUsers.Contains(user))
                throw new Exception("User is already a selling user of this group");
            _sellingUsers.Add(user);
        }
        public void RemoveRequestingToSellUser(Guid user)
        {
            if (!_usersRequestingToSell.Contains(user))
                throw new Exception("User is not a member of this group");
            if (!_usersRequestingToSell.Contains(user))
                throw new Exception("User is not a selling user of this group");
            _usersRequestingToSell.Remove(user);
        }

    }
}
