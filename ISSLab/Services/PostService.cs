using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.Model.Repositories;
namespace ISSLab.Services
{
    public class PostService : IPostService
    {
        private const double MINIMUM_SCORE_TO_POST_CONFIRMED_POSTS = 3.75;
        private const int NUMBER_OF_VIEWS_ABOVE_WHICH_CONFIRMATION_NEEDED = 10;

        private IPostRepository postRepository;
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;

        public PostService(IPostRepository posts, IUserRepository users, IGroupRepository groups)
        {
            this.postRepository = posts;
            this.userRepository = users;
            this.groupRepository = groups;
        }

        public List<Post> GetPosts()
        {
            return postRepository.GetAllPosts();
        }

        public void AddPost(Post post)
        {
            postRepository.AddPost(post);
        }
        public void RemovePost(Post post)
        {
            postRepository.RemovePost(post.Id);
        }
        public Post GetPostById(Guid id)
        {
            Post? p = postRepository.GetPostById(id);
            if (p == null)
            {
                throw new Exception("Post not found");
            }
            return p;
        }
        public Post CreatePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string type)
        {
            return new Post(media, authorId, groupId, location, description, title, contacts, type, true);
        }

        public Post CreateFixedPricePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId)
        {
            if (price < 0)
            {
                throw new Exception("Price can't be negative");
            }
            User? user = userRepository.GetById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSellInGroup(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < MINIMUM_SCORE_TO_POST_CONFIRMED_POSTS)
            {
                return new FixedPricePost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, Constants.EXTENDED_FIXED_PRICE_POST_TYPE, false);
            }
            return new FixedPricePost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, Constants.EXTENDED_FIXED_PRICE_POST_TYPE, true);
        }

        public Post CreateAuctionPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice)
        {
            if (price < 0 || minimumBidPrice < 0 || currentBidPrice < 0)
            {
                throw new Exception("Price can't be negative");
            }
            User? user = userRepository.GetById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSellInGroup(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < MINIMUM_SCORE_TO_POST_CONFIRMED_POSTS)
            {
                return new AuctionPost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, currentPriceLeader, currentBidPrice, minimumBidPrice, false);
            }
            return new AuctionPost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, currentPriceLeader, currentBidPrice, minimumBidPrice, false);
        }

        public Post CreateDonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink)
        {
            User? user = userRepository.GetById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSellInGroup(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < MINIMUM_SCORE_TO_POST_CONFIRMED_POSTS)
            {
                return new DonationPost(media, authorId, groupId, location, description, title, contacts, donationPageLink, "DonationPost", false);
            }
            return new DonationPost(media, authorId, groupId, location, description, title, contacts, donationPageLink, "DonationPost", true);
        }

        public IEnumerable<Post> GetPostsMainMarketPage(List<Post> postsForGroup)
        {
            return postsForGroup.OrderByDescending(post => post.Promoted).ThenByDescending(post => post.InterestLevel());
        }

        public bool CheckIfNeedsConfirmation(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Views > NUMBER_OF_VIEWS_ABOVE_WHICH_CONFIRMATION_NEEDED && post.Views / 2 <= post.Reports.Count)
                return true;
            return false;
        }

        public void RemoveConfirmation(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Confirmed = false;
        }

        public void ConfirmPost(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Confirmed = true;
        }

        public void AddReport(Guid postId, Guid userId, string reason)
        {
            Post? post = postRepository.GetPostById(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Reports.Add(new Report(userId, postId, reason));
        }

        public void RemoveReport(Guid postID, Guid userId)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Reports.RemoveAll(report => report.UserId == userId);
        }

        public bool CheckIfAuctionTimeEnded(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != Constants.EXTENDED_AUCTION_POST_TYPE)
            {
                throw new Exception("Post is not an auction post");
            }
            AuctionPost auctionPost = (AuctionPost)post;
            if (auctionPost.ExpirationDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public void BidOnAuction(Guid postID, Guid userID, double bidAmount)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != Constants.EXTENDED_AUCTION_POST_TYPE)
            {
                throw new Exception("Post is not an auction post");
            }
            AuctionPost auctionPost = (AuctionPost)post;
            if (auctionPost.CurrentBidPrice >= bidAmount)
            {
                throw new Exception("The bid amount must be over the current maximum bid!");
            }
            if (auctionPost.CurrentBidPrice + auctionPost.MinimumBidPrice > bidAmount)
            {
                throw new Exception("The bid amount must be at least the minimum bid price higher than the current maximum bid!");
            }
            auctionPost.CurrentBidPrice = bidAmount;
            auctionPost.CurrentPriceLeader = userID;
            if ((auctionPost.ExpirationDate - DateTime.Now).TotalSeconds < Constants.EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS)
                auctionPost.SlightlyPostponeExpirationDate();
        }

        public void Donate(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != Constants.EXTENDED_DONATION_POST_TYPE)
            {
                throw new Exception("Post is not a donation post");
            }
            DonationPost donationPost = (DonationPost)post;
            donationPost.Donate();
        }

        public void EndAuctionDueToTime(Guid postID)
        {
            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != Constants.EXTENDED_AUCTION_POST_TYPE)
            {
                throw new Exception("Post is not an auction post");
            }
            AuctionPost auctionPost = (AuctionPost)post;
            auctionPost.OnGoing = false;
        }

        public void EndAuctionExplicitly(Guid postID, Guid userID)
        {

            Post? post = postRepository.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != Constants.EXTENDED_AUCTION_POST_TYPE)
            {
                throw new Exception("Post is not an auction post");
            }
            AuctionPost auctionPost = (AuctionPost)post;
            if (auctionPost.AuthorId != userID)
            {
                throw new Exception("User is not the auctioneer");
            }

            auctionPost.OnGoing = false;
        }

        public void RemoveOldFixedPricePosts()
        {
            DateTime threeMonthsAgo = DateTime.Now.AddMonths(-3);
            List<Post> fixedPricePosts = postRepository.GetAllPosts().FindAll(checkedPost => checkedPost.Type == Constants.EXTENDED_FIXED_PRICE_POST_TYPE);
            fixedPricePosts.ForEach(fixedPricePost =>
            {
                if (fixedPricePost.CreationDate <= threeMonthsAgo)
                    postRepository.RemovePost(fixedPricePost.Id);
            });
        }

        public IEnumerable<Post> GetPostsByFavorites(List<Post> postsForGroup)
        {
            return postsForGroup.OrderByDescending(post => post.UsersThatFavorited.Count);
        }

        public void ToggleInterest(Guid postId, Guid userId, bool interested)
        {
            Post? post = postRepository.GetPostById(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            InterestStatus? status = post.InterestStatuses.Find(status => status.InterestedUserId == userId);
            if (status == null)
            {
                post.InterestStatuses.Add(new InterestStatus(userId, postId, interested));
            }
            else
            {
                if (status.Interested == interested)
                    post.RemoveInterestStatus(userId);
                else
                    post.ToggleInterestStatus(userId);
            }
        }

        public void PromotePost(Guid postId, Guid userId, Guid groupId)
        {
            Post? post = postRepository.GetPostById(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.AuthorId != userId)
            {
                throw new Exception("User is not the author of the post");
            }
            Group? group = groupRepository.FindById(groupId);
            if (group == null)
            {
                throw new Exception("That group does not exist");
            }
            if (group.TopSellers.Contains(userId))
            {
                post.Promoted = true;
                group.RemoveTopSeller(userId);
            }
            else
                throw new Exception("User is not a big seller in the group");
        }

        public void FavoritePost(Guid postId, Guid userId)
        {
            Post? post = postRepository.GetPostById(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.UsersThatFavorited.Add(userId);
        }

        public void UnfavoritePost(Guid postId, Guid userId)
        {
            Post? post = postRepository.GetPostById(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.UsersThatFavorited.Remove(userId);
        }
    }
}
