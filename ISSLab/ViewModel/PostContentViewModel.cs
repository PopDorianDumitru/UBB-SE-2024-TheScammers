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
        public static string MINUTES_AGO = " minutes ago";
        public static string SECONDS_AGO = " seconds ago";

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
        public IChatFactory _chatFactory { get; }
        public PostContentViewModel(Post post, User user, Guid accountId, Guid groupId, IUserService userService, IChatFactory chatFactory) : base()
        {
            this._userService = userService;
            this._groupId = groupId;
            this._accountId = accountId;
            this._post = post;
            this.user = user;
            this._visible = Constants.VISIBLE_VISIBILITY;
            this._donationButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this._buyButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this._bidButtonVisible = Constants.COLLAPSED_VISIBILITY;
            this._bidPriceVisible = Constants.COLLAPSED_VISIBILITY;
            if (_post.Type == Constants.DONATION_POST_TYPE)
            {
                this._donationButtonVisible = Constants.VISIBLE_VISIBILITY;
            }
            else if (_post.Type == Constants.FIXED_PRICE_POST_TYPE)
                this._buyButtonVisible = Constants.VISIBLE_VISIBILITY;
            else if (_post.Type == Constants.AUCTION_POST_TYPE)

            {
                this._buyButtonVisible = Constants.VISIBLE_VISIBILITY;
                this._bidButtonVisible = Constants.VISIBLE_VISIBILITY;
                this._bidPriceVisible = Constants.VISIBLE_VISIBILITY;
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _chatFactory = chatFactory;
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
                if (_post.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    return fixedPricePost.Delivery;
                }
                else if (_post.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost auctionPost = (AuctionPost)_post;
                    return auctionPost.Delivery;
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
            set
            {
                if (_post.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    fixedPricePost.Delivery = value;
                }
                else if (_post.Type == Constants.AUCTION_POST_TYPE)
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

        public string Media { get { return _post.MediaContent; } }

        public string Location { get { return _post.ItemLocation; } }

        public string ProfilePicture { get { return user.ProfilePicture; } }
        public string TimePosted
        {
            get
            {
                TimeSpan passed = DateTime.Now - _post.CreationDate;
                if (passed.TotalSeconds < 60)
                    return Math.Ceiling(passed.TotalSeconds).ToString() + SECONDS_AGO;
                if (passed.TotalMinutes < 60)
                    return Math.Ceiling(passed.TotalMinutes).ToString() + MINUTES_AGO;
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
            this._userService.AddPostToFavorites(_groupId, _post.Id, _accountId);
        }

        public void AddPostToCart()
        {
            this._userService.AddPostToCart(_groupId, _post.Id, _accountId);
        }

        public string AvailableFor
        {
            get
            {
                if (_post.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    FixedPricePost fixedPricePost = (FixedPricePost)_post;
                    TimeSpan timeLeft = fixedPricePost.ExpirationDate - DateTime.Now;
                    return DisplayRemainingTime(timeLeft);
                }
                else if (_post.Type == Constants.AUCTION_POST_TYPE)
                {
                    AuctionPost fixedPricePost = (AuctionPost)_post;
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
                if (_post.Type == Constants.FIXED_PRICE_POST_TYPE)
                {
                    return Constants.DOLLAR_SIGN + ((FixedPricePost)(_post)).Price;
                }
                else if (_post.Type == Constants.AUCTION_POST_TYPE)
                {
                    return Constants.DOLLAR_SIGN + ((AuctionPost)(_post)).Price;
                }
                else
                {
                    return Constants.EMPTY_STRING;
                }
            }
        }

        public void UpdateBidPrice()
        {
            if (_post.GetType() == typeof(AuctionPost))
            {
                AuctionPost auctionPost = (AuctionPost)_post;
                TimeSpan timeLeft = auctionPost.ExpirationDate - DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(Constants.EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS);
                if (timeLeft.TotalSeconds < Constants.EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS)
                {
                    auctionPost.SlightlyPostponeExpirationDate();
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
                if (_post.Type == Constants.AUCTION_POST_TYPE)
                    return Constants.DOLLAR_SIGN + ((AuctionPost)(_post)).CurrentBidPrice;
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
                int interested = _post.InterestStatuses.FindAll(interest => interest.Interested).Count;
                return interested.ToString() + " interested";
            }
        }

        public void AddInterests()
        {
            var existingInterest = _post.InterestStatuses.FirstOrDefault(interest => interest.InterestedUserId == user.Id && interest.PostId == _post.Id);

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

        public void AddUninterests()
        {
            var existingUninterest = _post.InterestStatuses.FirstOrDefault(interest => interest.InterestedUserId == user.Id && interest.PostId == _post.Id && !interest.Interested);

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
                return _post.Comments.Count + " comments";
            }
        }

        public void SendBuyingMessage()
        {
            var chatViewModel = new ChatViewModel(user, _post);
            IChat chat = _chatFactory.CreateChat(chatViewModel);
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
            Visible = Constants.COLLAPSED_VISIBILITY;
        }
    }

}
