using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;
using ISSLab.Model;
using ISSLab.Services;
using ISSLab.View;

namespace ISSLab.ViewModel
{
    public class PostContentViewModel : ViewModelBase, IPostContentViewModel
    {
        public static string MINUTES_AGO = " minutes ago";
        public static string SECONDS_AGO = " seconds ago";

        private IUserService userService;
        public Guid GroupId { get; set; }
        private Post OurPost { get; set; }
        public Guid AccountId { get; set; }
        public User OurUser;
        private string visible;
        private string donationButtonVisible;
        private string buyButtonVisible;
        private string bidButtonVisible;
        private string bidPriceVisible;
        private DispatcherTimer timer;
        public IChatFactory OurChatFactory { get; }
        public PostContentViewModel(Post post, User user, Guid accountId, Guid groupId, IUserService userService, IChatFactory chatFactory) : base()
        {
            this.userService = userService;
            this.GroupId = groupId;
            this.AccountId = accountId;
            this.OurPost = post;
            this.OurUser = user;
            this.visible = Constants.VISIBLE_VISIBILITY;
            this.donationButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this.buyButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this.bidButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this.bidPriceVisible = Constants.COLLAPSED_VISIBILITY;
            if (this.OurPost.Type == Constants.DONATION_POST_TYPE)
            {
                this.donationButtonVisible = Constants.VISIBLE_VISIBILITY;
            }
            else if (this.OurPost.Type == Constants.FIXED_PRICE_POST_TYPE)
            {
                this.buyButtonVisible = Constants.VISIBLE_VISIBILITY;
            }
            else if (this.OurPost.Type == Constants.AUCTION_POST_TYPE)
            {
                this.buyButtonVisible = Constants.VISIBLE_VISIBILITY;
                this.bidButtonVisible = Constants.VISIBLE_VISIBILITY;
                this.bidPriceVisible = Constants.VISIBLE_VISIBILITY;
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.OurChatFactory = chatFactory;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(AvailableFor));
        }
        public Post Post
        {
            get { return OurPost; }
            set { OurPost = value; }
        }

        public float Rating
        {
            get
            {
                return ((FixedPricePost)OurPost).ReviewScore;
            }
        }

        public string Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        public string Description
        {
            get { return OurPost.Description; }
            set { OurPost.Description = value; }
        }

        public string Contact
        {
            get { return OurPost.Contacts; }
            set { OurPost.Contacts = value; }
        }

        public string Delivery
        {
            get
            {
                if (OurPost.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)OurPost;
                    return fixedPricePost.Delivery;
                }
                else if (OurPost.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost auctionPost = (AuctionPost)OurPost;
                    return auctionPost.Delivery;
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
            set
            {
                if (OurPost.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)OurPost;
                    fixedPricePost.Delivery = value;
                }
                else if (OurPost.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost auctionPost = (AuctionPost)OurPost;
                    auctionPost.Delivery = value;
                }
            }
        }

        public string DonationButtonVisible
        {
            get
            {
                return donationButtonVisible;
            }
            set
            {
                donationButtonVisible = value;
                OnPropertyChanged(nameof(DonationButtonVisible));
            }
        }

        public string BuyButtonVisible
        {
            get
            {
                return buyButtonVisible;
            }
            set
            {
                buyButtonVisible = value;
                OnPropertyChanged(nameof(BuyButtonVisible));
            }
        }

        public string BidButtonVisible
        {
            get
            {
                return bidButtonVisible;
            }
            set
            {
                bidButtonVisible = value;
                OnPropertyChanged(nameof(BidButtonVisible));
            }
        }

        public string BidPriceVisible
        {
            get
            {
                return bidPriceVisible;
            }
            set
            {
                bidPriceVisible = value;
                OnPropertyChanged(nameof(BidPriceVisible));
            }
        }

        public string Username
        {
            get
            {
                return OurUser.Username;
            }
        }

        public string Media
        {
            get
            {
                return OurPost.MediaContent;
            }
        }

        public string Location
        {
            get
            {
                return OurPost.ItemLocation;
            }
        }

        public string ProfilePicture
        {
            get
            {
                return OurUser.ProfilePicture;
            }
        }
        public string TimePosted
        {
            get
            {
                TimeSpan passed = DateTime.Now - OurPost.CreationDate;
                if (passed.TotalSeconds < 60)
                {
                    return Math.Ceiling(passed.TotalSeconds).ToString() + SECONDS_AGO;
                }
                if (passed.TotalMinutes < 60)
                {
                    return Math.Ceiling(passed.TotalMinutes).ToString() + MINUTES_AGO;
                }
                if (passed.TotalHours < 24)
                {
                    return Math.Ceiling(passed.TotalHours).ToString() + " hours ago";
                }

                return Math.Ceiling(passed.TotalDays).ToString() + " days ago";
            }
        }

