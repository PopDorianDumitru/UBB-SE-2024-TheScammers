using ISSLab.Model;
using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    class CreatePostViewModel : ViewModelBase
    {
        private PostService postService;
        private UserService userService;
        private Guid groupId;
        private Guid accountId;

        public CreatePostViewModel(Guid accountId, Guid groupId, UserService userService, PostService postService) : base()
        {
            this.postService = postService;
            this.userService = userService;
            this.groupId = groupId;
            this.accountId = accountId;

        }
        public CreatePostViewModel()
        {
            postService = new PostService();
            userService = new UserService();
            groupId = new Guid();
            accountId = Guid.NewGuid();
      

        }

        private string type;
        private string phoneNumber;
        private string price;
        private string condition;
        private string delivery;
        private string availability;
        private string description;

        public string Type { get { return type; } set { type = value; OnPropertyChanged(nameof(Type)); } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Price { get { return price; } set { price = value; OnPropertyChanged(nameof(Price)); } }
        public string Condition { get { return condition; } set { condition = value; OnPropertyChanged(nameof(Condition)); } }
        public string Delivery { get { return delivery; } set { delivery = value; OnPropertyChanged(nameof(Delivery)); } }
        public string Availability { get { return availability; } set { availability = value; OnPropertyChanged(nameof(Availability)); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged(nameof(Description)); }
               }
        public Guid AccountId { get {  return accountId; }  }

        public void CreatePost()
        {
            if (Type == "Fixed price")
                CreateFixedPricePost();

        }

        public void CreateFixedPricePost()
        {
            FixedPricePost fixedPrice = new FixedPricePost("", accountId, groupId, "Cluj", Description, "", PhoneNumber, float.Parse(Price), DateTime.Now.AddMonths(3), Delivery, new List<Review>(), 0, Guid.Empty, "FixedPrice", false);
            postService.AddPost(fixedPrice);
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

