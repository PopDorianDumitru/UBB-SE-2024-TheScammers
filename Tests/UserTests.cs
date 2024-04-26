using System;
using ISSLab.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class UserTests
    {

        User userEmptyConstructor;

        User userSimpleConstructor;
        string usernameSimpleConstructor;
        string realNameSimpleConstructor;
        DateOnly dateOfBirthSimpleConstructor;
        string profilePictureSimpleConstructor;
        string passwordSimpleConstructor;

        User userFullConstructor;
        Guid idFullConstructor;
        string usernameFullConstructor;
        string realNameFullConstructor;
        DateOnly dateOfBirthFullConstructor;
        string profilePictureFullConstructor;
        string passwordFullConstructor;
        DateTime creationDateFullConstructor;
        List<Guid> groupsWithSellingPrivilegeFullConstructor;
        List<Guid> groupsWithActiveRequestToSellFullConstructor;
        List<SellingUserScore> userScoresFullConstructor;
        List<Cart> cartsFullConstructor;
        List<Favorites> favoritesFullConstructor;
        List<Guid> groupsFullConstructor;
        List<Review> receivedReviewsFullConstructor;
        int numberOfSalesFullConstructor;


        SellingUserScore scoreToAddAndRemove;
        Guid groupToUseForAccessTests;

        [SetUp]
        public void Setup()
        {
            userEmptyConstructor = new User();

            usernameSimpleConstructor = "Test";
            realNameSimpleConstructor = "Test";
            dateOfBirthSimpleConstructor = DateOnly.FromDateTime(DateTime.Now);
            profilePictureSimpleConstructor = "Test";
            passwordSimpleConstructor = "Test";

            userSimpleConstructor = new User(usernameSimpleConstructor, realNameSimpleConstructor, dateOfBirthSimpleConstructor, profilePictureSimpleConstructor, passwordSimpleConstructor);

            idFullConstructor = new Guid();
            usernameFullConstructor = "Test";
            realNameFullConstructor = "Test";
            dateOfBirthFullConstructor = DateOnly.FromDateTime(DateTime.Now);
            profilePictureFullConstructor = "Test";
            passwordFullConstructor = "Test";
            creationDateFullConstructor = DateTime.Now;
            groupsWithSellingPrivilegeFullConstructor = new List<Guid>();
            groupsWithActiveRequestToSellFullConstructor = new List<Guid>();
            userScoresFullConstructor = new List<SellingUserScore>();
            cartsFullConstructor = new List<Cart>();
            favoritesFullConstructor = new List<Favorites>();
            groupsFullConstructor = new List<Guid>();
            receivedReviewsFullConstructor = new List<Review>();
            numberOfSalesFullConstructor = 0;

            userFullConstructor = new User(idFullConstructor, usernameFullConstructor, realNameFullConstructor, dateOfBirthFullConstructor, profilePictureFullConstructor, passwordFullConstructor, creationDateFullConstructor, groupsWithSellingPrivilegeFullConstructor, groupsWithActiveRequestToSellFullConstructor, userScoresFullConstructor, cartsFullConstructor, favoritesFullConstructor, groupsFullConstructor, receivedReviewsFullConstructor, numberOfSalesFullConstructor);

            scoreToAddAndRemove = new SellingUserScore();
            groupToUseForAccessTests = new Guid();
           
        }

        [Test]
        public void UserIdGet_GetIdOfFullConstructorUser_IdMatches()
        {
            Assert.That(userFullConstructor.Id, Is.EqualTo(idFullConstructor));
        }

        [Test]
        public void UserUsernameGet_GetUsernameOfFullConstructorUser_UsernameMatches()
        {
            Assert.That(userFullConstructor.Username, Is.EqualTo(usernameFullConstructor));
        }


        [Test]
        public void UserUsernameSet_SetUsernameOfFullConstructorUser_NewUsernameMatches()
        {
            userFullConstructor.Username = "Test2";
            usernameFullConstructor = "Test2";
            Assert.That(userFullConstructor.Username, Is.EqualTo(usernameFullConstructor));
        }

        [Test]
        public void UserRealNameGet_GetRealNameOfFullConstructorUser_RealNameMatches()
        {
            Assert.That(userFullConstructor.RealName, Is.EqualTo(realNameFullConstructor));
        }


        [Test]
        public void UserRealNameSet_SetRealNameOfFullConstructorUser_NewRealNameMatches()
        {
            userFullConstructor.RealName = "Test2";
            realNameFullConstructor = "Test2";
            Assert.That(userFullConstructor.RealName, Is.EqualTo(realNameFullConstructor));
        }

        [Test]
        public void UserDateOfBirthGet_GetDateOfBirthOfFullConstructorUser_DateOfBirthMatches()
        {
            Assert.That(userFullConstructor.DateOfBirth, Is.EqualTo(dateOfBirthFullConstructor));
        }

        [Test]
        public void UserDateOfBirthSet_SetDateOfBirthOfFullConstructorUser_NewDateOfBirthMatches()
        {
            DateOnly newDate = userFullConstructor.DateOfBirth.AddDays(-1);
            dateOfBirthFullConstructor = newDate;
            userFullConstructor.DateOfBirth = newDate;
            Assert.That(userFullConstructor.DateOfBirth, Is.EqualTo(dateOfBirthFullConstructor));

        }

        [Test]
        public void UserProfilePictureGet_GetProfilePictureOfFullConstructorUser_ProfilePictureMatches()
        {
            Assert.That(userFullConstructor.ProfilePicture, Is.EqualTo(profilePictureFullConstructor));
        }

        [Test]
        public void UserProfilePictureSet_SetProfilePictureOfFullConstructorUser_NewProfilePictureMatches()
        {
            userFullConstructor.ProfilePicture = "Test2";
            profilePictureFullConstructor = "Test2";
            Assert.That(userFullConstructor.ProfilePicture, Is.EqualTo(profilePictureFullConstructor));
        }

        [Test]
        public void UserPasswordGet_GetPasswordOfFullConstructorUser_PasswordMatches()
        {
            Assert.That(userFullConstructor.Password, Is.EqualTo(passwordFullConstructor));
        }

        [Test]
        public void UserPasswordSet_SetPasswordOfFullConstructorUser_NewPasswordMatches()
        {
            userFullConstructor.Password = "Test2";
            passwordFullConstructor = "Test2";
            Assert.That(userFullConstructor.Password, Is.EqualTo(passwordFullConstructor));
        }

        [Test]
        public void UserSellingUserScoresGet_GetSellingUserScoresOfFullConstructorUser_SellingUserScoresMatches()
        {
            Assert.That(userFullConstructor.sellingUserScores, Is.EqualTo(userScoresFullConstructor));
        }

        [Test]
        public void UserCreationDateGet_GetCreationDateOfFullConstructorUser_CreationDateMatches()
        {
            Assert.That(userFullConstructor.CreationDate, Is.EqualTo(creationDateFullConstructor));
        }

        [Test]
        public void UserCartsGet_GetCartsOfFullConstructorUser_CartsMatch()
        {
            Assert.That(userFullConstructor.Carts, Is.EqualTo(cartsFullConstructor));
        }

        [Test]
        public void UserFavoritesGet_GetFavouritesOfFullConstructorUser_FavoritesMatch()
        {
            Assert.That(userFullConstructor.Favorites, Is.EqualTo(favoritesFullConstructor));
        }

        [Test]
        public void UserGroupsGet_GetGroupsOfFullConstructorUser_GroupsMatch()
        {
            Assert.That(userFullConstructor.Groups, Is.EqualTo(groupsFullConstructor));
        }

        [Test]
        public void UserReviewsGet_GetReviewsOfFullConstructorUser_ReviewsMatch()
        {
            Assert.That(userFullConstructor.Reviews, Is.EqualTo(receivedReviewsFullConstructor));
        }

        [Test]
        public void UserGroupsWithSellingPrivilege_GetGroupsWithSellingPrivilegeOfFullConstructorUser_GroupsMatch()
        {
            Assert.That(userFullConstructor.GroupsWithSellingPrivilege, Is.EqualTo(groupsWithSellingPrivilegeFullConstructor));
        }

        [Test]
        public void UserGroupsWithActiveRequest_GetGroupsWithActiveRequestOfFullConstructorUser_GroupsMatch()
        {
            Assert.That(userFullConstructor.GroupsWithActiveRequestToSell, Is.EqualTo(groupsWithActiveRequestToSellFullConstructor));
        }

        [Test]
        public void UserProfilePictureImageSourceGet_GetProfilePictureImageSourceFromFullConstructorUser_ReturnsImage()
        {
            userFullConstructor.ProfilePicture = "http://www.thewowstyle.com/wp-content/uploads/2015/04/cat1.jpg";
            Assert.That(userFullConstructor.ProfilePictureImageSource, Is.Not.Null);
        }

        [Test]
        public void UserProfilePictureImageSourceGet_GetProfilePictureImageSourceWithFaultyLink_ReturnsNull()
        {
            userFullConstructor.ProfilePicture = "Test";
            Assert.That(userFullConstructor.ProfilePictureImageSource, Is.Null);

        }



        [Test]
        public void UserAddCart_AddEmptyCartToFullConstructorUser_CartAdded()
        {
            Cart newCart = new Cart();
            userFullConstructor.AddCart(newCart);
            Assert.That(userFullConstructor.Carts[0], Is.EqualTo(newCart));
        }

        [Test]
        public void UserAddFavourites_AddEmptyFavouritesToFullConstructorUser_FavoritesAdded()
        {
            Favorites favorites = new Favorites();
            userFullConstructor.AddFavorites(favorites);
            Assert.That(userFullConstructor.Favorites[0], Is.EqualTo(favorites));

        }

        [Test]
        public void UserAddGroup_AddGroupToFullConstructorUser_GroupAdded()
        {
            Guid group = new Guid();
            userFullConstructor.AddGroup(group);
            Assert.That(userFullConstructor.Groups[0], Is.EqualTo(group));

        }

        [Test]
        public void UserAddReview_AddReviewToFullConstructorUser_ReviewAdded()
        {
            Review review = new Review();
            userFullConstructor.AddReview(review);
            Assert.That(userFullConstructor.Reviews[0], Is.EqualTo(review));
        }

        [Test]
        public void UserAddUserScore_AddUserScoreToFullConstructorUser_UserScoreAdded()
        {
            userFullConstructor.AddNewUserScore(scoreToAddAndRemove);
            Assert.That(userFullConstructor.sellingUserScores[0], Is.EqualTo(scoreToAddAndRemove));
        }

        [Test]
        public void UserSetNumberOfSales_SetNumberOfSalesOfFullConstructorUser_NumberOfSalesUpdates()
        {
            userFullConstructor.NumberOfSales = 1;
            Assert.That(userFullConstructor.NumberOfSales, Is.EqualTo(1));
        }

        [Test]
        public void UserRemoveUserScore_RemoveUserScoreFromFullConstructorUser_ScoreRemoved()
        {
            userFullConstructor.RemoveUserScore(scoreToAddAndRemove);
            Assert.That(userFullConstructor.sellingUserScores.Count, Is.EqualTo(0));
        }

        [Test]
        public void UserRequestSellingAccess_RequestSellingAccessToPresetGroup_RequestSucceeds()
        {
            userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
            Assert.That(userFullConstructor.GroupsWithActiveRequestToSell[0], Is.EqualTo(groupToUseForAccessTests));
        }

        [Test]
        public void UserRequestSellingAccess_RequestSellingAccessToAlreadyRequestedGroup_RaisesException()
        {
            try
            {
                userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
                Assert.IsTrue(userFullConstructor.GroupsWithActiveRequestToSell.Contains(groupToUseForAccessTests));
                userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.That(exception.Message, Is.EqualTo("Already requested access to sell in this group"));
            }
        }

        [Test]
        public void UserGiveAccessToSell_GiveAccessToRequest_AccessGranted()
        {
            userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
            userFullConstructor.GroupsWithSellingPrivilege.Contains(groupToUseForAccessTests);
        }

        [Test]
        public void UserGiveAccessToSell_GiveAccessToAlreadyExistingGroup_RaisesException()
        {
            try
            {
                userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
                userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.That(exception.Message, Is.EqualTo("You can already sell in this group"));
            }
        }

        [Test]
        public void UserRequestSellingAccess_RequestSellingAccessToAlreadyPermittedGroup_RaisesException()
        {
            try {
                userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
                userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
                userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.That(exception.Message, Is.EqualTo("Already have access to sell in this group"));
            }

        }

        [Test]
        public void UserHasAccessToSell_CheckIfUserHasAccessToPreset_ReturnsTrue()
        {
            userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
            userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
            Assert.IsTrue(userFullConstructor.HasAccessToSellInGroup(groupToUseForAccessTests));
        }

        [Test]
        public void UserHasAccessToSell_CheckIfUserHasAccessToRandom_ReturnsFalse()
        {
            Assert.IsFalse(userFullConstructor.HasAccessToSellInGroup(new Guid()));
        }

        [Test]
        public void UserTakeAwayAccessToSell_TakeAwayAccessFromPreset_TakingAwayWorks()
        {
            userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
            userFullConstructor.GiveAccessToSellInGroup(groupToUseForAccessTests);
            userFullConstructor.TakeAwayAccessToSellInGroup(groupToUseForAccessTests);
            Assert.That(userFullConstructor.GroupsWithSellingPrivilege.Count, Is.EqualTo(0));
        }

        [Test]
        public void UserTakeAwayAccessToSell_TakeAwayAccessFromRandomGroup_RaisesException()
        {
            try
            {
                userFullConstructor.TakeAwayAccessToSellInGroup(new Guid());
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.That(exception.Message, Is.EqualTo("No access to sell in this group"));
            }
        }

        [Test]
        public void UserDenyAccessToRequest_DenyAccessToNonExistingRequest_RaisesException()
        {
            try
            {
                userFullConstructor.DenyAccessToSellInGroup(new Guid());
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.That(exception.Message, Is.EqualTo("No active request to sell in this group"));
            }
        }

       

        [Test]
        public void UserDenyAccessToRequest_DenyAccessToPresetRequest_AccessDenied()
        {
            userFullConstructor.RequestSellingAccess(groupToUseForAccessTests);
            userFullConstructor.DenyAccessToSellInGroup(groupToUseForAccessTests);
            Assert.IsFalse(userFullConstructor.GroupsWithActiveRequestToSell.Contains(groupToUseForAccessTests));
            Assert.IsFalse(userFullConstructor.GroupsWithSellingPrivilege.Contains(groupToUseForAccessTests));
        }

    }
}
