using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ISSLab.Model
{
    public class User
    {
        private Guid id;
        private string username;
        private string realName;
        private DateOnly dateOfBirth;
        private string profilePicture;
        private string password;
        private int numberOfSales;
        private DateTime creationDate;
        private List<Guid> groupsWithSellingPrivilege;
        private List<SellingUserScore> userScores;
        private List<Guid> groupsWithActiveRequestToSell;
        private List<Cart> carts;
        private List<UsersFavoritePosts> favorites;
        private List<Guid> groups;
        private List<Review> receivedReviews;






        public User(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password)
        {
            this.id = Guid.NewGuid();
            this.username = username;
            this.realName = realName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            this.password = password;
            this.creationDate = DateTime.Now;
            this.groupsWithSellingPrivilege = new List<Guid>();
            this.groupsWithActiveRequestToSell = new List<Guid>();
            this.userScores = new List<SellingUserScore>();
            this.carts = new List<Cart>();
            this.favorites = new List<UsersFavoritePosts>();
            this.groups = new List<Guid>();
            this.receivedReviews = new List<Review>();
            this.numberOfSales = 0;
        }
        public User(Guid id, string username, string realName, DateOnly dateOfBirth, string profilePicture, string password, DateTime creationDate, List<Guid> groupsWithSellingPrivelage, List<Guid> groupsWithActiveRequestToSell,List<SellingUserScore> userScores, List<Cart> carts, List<UsersFavoritePosts> favorites, List<Guid> groups, List<Review> receivedReviews, int nrOfSells)
        {
            this.id = id;
            this.username = username;
            this.realName = realName;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            this.password = password;
            this.creationDate = creationDate;
            this.groupsWithSellingPrivilege = groupsWithSellingPrivelage;
            this.groupsWithActiveRequestToSell = groupsWithActiveRequestToSell;
            this.userScores = userScores;
            this.receivedReviews = receivedReviews;
            this.carts = carts;
            this.favorites = favorites;
            this.groups = groups;
            this.numberOfSales = nrOfSells;
            this.carts = carts;
            this.favorites = favorites;
            this.groups = groups;
            this.receivedReviews = receivedReviews;
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
            this.groupsWithSellingPrivilege = new List<Guid>();
            this.groupsWithActiveRequestToSell = new List<Guid>();
            this.userScores = new List<SellingUserScore>();
            this.numberOfSales = 0;
            this.carts = new List<Cart>();
            this.favorites = new List<UsersFavoritePosts>();
            this.groups = new List<Guid>();
            this.receivedReviews = new List<Review>();

        }

        public List<SellingUserScore> sellingUserScores { get => userScores; set => userScores = value; }
        public Guid Id { get => id; }
        public string Username { get => username; set => username = value; }
        public string RealName { get => realName; set => realName = value; }
        public DateOnly DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string ProfilePicture { get => profilePicture; set => profilePicture = value; }
        public string Password { get => password; set => password = value; }
        public DateTime CreationDate { get => creationDate; }

        public List<Cart> Carts { get => carts; }

        public List<UsersFavoritePosts> Favorites { get => favorites; }

        public List<Guid> Groups { get => groups; }

        public List<Review> Reviews { get => receivedReviews; }


        public List<Guid> GroupsWithSellingPrivilege { get => groupsWithSellingPrivilege; }

        public List<Guid> GroupsWithActiveRequestToSell { get => groupsWithActiveRequestToSell;}


        public ImageSource ProfilePictureImageSource
        {
            get
            {
                try
                {
                    return new BitmapImage(new Uri(profilePicture));
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void AddCart(Cart newCart)
        {
            carts.Add(newCart);
        }

        public void AddFavorites(UsersFavoritePosts newFavorite)
        {
            favorites.Add(newFavorite);
        }
        public void AddGroup(Guid newGroup)
        {
            groups.Add(newGroup);
        }
     

        
        public void AddReview(Review newReview)
        {
            receivedReviews.Add(newReview);
        }
        public void AddNewUserScore( SellingUserScore userScore)
        {
            this.userScores.Add(userScore);
        }


        public int NumberOfSales { get => numberOfSales; set => numberOfSales = value; }

        public void RemoveUserScore(SellingUserScore userScore)
        {
            this.userScores = this.userScores.FindAll(val => val.GroupId != userScore.GroupId);
        }  

        public void RequestSellingAccess(Guid groupId)
        {
            if(groupsWithActiveRequestToSell.Contains(groupId))
                throw new Exception("Already requested access to sell in this group");
            if(groupsWithSellingPrivilege.Contains(groupId))
                throw new Exception("Already have access to sell in this group");
            groupsWithActiveRequestToSell.Add(groupId);
        }
        public void DenyAccessToSellInGroup(Guid groupId)
        {
            if(!groupsWithActiveRequestToSell.Contains(groupId))
                throw new Exception("No active request to sell in this group");
            groupsWithActiveRequestToSell = groupsWithActiveRequestToSell.FindAll(val => val != groupId);
        }
        public void TakeAwayAccessToSellInGroup(Guid groupId)
        {
            if(!groupsWithSellingPrivilege.Contains(groupId))
                throw new Exception("No access to sell in this group");
            groupsWithSellingPrivilege = groupsWithSellingPrivilege.FindAll(val => val != groupId);
        }


        public void GiveAccessToSellInGroup(Guid groupId)
        {
            if (groupsWithSellingPrivilege.Contains(groupId))
                throw new Exception("You can already sell in this group");
            groupsWithActiveRequestToSell = groupsWithActiveRequestToSell.FindAll(val => val != groupId);
            groupsWithSellingPrivilege.Add(groupId);
        }

        public bool HasAccessToSellInGroup(Guid groupId)
        {
            if(!groupsWithSellingPrivilege.Contains(groupId))
                return false;
            return true;
        }

    }
}
