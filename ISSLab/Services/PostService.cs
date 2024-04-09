using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
namespace ISSLab.Services
{
    class PostService
    {
        List<Post> posts;
        public PostService()
        {
            posts = new List<Post>();
        }
        public PostService(List<Post> posts)
        {
            this.posts = posts;
        }

        public List<Post> GetPosts()
        {
               return posts;
        }
        public void AddPost(Post post)
        {
            posts.Add(post);
        }
        public void RemovePost(Post post)
        {
            posts.Remove(post);
        }
        public Post GetPostById(Guid id)
        {
            return posts.Find(post => post.Id == id);
        }
        public Post CreatePost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string type)
        {
            return new Post(media, authorId, groupId, location, description, title, contacts, type);
        }
    }
}
