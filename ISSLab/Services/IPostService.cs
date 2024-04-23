using ISSLab.Model;

namespace ISSLab.Services
{
    public interface IPostService
    {
        void AddPost(Post post);
        void AddReport(Guid postID, Guid userID, string reason);
        void BidOnAuction(Guid postID, Guid userID, double bidAmount);
        bool CheckIfAuctionTimeEnded(Guid postID);
        bool CheckIfNeedsConfirmation(Guid postID);
        void ConfirmPost(Guid postID);
        Post CreateAuctionPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice);
        Post CreateDonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink);
        Post CreateFixedPricePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId);
        Post CreatePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string type);
        void Donate(Guid postID);
        void EndAuctionDueToTime(Guid postID);
        void EndAuctionExplicitly(Guid postID, Guid userID);
        void FavoritePost(Guid postID, Guid userID);
        Post GetPostById(Guid id);
        List<Post> GetPosts();
        IEnumerable<Post> GetPostsByFavorites(List<Post> postsForGroup);
        IEnumerable<Post> GetPostsMainMarketPage(List<Post> postsForGroup);
        void PromotePost(Guid postID, Guid userID, Guid groupID);
        void RemoveConfirmation(Guid postID);
        void RemoveOldFixedPricePosts();
        void RemovePost(Post post);
        void RemoveReport(Guid postID, Guid userID);
        void ToggleInterest(Guid postID, Guid userID, bool interested);
        void UnfavoritePost(Guid postID, Guid userID);
    }
}