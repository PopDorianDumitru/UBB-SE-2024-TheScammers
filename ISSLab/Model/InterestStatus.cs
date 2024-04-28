using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class InterestStatus
    {
        private Guid _interestedUserId;
        private Guid _postId;
        private Guid _interestStatusId;
        private bool _interested;

        public InterestStatus(Guid interestedUserId, Guid postId, bool interested)
        {
            this._interestedUserId = interestedUserId;
            this._postId = postId;
            this._interestStatusId = new Guid();
            this._interested = interested;
        }

        public InterestStatus()
        {
            this._interestedUserId = Guid.NewGuid();
            this._postId = Guid.NewGuid();
            this._interestStatusId = Guid.NewGuid();
            this._interested = false;
        }

        public Guid InterestedUserId { get => _interestedUserId; }
        public Guid PostId { get => _postId; }
        public Guid InterestStatusId { get => _interestStatusId; }
        public bool Interested { get => _interested; }
        public bool ToggleInterested()
        {
            _interested = !_interested;
            return _interested;
        }

    }
}
