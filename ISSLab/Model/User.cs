using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class User
    {
        private Guid id;
        private string username;
        private string realName;
        private DateOnly dateOfBirth;
        private string profilePicture;
        private string password;
        private int nrOfSells;
        private DateTime creationDate;
        private List<Guid> groupsWithSellingPrivelage;
        private List<SellingUserScore> userScores;
        private List<Guid> groupsWithActiveRequestToSell;
        private List<Cart> carts;
        private List<Favorites> favorites;
        private List<Group> groups;
        private List<Review> reviews;





        public User(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password)
        {
            this.id = Guid.NewGuid();
            this.username = username;
            this.realName = realName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            this.password = password;
            this.creationDate = DateTime.Now;
            this.groupsWithSellingPrivelage = new List<Guid>();
            this.groupsWithActiveRequestToSell = new List<Guid>();
            this.userScores = new List<SellingUserScore>();
            this.nrOfSells = 0;
        }
        public User(Guid id, string username, string realName, DateOnly dateOfBirth, string profilePicture, string password, DateTime creationDate, List<Guid> groupsWithSellingPrivelage, List<Guid> groupsWithActiveRequestToSell,List<SellingUserScore> userScores, int nrOfSells)
        {
            this.id = id;
            this.username = username;
            this.realName = realName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            this.password = password;
            this.creationDate = creationDate;
            this.groupsWithSellingPrivelage = groupsWithSellingPrivelage;
            this.groupsWithActiveRequestToSell = groupsWithActiveRequestToSell;
            this.userScores = userScores;
            this.nrOfSells = nrOfSells;
        }

        public User()
        {
            this.id = Guid.NewGuid();
            this.username = "";
            this.realName = "";
            this.dateOfBirth = new DateOnly();
            this.profilePicture = "";
            this.password = "";
            this.creationDate = DateTime.Now;
            this.groupsWithSellingPrivelage = new List<Guid>();
            this.groupsWithActiveRequestToSell = new List<Guid>();
            this.userScores = new List<SellingUserScore>();
            this.nrOfSells = 0;
        }

        public List<SellingUserScore> sellingUserScores { get => userScores; set => userScores = value; }
        public Guid Id { get => id; }
        public string Username { get => username; set => username = value; }
        public string RealName { get => realName; set => realName = value; }
        public DateOnly DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string ProfilePicture { get => profilePicture; set => profilePicture = value; }
        public string Password { get => password; set => password = value; }
        public DateTime CreationDate { get => creationDate; }


        public List<Guid> GroupsWithSellingPrivelage { get => groupsWithSellingPrivelage; }

        public List<Guid> GroupsWithActiveRequestToSell { get => groupsWithSellingPrivelage;}



        public void addNewUserScore( SellingUserScore userScor)
        {
            this.userScores.Add(userScor);
        }


        public int NrOfSells { get => nrOfSells; set => nrOfSells = value; }

        public void removeUserScore(SellingUserScore userScore)
        {
            this.userScores = this.userScores.FindAll(val => val.GroupId != userScore.GroupId);
        }  

        public void requestSellingAccess(Guid groupId)
        {
            if(groupsWithActiveRequestToSell.Contains(groupId))
                throw new Exception("Already requested access to sell in this group");
            if(groupsWithSellingPrivelage.Contains(groupId))
                throw new Exception("Already have access to sell in this group");
            groupsWithActiveRequestToSell.Append(groupId);
        }
        public void accessToSellDenied(Guid groupId)
        {
            if(!groupsWithActiveRequestToSell.Contains(groupId))
                throw new Exception("No active request to sell in this group");
            if(groupsWithSellingPrivelage.Contains(groupId))
                throw new Exception("Already have access to sell in this group");
            groupsWithActiveRequestToSell = groupsWithActiveRequestToSell.FindAll(val => val != groupId);
        }
        public void accessToSellWasTaken(Guid groupId)
        {
            if(!groupsWithSellingPrivelage.Contains(groupId))
                throw new Exception("No access to sell in this group");
            if(groupsWithActiveRequestToSell.Contains(groupId))
                throw new Exception("No access to sell in this group yet, but request is active");
            groupsWithSellingPrivelage = groupsWithSellingPrivelage.FindAll(val => val != groupId);
        }


        public void receivedPrivelageToSell(Guid groupId)
        {
            if (groupsWithSellingPrivelage.Contains(groupId))
                throw new Exception("You can already sell in this group");
            groupsWithActiveRequestToSell = groupsWithActiveRequestToSell.FindAll(val => val != groupId);
            groupsWithSellingPrivelage.Add(groupId);
        }

        public bool hasAccessToSell(Guid groupId)
        {
            if(!groupsWithSellingPrivelage.Contains(groupId))
                return false;
            return true;
        }

    }
}
