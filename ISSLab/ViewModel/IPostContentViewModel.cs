using ISSLab.Model;

namespace ISSLab.ViewModel
{
    public interface IPostContentViewModel
    {
        string AvailableFor { get; }
        string BidButtonVisible { get; set; }
        string BidPrice { get; }
        string BidPriceVisible { get; set; }
        string BuyButtonVisible { get; set; }
        string Comments { get; }
        string Contact { get; set; }
        string Delivery { get; set; }
        string Description { get; set; }
        string DonationButtonVisible { get; set; }
        string Interests { get; }
        string Location { get; }
        string Media { get; }
        string Price { get; }
        string ProfilePicture { get; }
        Post Post { get; set; }
        float Rating { get; }
        string TimePosted { get; }
        string Uninterests { get; }
        string Username { get; }
        string Visible { get; set; }

        void AddInterests();
        void AddPostToCart();
        void AddPostToFavorites();
        void AddUniterests();
        void Donate();
        Post getPost();
        void HidePost();
        void SendBuyingMessage();
        void UpdateBidPrice();
    }
}