        public Post GetPost()
        {
            return OurPost;
        }

        public void AddPostToFavorites()
        {
            this.userService.AddPostToFavorites(GroupId, OurPost.Id, AccountId);
        }

        public void AddPostToCart()
        {
            this.userService.AddPostToCart(GroupId, OurPost.Id, AccountId);
        }

        public string AvailableFor
        {
            get
            {
                if (OurPost.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)OurPost;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    return DisplayRemainingTime(timeLeft);
                }
                else if (OurPost.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost fixedPricePost = (AuctionPost)OurPost;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    return DisplayRemainingTime(timeLeft);
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
        }

        public string DisplayRemainingTime(TimeSpan timeLeft)
        {
            if (timeLeft.TotalSeconds < 60)
            {
                return "Available for: " + Math.Ceiling(timeLeft.TotalSeconds).ToString() + " seconds";
            }
            if (timeLeft.TotalMinutes < 60)
            {
                return "Available for: " + Math.Ceiling(timeLeft.TotalMinutes).ToString() + " minutes";
            }
            if (timeLeft.TotalHours < 24)
            {
                return "Available for: " + Math.Ceiling(timeLeft.TotalHours).ToString() + " hours";
            }
            return "Available for: " + Math.Ceiling(timeLeft.TotalDays).ToString() + " days";
        }

        public string Price
        {
            get
            {
                if (OurPost.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    return Constants.DOLLAR_SIGN + ((FixedPricePost)(OurPost)).Price;
                }
                else if (OurPost.Type == Constants.AUCTION_POST_TYPE)
                {
                    return Constants.DOLLAR_SIGN + ((AuctionPost)(OurPost)).Price;
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
        }

        public void UpdateBidPrice()
        {
            if (OurPost.GetType() == typeof(AuctionPost))
            {
                AuctionPost auctionPost = (AuctionPost)OurPost;
                TimeSpan timeLeft = auctionPost.ExpirationDate - DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(Constants.EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS);
                if (timeLeft.TotalSeconds < Constants.EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS)
                {
                    auctionPost.SlightlyPostponeExpirationDate();
                    OnPropertyChanged(nameof(AvailableFor));
                }
            ((AuctionPost)(OurPost)).CurrentBidPrice += 5;
                ((AuctionPost)(OurPost)).MinimumBidPrice += 5;
                OnPropertyChanged(nameof(BidPrice));
            }
            else
            {
                throw new Exception("Post is not of type AuctionPost!");
            }
        }

        public string BidPrice
        {
            get
            {
                if (OurPost.Type == Constants.AUCTION_POST_TYPE)
                {
                    return Constants.DOLLAR_SIGN + ((AuctionPost)(OurPost)).CurrentBidPrice;
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
        }

        public string Interests
        {
            get
            {
                int interested = OurPost.InterestStatuses.FindAll(interest => interest.Interested).Count;
                return interested.ToString() + " interested";
            }
        }

        public void AddInterests()
        {
            var existingInterest = OurPost.InterestStatuses.FirstOrDefault(interest => interest.InterestedUserId == OurUser.Id && interest.PostId == OurPost.Id);

            if (existingInterest != null)
            {
                OurPost.InterestStatuses.Remove(existingInterest);
            }
            else
            {
                OurPost.InterestStatuses.Add(new InterestStatus(OurUser.Id, OurPost.Id, true));
            }

            OnPropertyChanged(nameof(Interests));
        }

        public string Uninterests
        {
            get
            {
                int uninterested = OurPost.InterestStatuses.FindAll(interest => !interest.Interested).Count;
                return uninterested.ToString() + " uninterested";
            }
        }

        public void AddUninterests()
        {
            var existingUninterest = OurPost.InterestStatuses.FirstOrDefault(interest => interest.InterestedUserId == OurUser.Id && interest.PostId == OurPost.Id && !interest.Interested);

            if (existingUninterest != null)
            {
                OurPost.InterestStatuses.Remove(existingUninterest);
            }
            else
            {
                OurPost.InterestStatuses.Add(new InterestStatus(OurUser.Id, OurPost.Id, false));
            }
            OnPropertyChanged(nameof(Uninterests));
        }

        public string Comments
        {
            get
            {
                return OurPost.Comments.Count + " comments";
            }
        }

        public void SendBuyingMessage()
        {
            var chatViewModel = new ChatViewModel(OurUser, OurPost);
            IChat chat = OurChatFactory.CreateChat(chatViewModel);
            chat.SendBuyingMessage(Media);
            chat.Show();
        }

        public void Donate()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = ((DonationPost)OurPost).DonationPageLink,
                UseShellExecute = true
            });
        }

        public void HidePost()
        {
            Visible = Constants.COLLAPSED_VISIBILITY;
        }
    }
}
