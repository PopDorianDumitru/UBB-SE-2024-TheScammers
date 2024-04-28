using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.Model
{
    public class Constants
    {
        public const string EMPTY_STRING = "";

        public const string AUCTION_POST_TYPE = "Auction";
        public const string EXTENDED_AUCTION_POST_TYPE = "AuctionPost";
        public const string DONATION_POST_TYPE = "Donation";
        public const string EXTENDED_DONATION_POST_TYPE = "DonationPost";
        public const string FIXED_PRICE_POST_TYPE = "FixedPrice";
        public const string EXTENDED_FIXED_PRICE_POST_TYPE = "FixedPricePost";
        public const string DEFAULT_POST_TYPE = "post";

        public const string SELLING_MESSAGE_DEFAULT_CONTENT = "SELLING POST: " + "";
        public const string BUYING_MESSAGE_DEFAULT_CONTENT = "I'm interested in buying your product!";

        public const string VISIBLE_VISIBILITY = "Visible";
        public const string COLLAPSED_VISIBILITY = "Collapsed";

        public const string DOLLAR_SIGN = "$";

        public const int EXPIRATION_DATE_SLIGHT_PROLONGMENT_THRESHOLD_IN_SECONDS = 30;
    }
}
