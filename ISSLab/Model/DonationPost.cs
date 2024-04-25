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
        private float _reviewScore;
        private double _currentDonationAmount;
        private string _donationPageLink;

        public DonationPost(string media, Guid authorId, Guid groupId, string location, string description, string title, string contacts, string donationPageLink, string type, bool confirmed) : base(media, authorId, groupId, location, description, title, contacts, type, confirmed)
        {
            this._currentDonationAmount = 0;
            this._donationPageLink = donationPageLink;
            this._reviewScore = 0;
        }

        public DonationPost() : base()
        {
            this._currentDonationAmount = 0;
            this._donationPageLink = "";
            this._reviewScore = 0;
        }

        public DonationPost(Guid id, List<Guid> usersThatShared, List<Guid> usersThatLiked, List<Comment> comments, string media, DateTime creationDate, Guid authorId, Guid groupId, bool promoted, List<Guid> usersThatFavorited, string location, string description, string title, List<InterestStatus> interestStatuses, string contacts, List<Report> reports, float reviewScore, double currentDonationAmount, string donationPageLink, string type, bool confirmed, int views) : base(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interestStatuses, contacts, reports, type, confirmed, views)
        {
            this._reviewScore = reviewScore;
            this._currentDonationAmount = currentDonationAmount;
            this._donationPageLink = donationPageLink;

        }

        public void AddReview(Review review)
        {
        }
        public void RemoveReview(Review review)
        {
        }

        public float ReviewScore
        {
            get { return _reviewScore; }
            set { _reviewScore = value; }
        }
        public double DonationAmount
        {
            get { return _currentDonationAmount; }
            set { _currentDonationAmount = value; }
        }

        public string DonationPageLink
        {
            get { return _donationPageLink; }
            set { _donationPageLink = value; }
        }

        public void Donate()
        {
            try
            {
                System.Diagnostics.Process.Start(_donationPageLink);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid URL");
            }
        }
    }
}
