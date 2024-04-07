using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    class Favorites
    {
        private Guid userId;
        private Guid postId;
        private List<Post> posts;
        public Favorites(Guid userId, Guid postId)
        {
            this.userId = userId;
            this.postId = postId;
            this.posts = new List<Post>();
        }

        public Favorites(Guid userId, Guid postId, List<Post> posts)
        {
            this.userId = userId;
            this.postId = postId;
            this.posts = posts;
        }

        public Favorites()
        {
            this.userId = Guid.NewGuid();
            this.postId = Guid.NewGuid();
            this.posts = new List<Post>();
        }

        public Guid UserId { get => userId; }
        public Guid PostId { get => postId; }
        public List<Post> Posts { get => posts; }
        public void addPost(Post post)
        {
            if(this.posts.Contains(post))
                throw new Exception("Post already in favorites");
            posts.Add(post);
        }
        public void removePost(Post post)
        {
            if(!this.posts.Contains(post))
                throw new Exception("Post not in favorites");
            posts.Remove(post);
        }
    }
}
