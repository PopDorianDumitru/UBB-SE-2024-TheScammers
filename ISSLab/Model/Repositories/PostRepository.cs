using ISSLab.Services;
using Lab3_1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
namespace ISSLab.Model.Repositories
{
    public class PostRepository : IPostRepository
    {
        private List<Post> _allPosts;

        public PostRepository()
        {
            this._allPosts = new List<Post>();
        }

        public void AddPost(Post newPost)
        {
            _allPosts.Add(newPost);
        }

        public void RemovePost(Guid id)
        {
            for (int i = 0; i < _allPosts.Count; i++)
            {
                if (_allPosts[i].Id == id)
                {
                    _allPosts.RemoveAt(i);
                    break;
                }
            }
        }

        public List<Post> GetAllPosts()
        {
            return _allPosts;
        }

        public Post GetPostById(Guid postId)
        {
            for (int i = 0; i < _allPosts.Count; i++)
            {
                if (_allPosts[i].Id == postId)
                {
                    return _allPosts[i];
                }
            }
            throw new Exception("Post does not exist!");
        }
    }
}
