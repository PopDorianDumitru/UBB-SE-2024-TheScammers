using ISSLab.Model;
using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    class PostContentViewModel : ViewModelBase
    {
        private PostService postService;
        private UserService userService;
        private Guid groupId;
        private Post post;
        private Guid accountId;
        private User user;
        private string visible;
        private string favoriteDisplay;
        private string donationButtonVisible;
        private string buyButtonVisible;

       

        public PostContentViewModel(Post post, User user, Guid accountId, Guid groupId, UserService userService, PostService postService) : base()
        {
            this.postService = postService;
            this.userService = userService;
            this.groupId = groupId;
            this.accountId = accountId;
            this.post = post;
            this.user = user;
            this.visible = "Visible";
            this.donationButtonVisible = "Collapsed";
            this.buyButtonVisible = "Collapsed";
            if (post.Type == "Donation")
            {
                this.donationButtonVisible = "Visible";
            }
            else if (post.Type == "FixedPrice")
                this.buyButtonVisible = "Visible";
        }
        public PostContentViewModel()
        {
            postService = new PostService();
            userService = new UserService();
            groupId = new Guid();
            accountId = Guid.NewGuid();
            post = new Post();
            user = new User();
            visible = "Visible";

        }



        public float Rating { get { return ((FixedPricePost)(post)).ReviewScore; } }

        public string Visible { get { return visible; } set { visible = value; OnPropertyChanged(nameof(Visible)); } }
        public string Description { get { return post.Description; } set { post.Description = value; } }
        public string Contact { get { return post.Contacts; } set { post.Contacts = value; } }
        public string Delivery {
            get
            {
                if (post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)post;
                    return fixedPricePost.Delivery;
                }
                else if (post.Type == "Auction")
                {
                    AuctionPost auctionPost = (AuctionPost)post;
                    return auctionPost.Delivery;
                }
                else
                {
                    DonationPost donationPost = (DonationPost)post;
                    return "";
                }
            }
            set
            {
                if (post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)post;
                    fixedPricePost.Delivery = value;
                }
                else if (post.Type == "Auction")
                {
                    AuctionPost auctionPost = (AuctionPost)post;
                    auctionPost.Delivery = value;
                }
            }
        }
        public string DonationButtonVisible
        {
            get { return donationButtonVisible; }
            set { donationButtonVisible = value; OnPropertyChanged(nameof(DonationButtonVisible)); }
        }

        public string BuyButtonVisible
        {
            get { return buyButtonVisible; }
            set { buyButtonVisible = value; OnPropertyChanged(nameof(BuyButtonVisible)); }
        }

        public string Username { get { return user.Username; } }
        public string Media { get { return post.Media; } }

        public string Location { get { return post.Location; } }
        public string ProfilePicture { get { return user.ProfilePicture; } }
        public string TimePosted {
          get {
                TimeSpan passed = DateTime.Now - post.CreationDate;
                if(passed.TotalSeconds < 60)
                    return Math.Ceiling(passed.TotalSeconds).ToString() + " seconds ago";
                if (passed.TotalMinutes < 60)
                    return Math.Ceiling(passed.TotalMinutes).ToString() + " minutes ago";
                if (passed.TotalHours < 24)
                    return Math.Ceiling(passed.TotalHours).ToString() + " hours ago";
       

                return Math.Ceiling(passed.TotalDays).ToString() + " days ago";
            }
        }

        public Post getPost()
        {
            return post;
        }

        public void AddPostToFavorites()
        {
            this.userService.AddItemToFavorites(groupId, post.Id, accountId);
        }
        public void AddPostToCart()
        {
            this.userService.AddItemToCart(groupId, post.Id, accountId);
        }
       public string AvailableFor
        {
            get
            {
                if (post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)post;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    if (timeLeft.TotalSeconds < 60)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalSeconds).ToString() + " seconds";
                    if (timeLeft.TotalMinutes < 60)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalMinutes).ToString() + " minutes";
                    if (timeLeft.TotalHours < 24)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalHours).ToString() + " hours";
                    return "Available for: " + Math.Ceiling(timeLeft.TotalDays).ToString() + " days";
                }
                else if (post.Type == "Auction")
                {
                     AuctionPost fixedPricePost = (AuctionPost)post;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    if (timeLeft.TotalSeconds < 60)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalSeconds).ToString() + " seconds";
                    if (timeLeft.TotalMinutes < 60)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalMinutes).ToString() + " minutes";
                    if (timeLeft.TotalHours < 24)
                        return "Available for: " + Math.Ceiling(timeLeft.TotalHours).ToString() + " hours";
                    return "Available for: " + Math.Ceiling(timeLeft.TotalDays).ToString() + " days";
                }
                else
                {
                    return "";
                }
                
            }
        }
        public string Price
        {
            get
            {
                if (post.Type == "FixedPrice")
                {
                    return "$" + ((FixedPricePost)(post)).Price;
                }
                else if (post.Type == "Auction")
                {
                    return "$" + ((AuctionPost)(post)).Price; 
                }
                else
                {
                    DonationPost donationPost = (DonationPost)post;
                    return "";
                }
            }
        }

        public string Interests
        {
            get
            {
                int interested = post.InterestStatuses.FindAll(interest => interest.Interested).Count;
                return interested.ToString() + " interested";
            }
        }


        public void AddInterests()
        {
            post.InterestStatuses.Add(new InterestStatus(user.Id, post.Id, true));
            OnPropertyChanged(nameof(Interests));
        }

        public string Uninterests
        {
            get
            {
                int uninterested = post.InterestStatuses.FindAll(interest => !interest.Interested).Count;
                return uninterested.ToString() + " uninterested";
            }
        }

        public void AddUniterests()
        {
            post.InterestStatuses.Add(new InterestStatus(user.Id,post.Id,false));
            OnPropertyChanged(nameof(Uninterests));
        }

        public string Comments
        {
            get
            {
                return post.NrComments.Count + " comments";
            }
        }


    }

}
