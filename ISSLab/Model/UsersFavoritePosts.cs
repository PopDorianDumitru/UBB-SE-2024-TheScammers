using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class UsersFavoritePosts
    {
        private Guid _userId;
        private Guid _groupId;
        private List<Guid> _posts;

        public UsersFavoritePosts(Guid userId, Guid groupId)
        {
            this._userId = userId;
            this._groupId = groupId;
            this._posts = new List<Guid>();
        }

        public UsersFavoritePosts(Guid userId, Guid groupId, List<Guid> posts)
        {
            this._userId = userId;
            this._groupId = groupId;
            this._posts = posts;
        }

        public UsersFavoritePosts()
        {
            this._userId = Guid.NewGuid();
            this._groupId = Guid.NewGuid();
            this._posts = new List<Guid>();
        }

        public Guid UserId { get => _userId; }
        public Guid GroupId { get => _groupId; }
        public List<Guid> Posts { get => _posts; }

        public void AddPost(Guid post)
        {
            if (this._posts.Contains(post))
                throw new Exception("Post already in favorites");
            _posts.Add(post);
        }

        public void RemovePost(Guid post)
        {
            if (!this._posts.Contains(post))
                throw new Exception("Post not in favorites");
            _posts.Remove(post);
        }
    }
}
