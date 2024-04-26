using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Post
    {
        private Guid _id;
        private int _views;
        private List<Guid> _usersThatShared;
        private List<Guid> _usersThatLiked;
        private List<Comment> _comments;
        private string _mediaContent;
        private DateTime _creationDate;
        private Guid _authorId;
        private Guid _groupId;
        private bool _promoted;
        private List<Guid> _usersThatFavorited;
        private List<Report> _reports;
        private string _itemLocation;
        private bool _confirmed;
        private string _description;
        private string _title;
        private List<InterestStatus> _interestStatuses;
        private string _contacts;
        private string _type;


        public Post(string mediaContent, Guid authorId, Guid groupId, string itemLocation, string description, string title, string contacts, string type, bool confirmed)
        {
            this._confirmed = confirmed;
            this._id = Guid.NewGuid();
            this._usersThatShared = new List<Guid>();
            this._usersThatLiked = new List<Guid>();
            this._comments = new List<Comment>();
            this._mediaContent = mediaContent;
            this._creationDate = DateTime.Now;
            this._authorId = authorId;
            this._groupId = groupId;
            this._promoted = false;
            this._usersThatFavorited = new List<Guid>();
            this._itemLocation = itemLocation;
            this._description = description;
            this._title = title;
            this._views = 0;
            this._interestStatuses = new List<InterestStatus>();
            this._contacts = contacts;
            this._reports = new List<Report>();
            this._type = type;
        }

        public Post(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string mediaContent, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string itemLocation, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, string type, bool confirmed, int views)
        {
            this._id = id;
            this._usersThatShared = usersThatShared;
            this._usersThatLiked = usersThatLiked;
            this._comments = comments;
            this._mediaContent = mediaContent;
            this._creationDate = creationDate;
            this._authorId = authorId;
            this._groupId = groupId;
            this._promoted = promoted;
            this._usersThatFavorited = usersThatFavorited;
            this._itemLocation = itemLocation;
            this._description = description;
            this._title = title;
            this._interestStatuses = interestStatuses;
            this._contacts = contacts;
            this._reports = reports;
            this._type = type;
            this._views = views;
            this._confirmed = confirmed;
        }

        public Post()
        {
            this._id = Guid.NewGuid();
            this._usersThatShared = new List<Guid>();
            this._usersThatLiked = new List<Guid>();
            this._comments = new List<Comment>();
            this._reports = new List<Report>();
            this._mediaContent = "";
            this._creationDate = DateTime.Now;
            this._authorId = Guid.NewGuid();
            this._groupId = Guid.NewGuid();
            this._promoted = false;
            this._usersThatFavorited = new List<Guid>();
            this._itemLocation = "";
            this._description = "";
            this._title = "";
            this._interestStatuses = new List<InterestStatus>();
            this._contacts = "";
            this._type = Constants.DEFAULT_POST_TYPE;
            this._confirmed = false;
        }

        public int Views { get => _views; set => _views = value; }
        public string Type { get => _type; set => _type = value; }

        public Guid Id { get => _id; }
        public List<Guid> UsersThatShared { get => _usersThatShared; }
        public List<Guid> UsersThatLiked { get => _usersThatLiked; }
        public List<Comment> Comments { get => _comments; }
        public string MediaContent { get => _mediaContent; set => _mediaContent = value; }
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }
        public Guid AuthorId { get => _authorId; }
        public Guid GroupId { get => _groupId; }
        public bool Promoted { get => _promoted; set => _promoted = value; }
        public List<Guid> UsersThatFavorited { get => _usersThatFavorited; }
        public List<Report> Reports { get => _reports; }
        public string ItemLocation { get => _itemLocation; set => _itemLocation = value; }
        public string Description { get => _description; set => _description = value; }
        public string Title { get => _title; set => _title = value; }
        public List<InterestStatus> InterestStatuses { get => _interestStatuses; }
        public string Contacts { get => _contacts; set => _contacts = value; }

        public bool Confirmed { get => _confirmed; set => _confirmed = value; }

        public void AddReport(Report report)
        {
            _reports.Add(report);
        }
        public void RemoveReport(Guid userId)
        {
            _reports.RemoveAll(userOnTrial => userOnTrial.UserId == userId);
        }

        public void ToggleFavorite(Guid userId)
        {
            if (_usersThatFavorited.Contains(userId))
            {
                _usersThatFavorited.Remove(userId);
            }
            else
            {
                _usersThatFavorited.Add(userId);
            }
        }

        public void ToggleLike(Guid userId)
        {
            if (_usersThatLiked.Contains(userId))
                _usersThatLiked.Remove(userId);
            else
                _usersThatLiked.Add(userId);
        }

        public void ToggleShare(Guid userId)
        {
            if (_usersThatShared.Contains(userId))
            {
                _usersThatShared.Remove(userId);
            }
            else
            {
                _usersThatShared.Add(userId);
            }
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
        }

        public int InterestLevel()
        {
            return _interestStatuses.FindAll(checkedInterestStatus => checkedInterestStatus.Interested).Count
                - _interestStatuses.FindAll(checkedInterestStatus => !checkedInterestStatus.Interested).Count;
        }

        public void AddInterestStatus(InterestStatus interestStatus)
        {
            _interestStatuses.Add(interestStatus);
        }

        public void RemoveInterestStatus(Guid userId)
        {
            _interestStatuses.RemoveAll(checkedInterestStatus => checkedInterestStatus.InterestedUserId == userId);
        }
        public void ToggleInterestStatus(Guid userId)
        {
            int indexOfUsersInterestStatus = _interestStatuses.FindIndex(checkedInterestStatus => checkedInterestStatus.InterestedUserId == userId);
            if (indexOfUsersInterestStatus == -1)
                throw new Exception("Interest status not found");
            else
                _interestStatuses[indexOfUsersInterestStatus].ToggleInterested();
        }

    }

}
