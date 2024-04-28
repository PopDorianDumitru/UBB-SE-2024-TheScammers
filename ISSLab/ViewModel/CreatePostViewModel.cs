using ISSLab.Model;
using ISSLab.Services;

namespace ISSLab.ViewModel
{
    public class CreatePostViewModel : ViewModelBase, ICreatePostViewModel
    {
        private IPostService postService;
        private Guid groupId;
        private Guid accountId;


        private string phoneVisibleProperty;
        private string priceVisibleProperty;
        private string conditionVisibleProperty;
        private string deliveryVisibleProperty;
        private string availabilityVisibleProperty;
        private string isDonation;
        private string donationLink;
        private string isAuction;
        private string minimumBid;

        public CreatePostViewModel(Guid accountId, Guid groupId, IPostService postService) : base()
        {
            this.postService = postService;
            this.groupId = groupId;
            this.accountId = accountId;
            IsDonation = Constants.COLLAPSED_VISIBILITY;
            IsAuction = Constants.COLLAPSED_VISIBILITY;
        }

        private string type;
        private string phoneNumber;
        private string price;
        private string condition;
        private string delivery;
        private string availability;
        private string description;


        public string MinimumBid { get { return minimumBid; } set { minimumBid = value; } }
        public string IsAuction { get { return isAuction; } set { isAuction = value; OnPropertyChanged(IsAuction); } }
        public string DonationLink { get { return donationLink; } set { donationLink = value; OnPropertyChanged(nameof(DonationLink)); } }
        public string IsDonation { get { return isDonation; } set { isDonation = value; OnPropertyChanged(nameof(IsDonation)); } }
        public string PhoneVisible { get { return phoneVisibleProperty; } set { phoneVisibleProperty = value; OnPropertyChanged(nameof(PhoneVisible)); } }
        public string PriceVisible { get { return priceVisibleProperty; } set { priceVisibleProperty = value; OnPropertyChanged(nameof(PriceVisible)); } }
        public string ConditionVisible { get { return conditionVisibleProperty; } set { conditionVisibleProperty = value; OnPropertyChanged(nameof(ConditionVisible)); } }
        public string DeliveryVisible { get { return deliveryVisibleProperty; } set { deliveryVisibleProperty = value; OnPropertyChanged(nameof(DeliveryVisible)); } }
        public string AvailabilityVisible { get { return availabilityVisibleProperty; } set { availabilityVisibleProperty = value; OnPropertyChanged(nameof(AvailabilityVisible)); } }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                if (type.Contains("Fixed price"))
                {
                    IsAuction = Constants.COLLAPSED_VISIBILITY;
                    PhoneVisible = Constants.VISIBLE_VISIBILITY;
                    PriceVisible = Constants.VISIBLE_VISIBILITY;
                    ConditionVisible = Constants.VISIBLE_VISIBILITY;
                    DeliveryVisible = Constants.VISIBLE_VISIBILITY;
                    AvailabilityVisible = Constants.VISIBLE_VISIBILITY;
                    IsDonation = Constants.COLLAPSED_VISIBILITY;
                }
                else if (type.Contains(Constants.DONATION_POST_TYPE))
                {
                    PriceVisible = Constants.COLLAPSED_VISIBILITY;
                    ConditionVisible = Constants.COLLAPSED_VISIBILITY;
                    DeliveryVisible = Constants.COLLAPSED_VISIBILITY;
                    AvailabilityVisible = Constants.COLLAPSED_VISIBILITY;
                    IsDonation = Constants.VISIBLE_VISIBILITY;
                    IsAuction = Constants.COLLAPSED_VISIBILITY;
                }
                else
                {
                    IsAuction = Constants.VISIBLE_VISIBILITY;
                    IsDonation = Constants.COLLAPSED_VISIBILITY;
                    PhoneVisible = Constants.VISIBLE_VISIBILITY;
                    AvailabilityVisible = Constants.COLLAPSED_VISIBILITY;
                    ConditionVisible = Constants.COLLAPSED_VISIBILITY;
                    PriceVisible = Constants.VISIBLE_VISIBILITY;

                }

                OnPropertyChanged(nameof(Type));
            }
        }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Price { get { return price; } set { price = value; OnPropertyChanged(nameof(Price)); } }
        public string Condition { get { return condition; } set { condition = value; OnPropertyChanged(nameof(Condition)); } }
        public string Delivery { get { return delivery; } set { delivery = value; OnPropertyChanged(nameof(Delivery)); } }
        public string Availability { get { return availability; } set { availability = value; OnPropertyChanged(nameof(Availability)); } }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
        public Guid AccountId { get { return accountId; } }
        public Guid GroupId { get { return groupId; } }
        public void CreatePost()
        {
            if (Type.Contains("Fixed price"))
                CreateFixedPricePost();
            else
                CreateDonationPost();
        }

        public void CreateDonationPost()
        {
            DonationPost donationPost = new DonationPost("", accountId, groupId, "", Description, "", phoneNumber, donationLink, Constants.DONATION_POST_TYPE, true);
            postService.AddPost(donationPost);

            ResetFields();
        }

        public void CreateFixedPricePost()
        {
            FixedPricePost fixedPrice = new FixedPricePost("", accountId, groupId, "Cluj", Description, "", PhoneNumber, float.Parse(Price), DateTime.Now.AddMonths(3), Delivery, new List<Review>(), 0, Guid.Empty, Constants.FIXED_PRICE_POST_TYPE, false);
            postService.AddPost(fixedPrice);

            ResetFields();
        }

        private void ResetFields()
        {
            Type = "";
            PhoneNumber = "";
            Price = "";
            Condition = "";
            Delivery = "";
            Availability = "";
            Description = "";
        }
    }
}

