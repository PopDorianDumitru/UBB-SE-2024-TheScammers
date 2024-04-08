using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ISSLab.Model
{
    class DonationPost : Post
    {
        private float reviewScore;
        private List<Review> reviews;
        private double currentDonationAmount;
        private string donationPageLink;

        public DonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink, string type) : base(media, authorId, groupId, location, description, title, contacts, type)
        {
            this.currentDonationAmount = 0;
            this.donationPageLink = donationPageLink;
            this.reviewScore = 0;
            this.reviews = new List<Review>();
        }

        public DonationPost() : base()
        {
            this.currentDonationAmount = 0;
            this.donationPageLink = "";
            this.reviewScore = 0;
            this.reviews = new List<Review>();
        }

        public DonationPost(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string media, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string location, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, float reviewScore, List<Review> reviews, double currentDonationAmount, string donationPageLink, string type) : base(id, usersThatShared, usersThatLiked, comments, media, creationDate,  authorId,  groupId,  promoted,  usersThatFavorited,  location,  description,  title, interestStatuses,  contacts, reports, type)
        {
            this.reviewScore = reviewScore;
            this.reviews = reviews;
            this.currentDonationAmount = currentDonationAmount;
            this.donationPageLink = donationPageLink;   

        }


        public void addReview(Review review)
        {
            if (reviews.Contains(review))
            {
                throw new Exception("Review already exists. Edit the existing one if you want");
            }
            reviews.Add(review);
        }
        public void removeReview(Review review)
        {
            if (!reviews.Contains(review))
            {
                throw new Exception("Review does not exist");
            }
            reviews.Remove(review);
        }

        public float ReviewScore
        {
            get { return reviewScore; }
            set { reviewScore = value; }
        }
        public double DonationAmount
        {
            get { return currentDonationAmount; }
            set { currentDonationAmount = value; }
        }

        public string DonationPageLink
        {
            get { return donationPageLink; }
            set { donationPageLink = value; }
        }

        void donate()
        {
            try
            {
                System.Diagnostics.Process.Start(donationPageLink);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid URL");   
            }
        }


    }
}
