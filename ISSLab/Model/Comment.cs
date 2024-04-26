using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Comment
    {
        private Guid _commentId;
        private string _content;
        private Guid _userId;
        private List<Comment> _replies;

        public Comment(Guid userId, string content)
        {
            this._commentId = Guid.NewGuid();
            this._userId = userId;
            this._content = content;
            this._replies = new List<Comment>();
        }

        public Comment()
        {
            this._commentId = Guid.NewGuid();
            this._userId = Guid.NewGuid();
            this._content = Constants.EMPTY_STRING;
            this._replies = new List<Comment>();
        }

        public Comment(Guid commentId, Guid userId, string content, List<Comment> replies)
        {
            this._commentId = commentId;
            this._userId = userId;
            this._content = content;
            this._replies = replies;
        }

        public Guid CommentId { get => _commentId; }
        public Guid UserId { get => _userId; }
        public string Content { get => _content; set => _content = value; }
        public List<Comment> Replies { get => _replies; }

        public void AddReply(Comment reply)
        {
            _replies.Add(reply);
        }

    }
}
