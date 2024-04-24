
namespace ISSLab.ViewModel
{
    public interface ICreatePostViewModel
    {
        Guid AccountId { get; }
        string Availability { get; set; }
        string AvailabilityVisible { get; set; }
        string Condition { get; set; }
        string ConditionVisible { get; set; }
        string Delivery { get; set; }
        string DeliveryVisible { get; set; }
        string Description { get; set; }
        string DonationLink { get; set; }
        string IsAuction { get; set; }
        string IsDonation { get; set; }
        string MinimumBid { get; set; }
        string PhoneNumber { get; set; }
        string PhoneVisible { get; set; }
        string Price { get; set; }
        string PriceVisible { get; set; }
        string Type { get; set; }

        void CreateDonationPost();
        void CreateFixedPricePost();
        void CreatePost();
    }
}