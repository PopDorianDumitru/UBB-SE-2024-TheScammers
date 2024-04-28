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
        private IPostRepository postRepository;

        public PostService(IPostRepository posts)
        {
            this.postRepository = posts;
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
            Post? postWithThatId = postRepository.GetPostById(id);
            if (postWithThatId == null)
            {
                throw new Exception("Post not found");
            }
            return postWithThatId;
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
