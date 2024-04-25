using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class InterestStatus
    {
        private Guid interestedUserId;
        private Guid postId;
        private Guid interestStatusId;
        private bool interested;

        public InterestStatus(Guid interestedUserId, Guid postId, bool interested)
        {
            this.interestedUserId = interestedUserId;
            this.postId = postId;
            this.interestStatusId = new Guid();
            this.interested = interested;
        }

        public InterestStatus()
        {
            this.interestedUserId = Guid.NewGuid();
            this.postId = Guid.NewGuid();
            this.interestStatusId = Guid.NewGuid();
            this.interested = false;
        }

        public Guid InterestedUserId { get => interestedUserId; }
        public Guid PostId { get => postId; }
        public Guid InterestStatusId { get => interestStatusId; }
        public bool Interested { get => interested; }
        public bool ToggleInterested()
        {
            interested = !interested;
            return interested;
        }



    }
}
