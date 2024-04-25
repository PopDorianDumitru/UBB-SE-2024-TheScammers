using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class PostTests
    {
        public ISSLab.Model.Post firstConstructorPostForTest;
        public ISSLab.Model.Post secondConstructorPostForTest;
        public ISSLab.Model.Post thirdConstructorPostForTest;
        public Report reportToAdd;
        public Guid idUserToLike;
        public Guid idUserToFavorite;
        public Guid idUserToShare;
        public Guid idUserInterested1, idUserInterested2;
        public Comment commentToAdd1;
        public Comment commentToAdd2;
        public Comment commentToAdd3;
        public InterestStatus interestStatusToAdd1, interestStatusToAdd2, interestStatusToAdd3, interestStatusToAdd4;




        [SetUp]
        public void SetUp() {
            reportToAdd = new Report();
            idUserToLike = Guid.NewGuid();
            idUserToFavorite = Guid.NewGuid();
            idUserInterested1 = Guid.NewGuid();
            idUserInterested2 = Guid.NewGuid();
            idUserToShare = Guid.NewGuid();
            commentToAdd1 = new Comment();
            firstConstructorPostForTest = new Post("./cat.jpg", Guid.NewGuid(), Guid.NewGuid(), "Cluj", "description1", "title1", "contacts1", "", true);
            secondConstructorPostForTest = new Post(Guid.NewGuid(), new List<Guid>(), new List<Guid>(),
                new List<Comment>(), "./cat.jpg", DateTime.Parse("Jan 1, 2024"), Guid.NewGuid(), Guid.NewGuid(), false, new List<Guid>(), "Cluj", "description2", "title2",
                new List<InterestStatus>(), "0744444444", new List<Report>(), "type2", false, 100);
            thirdConstructorPostForTest = new Post();
            interestStatusToAdd1 = new InterestStatus(idUserInterested1, firstConstructorPostForTest.Id, true);
            interestStatusToAdd2 = new InterestStatus(idUserInterested1, secondConstructorPostForTest.Id, true);
            interestStatusToAdd3 = new InterestStatus(idUserInterested1, thirdConstructorPostForTest.Id, false);
            interestStatusToAdd4 = new InterestStatus(idUserInterested2, firstConstructorPostForTest.Id, false);



        }


        [Test]
        public void IdGet_GetTheOfPostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPostForTest.Id != new Guid());

        }

        [Test]
        public void IdGet_GetTheIdOfPostSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.True(secondConstructorPostForTest.Id != new Guid());

        }

        [Test]
        public void IdGet_GetTheIdOfPostThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.True(thirdConstructorPostForTest.Id != new Guid());

        }

        [Test]
        public void ViewsGet_getTheViewsOfPostFirstConstructor_ShouldBe100()
        {
            Assert.True(firstConstructorPostForTest.Views.Equals(0));
        }
        [Test]
        public void ViewsGet_getTheViewsPostSecondConstructor_ShouldBe_0()
        {
            Assert.True(secondConstructorPostForTest.Views.Equals(100));
        }
        [Test]
        public void ViewsGet_getTheViewsPostThirdConstructor_ShouldBe_0()
        {
            Assert.True(thirdConstructorPostForTest.Views.Equals(0));
        }
         [Test]
        public void UsersThatLikedGet_getUsersThatLikedOfPostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPostForTest.UsersThatLiked);
        }

        [Test]
        public void UsersThatLikedGet_getUsersThatLikedOfPostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPostForTest.UsersThatLiked);
        }
       
        [Test]
         public void UsersThatLikedGet_getUsersThatLikedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPostForTest.UsersThatLiked);
        }
        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPostForTest.UsersThatShared);
        }
        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPostForTest.UsersThatShared);
        }

        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPostForTest.UsersThatShared);
        }
        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPostForTest.UsersThatFavorited);
        }
        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPostForTest.UsersThatFavorited);
        }

        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPostForTest.UsersThatFavorited);
        }
        [Test]
        public void CommentsGet_getCommentOfPostFirstConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(firstConstructorPostForTest.Comments);
        }
        [Test]
        public void CommentsGet_getCommentsOfPostSecondConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(secondConstructorPostForTest.Comments);
        }

        [Test]
        public void CommentsGet_getUsersThatFavoritedOfPostThirdConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(thirdConstructorPostForTest.Comments);
        }

        [Test]
        public void ReportsGet_getReportsOfPostFirstConstructor_ShouldBeListReport()
        {
            Assert.IsInstanceOf<List<Report>>(firstConstructorPostForTest.Reports);
        }
        [Test]
        public void ReportsGet_getReportsOfPostSecondConstructor_ShouldBeListReport()
        {
            Assert.IsInstanceOf<List<Report>>(secondConstructorPostForTest.Reports);
        }

        [Test]
        public void ReportsGet_getReportsOfPostThirdConstructor_ShouldBeListReports()
        {
            Assert.IsInstanceOf<List<Report>>(thirdConstructorPostForTest.Reports);
        }
        [Test]
        public void MediaContentGet_getMediaContentOfPostFirstConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<String>(firstConstructorPostForTest.MediaContent);
            Assert.That(firstConstructorPostForTest.MediaContent == "./cat.jpg");
        }
        [Test]
        public void MediaContentGet_getMediaContentOfPostSecondConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<String>(secondConstructorPostForTest.MediaContent);
            Assert.That(secondConstructorPostForTest.MediaContent == "./cat.jpg");
        }
        [Test]
        public void MediaContentGet_getMediaContentOfPostThirdConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<String>(thirdConstructorPostForTest.MediaContent);
            Assert.That(thirdConstructorPostForTest.MediaContent == "");
        }
        [Test]
        public void CreationDateGet_getCreationDateOfPostFirstConstructor_ShouldBeDatetime()
        {
            Assert.IsInstanceOf<DateTime>(firstConstructorPostForTest.CreationDate);
        }
        [Test]
        
        public void CreationDateGet_getCreationDateOfPostThirdConstructor_ShouldBeDatetime()
        {
            Assert.IsInstanceOf<DateTime>(thirdConstructorPostForTest.CreationDate);

        }
        [Test]
        public void AuthorIdGet_GetTheAuthorIdOfPostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPostForTest.AuthorId != new Guid());

        }
        [Test]
        public void GroupIdGet_GetTheGroupIdOfPostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPostForTest.GroupId != new Guid());

        }
        [Test]
        public void ContactsSet_SetTheContactsOfPostFirstConstructor_ShouldBeEqualWith0755555555()
        {
            firstConstructorPostForTest.Contacts = "0755555555";
            Assert.True(firstConstructorPostForTest.Contacts=="0755555555");

        }
        [Test]
        public void ConfirmedSet_SetTheConfirmedOfPostFirstConstructor_ShouldBeEqualWithTrue()
        {
            firstConstructorPostForTest.Confirmed = true;
            Assert.True(firstConstructorPostForTest.Confirmed);

        }
        [Test]
        public void PromotedSet_SetTheContactsOfPostSecondConstructor_ShouldBeEqualWithTrue()
        {
            Assert.False(firstConstructorPostForTest.Promoted);
            firstConstructorPostForTest.Promoted = true;
            Assert.True(firstConstructorPostForTest.Promoted);


        }
        [Test]
        public void TitleSet_SetTheTitleOfPostFirstConstructor_ShouldBeEqualWithtitle1()
        {
           
            Assert.True(firstConstructorPostForTest.Title=="title1");
        }
        [Test]
        public void DescriptionSet_SetTheDescriptionOfPostFirstConstructor_ShouldBeEqualWithdescription1()
        {

            Assert.True(firstConstructorPostForTest.Description == "description1");


        }
        [Test]
        public void ItemLotionSet_SetTheItemLocationOfPostFirstConstructor_ShouldBeEqualWithBucuresti()
        {

            Assert.True(firstConstructorPostForTest.ItemLocation == "Cluj");
            firstConstructorPostForTest.ItemLocation = "Bucuresti";
            Assert.True(firstConstructorPostForTest.ItemLocation == "Bucuresti");

        }

        [Test]
        public void AddReport_AddReportToPostFirstConstructor_PostFirstConstructorShouldHaveOneReport()
        {
            firstConstructorPostForTest.AddReport(reportToAdd);
            Assert.That(firstConstructorPostForTest.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddReport_AddReportToPostSecondConstructor_PostSecondConstructorShouldHaveOneReport()
        {
            secondConstructorPostForTest.AddReport(reportToAdd);
            Assert.That(secondConstructorPostForTest.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddReport_AddReportToPostThirdConstructor_PostThirdConstructorShouldHaveOneReport()
        {
            thirdConstructorPostForTest.AddReport(reportToAdd);
            Assert.That(thirdConstructorPostForTest.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostFirstConstructor_PostFirstConstructorShouldHaveNoReports()
        {
            firstConstructorPostForTest.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            firstConstructorPostForTest.RemoveReport(userId);
            Assert.That(firstConstructorPostForTest.Reports, Has.Count.EqualTo(0));
          
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostSecondConstructor_PostSecondConstructorShouldHaveNoReports()
        {
            secondConstructorPostForTest.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            secondConstructorPostForTest.RemoveReport(userId);
            Assert.That(secondConstructorPostForTest.Reports, Has.Count.EqualTo(0));
           
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostThirdConstructor_PostThirdConstructorShouldHaveNoReports()
        {
            thirdConstructorPostForTest.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            thirdConstructorPostForTest.RemoveReport(userId);
            Assert.That(thirdConstructorPostForTest.Reports, Has.Count.EqualTo(0));
        }


        [Test]
        public void ToggleLike_OneUserLikesThePostFirstConstructor_NumberOfUsersThatLikedPostFirstConstructorShouldBe1()
        {
            firstConstructorPostForTest.ToggleLike(idUserToShare);
            Assert.That(firstConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleLike_OneUserLikesThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe1()
        {
            secondConstructorPostForTest.ToggleLike(idUserToShare);
            Assert.That(secondConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleLike_OneUserLikesThePostThirdConstructor_NumberOfUsersThatLikedPostThirdConstructorShouldBe1()
        {
            thirdConstructorPostForTest.ToggleLike(idUserToShare);
            Assert.That(thirdConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleLike_OneUserDoesNotSharesAnymoreThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe0()
        {
            firstConstructorPostForTest.ToggleLike(idUserToShare);
            firstConstructorPostForTest.ToggleLike(idUserToShare);
            Assert.That(firstConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleLike_OneUserDoesNotLikeAnymoreThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe0()
        {
            secondConstructorPostForTest.ToggleLike(idUserToLike);
            secondConstructorPostForTest.ToggleLike(idUserToLike);
            Assert.That(secondConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleLike_OneUserDoesNotLikeAnymoreThePostThirdConstructor_NumberOfUsersThatLikedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPostForTest.ToggleLike(idUserToLike);
            thirdConstructorPostForTest.ToggleLike(idUserToLike);
            Assert.That(thirdConstructorPostForTest.UsersThatLiked, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleFavorite_OneUserFavoritesThePostFirstConstructor_NumberOfUsersThatFavoritedPostFirstConstructorShouldBe1()
        {
            firstConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(firstConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleFavorites_OneUserFavoritesThePostSecondConstructor_NumberOfUsersThatFavoritedPostSecondConstructorShouldBe1()
        {
            secondConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(secondConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleFavorites_OneUserFavoritesThePostThirdConstructor_NumberOfUsersThatFavoritesPostThirdConstructorShouldBe1()
        {
            thirdConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(thirdConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleFavorites_OneUserDoesNotFavoriteAnymoreThePostFirstConstructor_NumberOfUsersThatFavoritedPostFirstConstructorShouldBe0()
        {
            firstConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            firstConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(firstConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleFavorite_OneUserDoesNotFavoriteAnymoreThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe0()
        {
            secondConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            secondConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(secondConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleFavorite_OneUserDoesNotFavoriteAnymoreThePostThirdConstructor_NumberOfUsersThatFavoritedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            thirdConstructorPostForTest.ToggleFavorite(idUserToFavorite);
            Assert.That(thirdConstructorPostForTest.UsersThatFavorited, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleShare_OneUserSharesThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe1()
        {
            firstConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(firstConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleShare_OneUserSharesThePostSecondConstructor_NumberOfUsersThatSharedPostSecondConstructorShouldBe1()
        {
            secondConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(secondConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleShare_OneUserSharesThePostThirdConstructor_NumberOfUsersThatSharedPostThirdConstructorShouldBe1()
        {
            thirdConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(thirdConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(1));

        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe0()
        {
            firstConstructorPostForTest.ToggleShare(idUserToShare);
            firstConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(firstConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostSecondConstructor_NumberOfUsersThatSharedPostSecondConstructorShouldBe0()
        {
            secondConstructorPostForTest.ToggleShare(idUserToShare);
            secondConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(secondConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(0));

        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostThirdConstructor_NumberOfUsersThatSharedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPostForTest.ToggleShare(idUserToShare);
            thirdConstructorPostForTest.ToggleShare(idUserToShare);
            Assert.That(thirdConstructorPostForTest.UsersThatShared, Has.Count.EqualTo(0));

        }

        [Test]
        public void AddComment_CommentIsAddedToPostFirstConstructor_ThereShouldBeOneCommentInPostFirstConstructor()
        {

            firstConstructorPostForTest.AddComment(commentToAdd1);
            Assert.That(firstConstructorPostForTest.Comments, Has.Count.EqualTo(1));
           
        }
        [Test]
        public void AddComment_CommentIsAddedToPostSecondConstructor_ThereShouldBeOneCommentInPostSecondConstructor()
        {

            secondConstructorPostForTest.AddComment(commentToAdd2);
            Assert.That(secondConstructorPostForTest.Comments, Has.Count.EqualTo(1));

        }
        [Test]
        public void AddComment_CommentIsAddedToPostThirdConstructor_ThereShouldBeOneCommentInPostThirdConstructor()
        {

            thirdConstructorPostForTest.AddComment(commentToAdd3);
            Assert.That(thirdConstructorPostForTest.Comments, Has.Count.EqualTo(1));


        }
        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromPostFirstConstructor_NoMoreCommentsInPostFirstConstructor()
        {

            firstConstructorPostForTest.AddComment(commentToAdd1);
            firstConstructorPostForTest.RemoveComment(commentToAdd1);
            Assert.That(firstConstructorPostForTest.Comments, Has.Count.EqualTo(0));

        }
        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromSecondConstructor_NoMoreCommentsInPostSecondConstructor()
        {

            secondConstructorPostForTest.AddComment(commentToAdd2);
            secondConstructorPostForTest.RemoveComment(commentToAdd2);
            Assert.That(secondConstructorPostForTest.Comments, Has.Count.EqualTo(0));
 

        }
        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromThirdConstructor_NoMoreCommentsInPostThirdConstructor()
        {

            thirdConstructorPostForTest.AddComment(commentToAdd3);
            thirdConstructorPostForTest.RemoveComment(commentToAdd3);
            Assert.That(thirdConstructorPostForTest.Comments, Has.Count.EqualTo(0));

        }
        [Test]
        public void AddInterestUser_InterestStatus1andInterestStatus4AddedToPostFirstConstructor_ThereShouldBeTwoInterestStatusesInPostFirstConstructor()
        {
            
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd4);
            Assert.That(firstConstructorPostForTest.InterestStatuses, Has.Count.EqualTo(2));
           

        }
        [Test]
        public void AddInterestUser_InterestStatus2AddedToPostSecondConstructor_ThereShouldBeOneInterestStatusInPostSecondConstructor()
        {
          
            secondConstructorPostForTest.AddInterestStatus(interestStatusToAdd2);
            Assert.That(secondConstructorPostForTest.InterestStatuses, Has.Count.EqualTo(1));
            

        }
        [Test]
        public void AddInterestUser_InterestStatus3AddedToPostThirdConstructor_ThereShouldBeOneInterestStatusInPostThirdConstructor()
        {

            thirdConstructorPostForTest.AddInterestStatus(interestStatusToAdd3);
            Assert.That(thirdConstructorPostForTest.InterestStatuses, Has.Count.EqualTo(1));

        }

        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostFirstConstructor_SwitchedTheInterestStatusesOfPostFirstConstructorToTheOppositeValues()
        {
            
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd4);
            firstConstructorPostForTest.ToggleInterestStatus(interestStatusToAdd1.UserId);
            firstConstructorPostForTest.ToggleInterestStatus(interestStatusToAdd4.UserId);
            Assert.False(firstConstructorPostForTest.InterestStatuses[0].Interested);
            Assert.True(firstConstructorPostForTest.InterestStatuses[1].Interested);
           
        }
        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostSecondConstructor_SwitchedTheInterestStatusesOfPostSecondConstructorToTheOppositeValue()
        {

            secondConstructorPostForTest.AddInterestStatus(interestStatusToAdd2);
            secondConstructorPostForTest.ToggleInterestStatus(interestStatusToAdd2.UserId);
            Assert.False(secondConstructorPostForTest.InterestStatuses[0].Interested);

        }
        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostThirdConstructor_SwitchedTheInterestStatusesOfPostThirdConstructorToTheOppositeValue()
        {
          
            thirdConstructorPostForTest.AddInterestStatus(interestStatusToAdd3);
            thirdConstructorPostForTest.ToggleInterestStatus(interestStatusToAdd3.UserId);
            Assert.True(thirdConstructorPostForTest.InterestStatuses[0].Interested);
            
        }
        [Test]
        public void ToggleInterestStatus_TryToToggleInterestStatusOfAUserThatWasNotInterestedForPostThirdConstructor_ThereShouldBeAnError()
        {
        
            var exceptionMessage = Assert.Throws<Exception>(() => { thirdConstructorPostForTest.ToggleInterestStatus(interestStatusToAdd3.UserId);});
            Assert.That(exceptionMessage.Message, Is.EqualTo("Interest status not found"));


        }

        [Test]
        public void RemoveInterestUser_InterestStatusRemovedFromPost()
        {
 
            thirdConstructorPostForTest.AddInterestStatus(interestStatusToAdd3);
            thirdConstructorPostForTest.RemoveInterestStatus(interestStatusToAdd3.UserId);
            Assert.That(thirdConstructorPostForTest.InterestStatuses, Has.Count.EqualTo(0));
        }
        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostFirstConstructor_TheInterestShouldBeEqualWith0 ()
        {
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPostForTest.AddInterestStatus(interestStatusToAdd4);
            Assert.True(firstConstructorPostForTest.InterestLevel()==0);



        }
        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostSecondConstructor_TheInterestShouldBeEqualWith1()
        {
            
            secondConstructorPostForTest.AddInterestStatus(interestStatusToAdd2);
            Assert.True(secondConstructorPostForTest.InterestLevel() == 1);
      
        }
        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostThirdConstructor_TheInterestShouldBeEqualWithMinus1()
        {

            thirdConstructorPostForTest.AddInterestStatus(interestStatusToAdd3);
            Assert.True(thirdConstructorPostForTest.InterestLevel() == -1);

        }
       
    } 
}
