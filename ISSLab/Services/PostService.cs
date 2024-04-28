﻿using System;
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
        IPostRepository posts;
        IUserRepository users;
        IGroupRepository groups;

        public PostService(IPostRepository posts, IUserRepository users, IGroupRepository groups)
        {
            this.posts = posts;
            this.users = users;
            this.groups = groups;
        }

        public List<Post> GetPosts()
        {
            return posts.GetAllPosts();
        }

        public void AddPost(Post post)
        {
            posts.AddPost(post);
        }
        public void RemovePost(Post post)
        {
            posts.RemovePost(post.Id);
        }
        public Post GetPostById(Guid id)
        {
            Post? p = posts.GetPostById(id);
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
            User? user = users.FindById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSell(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < 3.75)
            {
                return new FixedPricePost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, "FixedPricePost", false);
            }
            return new FixedPricePost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, "FixedPricePost", true);
        }

        public Post CreateAuctionPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, double price, DateTime expirationDate, string delivery, List<Review> reviews, float reviewScore, Guid buyerId, Guid currentPriceLeader, double currentBidPrice, double minimumBidPrice)
        {
            if (price < 0 || minimumBidPrice < 0 || currentBidPrice < 0)
            {
                throw new Exception("Price can't be negative");
            }
            User? user = users.FindById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSell(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < 3.75)
            {
                return new AuctionPost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, currentPriceLeader, currentBidPrice, minimumBidPrice, false);
            }
            return new AuctionPost(media, authorId, groupId, location, description, title, contacts, price, expirationDate, delivery, reviews, reviewScore, buyerId, currentPriceLeader, currentBidPrice, minimumBidPrice, false);
        }

        public Post CreateDonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink)
        {
            User? user = users.FindById(authorId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.HasAccessToSell(groupId))
            {
                throw new Exception("User can't sell in this group!");
            }
            SellingUserScore? score = user.sellingUserScores.Find(score => score.GroupId == groupId);
            if (score == null || score.Score < 3.75)
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
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Views > 10 && post.Views / 2 <= post.Reports.Count)
                return true;
            return false;
        }

        public void RemoveConfirmation(Guid postID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Confirmed = false;
        }

        public void ConfirmPost(Guid postID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Confirmed = true;
        }

        public void AddReport(Guid postID, Guid userID, string reason)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Reports.Add(new Report(userID, postID, reason));
        }

        public void RemoveReport(Guid postID, Guid userID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.Reports.RemoveAll(report => report.UserId == userID);
        }

        public bool CheckIfAuctionTimeEnded(Guid postID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != "AuctionPost")
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
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != "AuctionPost")
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
            if ((auctionPost.ExpirationDate - DateTime.Now).TotalSeconds < 30)
                auctionPost.Add30SecondsToExpirationDate();
        }

        public void Donate(Guid postID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != "DonationPost")
            {
                throw new Exception("Post is not a donation post");
            }
            DonationPost donationPost = (DonationPost)post;
            donationPost.Donate();
        }

        public void EndAuctionDueToTime(Guid postID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != "AuctionPost")
            {
                throw new Exception("Post is not an auction post");
            }
            AuctionPost auctionPost = (AuctionPost)post;
            auctionPost.OnGoing = false;
        }

        public void EndAuctionExplicitly(Guid postID, Guid userID)
        {

            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.Type != "AuctionPost")
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
            List<Post> fixedPricePosts = posts.GetAllPosts().FindAll(p => p.Type == "FixedPricePost");
            fixedPricePosts.ForEach(p =>
            {
                if (p.CreationDate <= threeMonthsAgo)
                    posts.RemovePost(p.Id);
            });
        }

        public IEnumerable<Post> GetPostsByFavorites(List<Post> postsForGroup)
        {
            return postsForGroup.OrderByDescending(post => post.UsersThatFavorited.Count);
        }


        public void ToggleInterest(Guid postID, Guid userID, bool interested)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            InterestStatus? status = post.InterestStatuses.Find(status => status.InterestedUserId == userID);
            if (status == null)
            {
                post.InterestStatuses.Add(new InterestStatus(userID, postID, interested));
            }
            else
            {
                if (status.Interested == interested)
                    post.RemoveInterestStatus(userID);
                else
                    post.ToggleInterestStatus(userID);
            }
        }

        public void PromotePost(Guid postID, Guid userID, Guid groupID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.AuthorId != userID)
            {
                throw new Exception("User is not the author of the post");
            }
            Group? group = groups.FindById(groupID);
            if (group == null)
            {
                throw new Exception("That group does not exist");
            }
            if (group.TopSellers.Contains(userID))
            {
                post.Promoted = true;
                group.RemoveTopSeller(userID);
            }
            else
                throw new Exception("User is not a big seller in the group");
        }

        public void FavoritePost(Guid postID, Guid userID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.UsersThatFavorited.Add(userID);
        }

        public void UnfavoritePost(Guid postID, Guid userID)
        {
            Post? post = posts.GetPostById(postID);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.UsersThatFavorited.Remove(userID);
        }
    }
}
