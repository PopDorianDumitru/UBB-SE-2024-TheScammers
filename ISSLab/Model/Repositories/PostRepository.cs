using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Lab3_1;
using ISSLab.Services;

namespace ISSLab.Model.Repositories
{
    public class PostRepository : IPostRepository
    {
        private List<Post> allPosts;

        public PostRepository()
        {
            this.allPosts = new List<Post>();
        }

        public void AddPost(Post newPost)
        {
            allPosts.Add(newPost);
        }

        public void RemovePost(Guid id)
        {
            for (int i = 0; i < allPosts.Count; i++)
            {
                if (allPosts[i].Id == id)
                {
                    allPosts.RemoveAt(i);
                    break;
                }
            }
        }

        public List<Post> GetAllPosts()
        {
            return allPosts;
        }

        public Post GetPostById(Guid postId)
        {
            for (int i = 0; i < allPosts.Count; i++)
            {
                if (allPosts[i].Id == postId)
                {
                    return allPosts[i];
                }
            }
            throw new Exception("Post does not exist!");
        }
    }
}
