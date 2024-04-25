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
    public class DonationPost : Post
    {
        private float reviewScore;
        private double currentDonationAmount;
        private string donationPageLink;

        public DonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink, string type, bool confirmed) : base(media, authorId, groupId, location, description, title, contacts, type, confirmed)
        {
            this.currentDonationAmount = 0;
            this.donationPageLink = donationPageLink;
            this.reviewScore = 0;
        }

        public DonationPost() : base()
        {
            this.currentDonationAmount = 0;
            this.donationPageLink = "";
            this.reviewScore = 0;
        }

        public DonationPost(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string media, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string location, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, float reviewScore, double currentDonationAmount, string donationPageLink, string type, bool confirmed, int views) : base(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interestStatuses, contacts, reports, type, confirmed, views)
        {
            this.reviewScore = reviewScore;
            this.currentDonationAmount = currentDonationAmount;
            this.donationPageLink = donationPageLink;   

        }

        public void AddReview(Review review)
        {
        }
        public void RemoveReview(Review review)
        {
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

        public void Donate()
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
