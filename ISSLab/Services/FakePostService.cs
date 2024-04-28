using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace ISSLab.Services
{
    public class FakePostService : IPostService
    {
        public bool GetPostsCalled;
        public void AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public void AddReport(Guid postID, Guid userID, string reason)
        {
            throw new NotImplementedException();
        }

        public void BidOnAuction(Guid postID, Guid userID, double bidAmount)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfAuctionTimeEnded(Guid postID)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfNeedsConfirmation(Guid postID)
        {
            throw new NotImplementedException();
        }

        public void ConfirmPost(Guid postID)
        {
            throw new NotImplementedException();
        }

        public Post CreateAuctionPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice)
        {
            throw new NotImplementedException();
        }

        public Post CreateDonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink)
        {
            throw new NotImplementedException();
        }

        public Post CreateFixedPricePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public Post CreatePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string type)
        {
            throw new NotImplementedException();
        }

        public void Donate(Guid postID)
        {
            throw new NotImplementedException();
        }

        public void EndAuctionDueToTime(Guid postID)
        {
            throw new NotImplementedException();
        }

        public void EndAuctionExplicitly(Guid postID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public void FavoritePost(Guid postID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts()
        {
            GetPostsCalled = true;
            string expectedMediaContent = "expected Media Content";
            Post dummyPost = new Post(expectedMediaContent, Guid.NewGuid(), Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            return new List<Post> { dummyPost };
        }

        public IEnumerable<Post> GetPostsByFavorites(List<Post> postsForGroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsMainMarketPage(List<Post> postsForGroup)
        {
            throw new NotImplementedException();
        }

        public void PromotePost(Guid postID, Guid userID, Guid groupID)
        {
            throw new NotImplementedException();
        }

        public void RemoveConfirmation(Guid postID)
        {
            throw new NotImplementedException();
        }

        public void RemoveOldFixedPricePosts()
        {
            throw new NotImplementedException();
        }

        public void RemovePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void RemoveReport(Guid postID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public void ToggleInterest(Guid postID, Guid userID, bool interested)
        {
            throw new NotImplementedException();
        }

        public void UnfavoritePost(Guid postID, Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
