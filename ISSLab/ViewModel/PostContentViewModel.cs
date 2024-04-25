using ISSLab.Model;
using ISSLab.Services;
using ISSLab.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ISSLab.ViewModel
{
    public class PostContentViewModel : ViewModelBase, IPostContentViewModel
    {
        private IUserService _userService;
        public Guid _groupId { get; set; }
        private Post _post { get; set; }
        public Guid _accountId { get; set; }
        public User user;
        private string _visible;
        private string _donationButtonVisible;
        private string _buyButtonVisible;
        private string _bidButtonVisible;
        private string _bidPriceVisible;
        private DispatcherTimer _timer;

        public PostContentViewModel(Post post, User user, Guid accountId, Guid groupId, IUserService userService) : base()
        {
            this._userService = userService;
            this._groupId = groupId;
            this._accountId = accountId;
            this._post = post;
            this.user = user;
            this._visible = "Visible";
            this._donationButtonVisible = "Collapsed";
            this._buyButtonVisible = "Collapsed";
            this._bidButtonVisible = "Collapsed";
            this._bidPriceVisible = "Collapsed";
            if (post.Type == "Donation")
            {
                this._donationButtonVisible = "Visible";
            }
            else if (post.Type == "FixedPrice")
                this.buyButtonVisible = "Visible";
            else if (post.Type == Constants.AUCTION_POST_TYPE)

            {
                this._buyButtonVisible = "Visible";
                this._bidButtonVisible = "Visible";
                this._bidPriceVisible = "Visible";
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(AvailableFor));
        }
        public Post Post
        {
            get { return _post; }
            set { _post = value; }
        }

        public float Rating
        {
            get
            {
                return ((FixedPricePost)_post).ReviewScore;
            }
        }

        public string Visible { get { return _visible; } set { _visible = value; OnPropertyChanged(nameof(Visible)); } }
        
        public string Description { get { return _post.Description; } set { _post.Description = value; } }
        
        public string Contact { get { return _post.Contacts; } set { _post.Contacts = value; } }
        
        public string Delivery
        {
            get
            {
                if (_post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    return fixedPricePost.Delivery;
                }
                else if (post.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost auctionPost = (AuctionPost)_post;
                    return auctionPost.Delivery;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    fixedPricePost.Delivery = value;
                }
                else if (post.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost auctionPost = (AuctionPost)_post;
                    auctionPost.Delivery = value;
                }
            }
        }
        
        public string DonationButtonVisible
        {
            get { return _donationButtonVisible; }
            set { _donationButtonVisible = value; OnPropertyChanged(nameof(DonationButtonVisible)); }
        }

        public string BuyButtonVisible
        {
            get { return _buyButtonVisible; }
            set { _buyButtonVisible = value; OnPropertyChanged(nameof(BuyButtonVisible)); }
        }

        public string BidButtonVisible
        {
            get { return _bidButtonVisible; }
            set { _bidButtonVisible = value; OnPropertyChanged(nameof(BidButtonVisible)); }
        }

        public string BidPriceVisible
        {
            get { return _bidPriceVisible; }
            set { _bidPriceVisible = value; OnPropertyChanged(nameof(BidPriceVisible)); }
        }

        public string Username { get { return user.Username; } }
        
        public string Media { get { return _post.Media; } }

        public string Location { get { return _post.Location; } }

        public string ProfilePicture { get { return user.ProfilePicture; } }
        public string TimePosted
        {
            get
            {
                TimeSpan passed = DateTime.Now - _post.CreationDate;
                if (passed.TotalSeconds < 60)
                    return Math.Ceiling(passed.TotalSeconds).ToString() + " seconds ago";
                if (passed.TotalMinutes < 60)
                    return Math.Ceiling(passed.TotalMinutes).ToString() + " minutes ago";
                if (passed.TotalHours < 24)
                    return Math.Ceiling(passed.TotalHours).ToString() + " hours ago";


                return Math.Ceiling(passed.TotalDays).ToString() + " days ago";
            }
        }

        public Post GetPost()
        {
            return _post;
        }

        public void AddPostToFavorites()
        {
            this._userService.AddItemToFavorites(_groupId, _post.Id, _accountId);
        }

        public void AddPostToCart()
        {
            this._userService.AddItemToCart(_groupId, _post.Id, _accountId);
        }

        public string AvailableFor
        {
            get
            {
                if (_post.Type == "FixedPrice")
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    return DisplayRemainingTime(timeLeft);
                }
                else if (post.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost fixedPricePost = (AuctionPost)_post;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    return DisplayRemainingTime(timeLeft);
                }
                else
                {
                    return "";
                }

            }
        }

        public string DisplayRemainingTime(TimeSpan timeLeft)
        {
            if (timeLeft.TotalSeconds < 60)
                return "Available for: " + Math.Ceiling(timeLeft.TotalSeconds).ToString() + " seconds";
            if (timeLeft.TotalMinutes < 60)
                return "Available for: " + Math.Ceiling(timeLeft.TotalMinutes).ToString() + " minutes";
            if (timeLeft.TotalHours < 24)
                return "Available for: " + Math.Ceiling(timeLeft.TotalHours).ToString() + " hours";
            return "Available for: " + Math.Ceiling(timeLeft.TotalDays).ToString() + " days";
        }

        public string Price
        {
            get
            {
                if (_post.Type == "FixedPrice")
                {
                    return "$" + ((FixedPricePost)(_post)).Price;
                }
                else if (post.Type == Constants.AUCTION_POST_TYPE)
                {
                    return "$" + ((AuctionPost)(_post)).Price;
                }
                else
                {
                    return "";
                }
            }
        }

        public void UpdateBidPrice()
        {
            if (_post.GetType() == typeof(AuctionPost)) {
                AuctionPost auctionPost = (AuctionPost)_post;
                TimeSpan timeLeft = auctionPost.ExpirationDate - DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(30);
            if (timeLeft.TotalSeconds < 30)
            {
                auctionPost.Add30SecondsToExpirationDate();
                OnPropertyChanged(nameof(AvailableFor));
            }
            ((AuctionPost)(_post)).CurrentBidPrice += 5;
                ((AuctionPost)(_post)).MinimumBidPrice += 5;
                OnPropertyChanged(nameof(BidPrice));
            }
            else throw new Exception("Post is not of type AuctionPost!");
        }

        public string BidPrice
        {
            get
            {
                if (post.Type == Constants.AUCTION_POST_TYPE)
                    return "$" + ((AuctionPost)(post)).CurrentBidPrice;
                else
                {
                    return "";
                }
            }
        }

        public string Interests
        {
            get
            {
                int interested = _post.InterestStatuses.FindAll(interest => interest.Interested).Count;
                return interested.ToString() + " interested";
            }
        }

        public void AddInterests()
        {
            var existingInterest = _post.InterestStatuses.FirstOrDefault(interest => interest.UserId == user.Id && interest.PostId == _post.Id);

            if (existingInterest != null)
            {
                _post.InterestStatuses.Remove(existingInterest);
            }
            else
            {
                _post.InterestStatuses.Add(new InterestStatus(user.Id, _post.Id, true));
            }

            OnPropertyChanged(nameof(Interests));
        }

        public string Uninterests
        {
            get
            {
                int uninterested = _post.InterestStatuses.FindAll(interest => !interest.Interested).Count;
                return uninterested.ToString() + " uninterested";
            }
        }

        public void AddUniterests()
        {
            var existingUninterest = _post.InterestStatuses.FirstOrDefault(interest => interest.UserId == user.Id && interest.PostId == _post.Id && !interest.Interested);

            if (existingUninterest != null)
            {
                _post.InterestStatuses.Remove(existingUninterest);
            }
            else
            {
                _post.InterestStatuses.Add(new InterestStatus(user.Id, _post.Id, false));
            }
            OnPropertyChanged(nameof(Uninterests));
        }

        public string Comments
        {
            get
            {
                return _post.NrComments.Count + " comments";
            }
        }

        public void SendBuyingMessage()
        {
            Chat chat = new Chat(new ChatViewModel(user, _post));
            chat.SendBuyingMessage(Media);
            chat.Show();
        }

        public void Donate()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = ((DonationPost)_post).DonationPageLink,
                UseShellExecute = true
            });
        }

        public void HidePost()
        {
            Visible = "Collapsed";
        }
    }

}
