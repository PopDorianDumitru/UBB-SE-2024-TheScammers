using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class PostTests
    {
        private ISSLab.Model.Post firstConstructorPost;
        private ISSLab.Model.Post secondConstructorPost;
        private ISSLab.Model.Post thirdConstructorPost;
        private Report reportToAdd;
        private Guid idUserToLike;
        private Guid idUserToFavorite;
        private Guid idUserToShare;
        private Guid idUserInterested1;
        private Guid idUserInterested2;
        private Comment? commentToAdd1;
        private Comment? commentToAdd2;
        private Comment? commentToAdd3;
        private InterestStatus interestStatusToAdd1;
        private InterestStatus interestStatusToAdd2;
        private InterestStatus interestStatusToAdd3;
        private InterestStatus interestStatusToAdd4;

        [SetUp]
        public void SetUp()
        {
            reportToAdd = new Report();
            idUserToLike = Guid.NewGuid();
            idUserToFavorite = Guid.NewGuid();
            idUserInterested1 = Guid.NewGuid();
            idUserInterested2 = Guid.NewGuid();
            idUserToShare = Guid.NewGuid();
            commentToAdd1 = new Comment();
            firstConstructorPost = new Post("./cat.jpg", Guid.NewGuid(), Guid.NewGuid(), "Cluj", "description1", "title1", "contacts1", string.Empty, true);
            secondConstructorPost = new Post(Guid.NewGuid(), new List<Guid>(), new List<Guid>(),
                new List<Comment>(), "./cat.jpg", DateTime.Parse("Jan 1, 2024"), Guid.NewGuid(), Guid.NewGuid(), false, new List<Guid>(), "Cluj", "description2", "title2",
                new List<InterestStatus>(), "0744444444", new List<Report>(), "type2", false, 100);
            thirdConstructorPost = new Post();
            interestStatusToAdd1 = new InterestStatus(idUserInterested1, firstConstructorPost.Id, true);
            interestStatusToAdd2 = new InterestStatus(idUserInterested1, secondConstructorPost.Id, true);
            interestStatusToAdd3 = new InterestStatus(idUserInterested1, thirdConstructorPost.Id, false);
            interestStatusToAdd4 = new InterestStatus(idUserInterested2, firstConstructorPost.Id, false);
        }

        [Test]
        public void IdGet_PostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPost.Id != new Guid());
        }

        [Test]
        public void IdGet_GetTheIdOfPostSecondConstructor_ShouldBeNotEmpty()
        {
            Assert.True(secondConstructorPost.Id != new Guid());
        }

        [Test]
        public void IdGet_GetTheIdOfPostThirdConstructor_ShouldBeNotEmpty()
        {
            Assert.True(thirdConstructorPost.Id != new Guid());
        }

        [Test]
        public void ViewsGet_PostFirstConstructor_ShouldBe100()
        {
            Assert.True(firstConstructorPost.Views.Equals(0));
        }
        [Test]
        public void ViewsGet_PostSecondConstructor_ShouldBe_0()
        {
            Assert.True(secondConstructorPost.Views.Equals(100));
        }
        [Test]
        public void ViewsGet_getTheViewsPostThirdConstructor_ShouldBe_0()
        {
            Assert.True(thirdConstructorPost.Views.Equals(0));
        }
        [Test]
        public void UsersThatLikedGet_PostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPost.UsersThatLiked);
        }

        [Test]
        public void UsersThatLikedGet_PostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPost.UsersThatLiked);
        }

        [Test]
        public void UsersThatLikedGet_getUsersThatLikedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPost.UsersThatLiked);
        }

        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPost.UsersThatShared);
        }
        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPost.UsersThatShared);
        }

        [Test]
        public void UsersThatSharedGet_getUsersThatSharedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPost.UsersThatShared);
        }

        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostFirstConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(firstConstructorPost.UsersThatFavorited);
        }

        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostSecondConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(secondConstructorPost.UsersThatFavorited);
        }

        [Test]
        public void UsersThatFavoritedGet_getUsersThatFavoritedOfPostThirdConstructor_ShouldBeListGuid()
        {
            Assert.IsInstanceOf<List<Guid>>(thirdConstructorPost.UsersThatFavorited);
        }

        [Test]
        public void CommentsGet_getCommentOfPostFirstConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(firstConstructorPost.Comments);
        }

        [Test]
        public void CommentsGet_getCommentsOfPostSecondConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(secondConstructorPost.Comments);
        }

        [Test]
        public void CommentsGet_getUsersThatFavoritedOfPostThirdConstructor_ShouldBeListComment()
        {
            Assert.IsInstanceOf<List<Comment>>(thirdConstructorPost.Comments);
        }

        [Test]
        public void ReportsGet_getReportsOfPostFirstConstructor_ShouldBeListReport()
        {
            Assert.IsInstanceOf<List<Report>>(firstConstructorPost.Reports);
        }

        [Test]
        public void ReportsGet_getReportsOfPostSecondConstructor_ShouldBeListReport()
        {
            Assert.IsInstanceOf<List<Report>>(secondConstructorPost.Reports);
        }

        [Test]
        public void ReportsGet_getReportsOfPostThirdConstructor_ShouldBeListReports()
        {
            Assert.IsInstanceOf<List<Report>>(thirdConstructorPost.Reports);
        }

        [Test]
        public void MediaContentGet_getMediaContentOfPostFirstConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<string>(firstConstructorPost.MediaContent);
            Assert.That(firstConstructorPost.MediaContent == "./cat.jpg");
        }

        [Test]
        public void MediaContentGet_getMediaContentOfPostSecondConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<string>(secondConstructorPost.MediaContent);
            Assert.That(secondConstructorPost.MediaContent == "./cat.jpg");
        }

        [Test]
        public void MediaContentGet_getMediaContentOfPostThirdConstructor_ShouldBeString()
        {
            Assert.IsInstanceOf<string>(thirdConstructorPost.MediaContent);
            Assert.That(thirdConstructorPost.MediaContent == string.Empty);
        }

        [Test]
        public void CreationDateGet_getCreationDateOfPostFirstConstructor_ShouldBeDatetime()
        {
            Assert.IsInstanceOf<DateTime>(firstConstructorPost.CreationDate);
        }

        [Test]
        public void CreationDateGet_getCreationDateOfPostThirdConstructor_ShouldBeDatetime()
        {
            Assert.IsInstanceOf<DateTime>(thirdConstructorPost.CreationDate);
        }

        [Test]
        public void AuthorIdGet_GetTheAuthorIdOfPostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPost.AuthorId != new Guid());
        }

        [Test]
        public void GroupIdGet_GetTheGroupIdOfPostFirstConstructor_ShouldBeNotEmpty()
        {
            Assert.True(firstConstructorPost.GroupId != new Guid());
        }

        [Test]
        public void ContactsSet_SetTheContactsOfPostFirstConstructor_ShouldBeEqualWith0755555555()
        {
            firstConstructorPost.Contacts = "0755555555";
            Assert.True(firstConstructorPost.Contacts == "0755555555");
        }

        [Test]
        public void ConfirmedSet_SetTheConfirmedOfPostFirstConstructor_ShouldBeEqualWithTrue()
        {
            firstConstructorPost.Confirmed = true;
            Assert.True(firstConstructorPost.Confirmed);
        }

        [Test]
        public void PromotedSet_SetTheContactsOfPostSecondConstructor_ShouldBeEqualWithTrue()
        {
            Assert.False(firstConstructorPost.Promoted);
            firstConstructorPost.Promoted = true;
            Assert.True(firstConstructorPost.Promoted);
        }

        [Test]
        public void TitleSet_SetTheTitleOfPostFirstConstructor_ShouldBeEqualWithtitle1()
        {
            Assert.True(firstConstructorPost.Title == "title1");
        }

        [Test]
        public void DescriptionSet_SetTheDescriptionOfPostFirstConstructor_ShouldBeEqualWithdescription1()
        {
            Assert.True(firstConstructorPost.Description == "description1");
        }

        [Test]
        public void ItemLotionSet_SetTheItemLocationOfPostFirstConstructor_ShouldBeEqualWithBucuresti()
        {
            Assert.True(firstConstructorPost.ItemLocation == "Cluj");
            firstConstructorPost.ItemLocation = "Bucuresti";
            Assert.True(firstConstructorPost.ItemLocation == "Bucuresti");
        }

        [Test]
        public void AddReport_AddReportToPostFirstConstructor_PostFirstConstructorShouldHaveOneReport()
        {
            firstConstructorPost.AddReport(reportToAdd);
            Assert.That(firstConstructorPost.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddReport_AddReportToPostSecondConstructor_PostSecondConstructorShouldHaveOneReport()
        {
            secondConstructorPost.AddReport(reportToAdd);
            Assert.That(secondConstructorPost.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddReport_AddReportToPostThirdConstructor_PostThirdConstructorShouldHaveOneReport()
        {
            thirdConstructorPost.AddReport(reportToAdd);
            Assert.That(thirdConstructorPost.Reports, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostFirstConstructor_PostFirstConstructorShouldHaveNoReports()
        {
            firstConstructorPost.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            firstConstructorPost.RemoveReport(userId);
            Assert.That(firstConstructorPost.Reports, Has.Count.EqualTo(0));
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostSecondConstructor_PostSecondConstructorShouldHaveNoReports()
        {
            secondConstructorPost.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            secondConstructorPost.RemoveReport(userId);
            Assert.That(secondConstructorPost.Reports, Has.Count.EqualTo(0));
        }
        [Test]
        public void RemoveReport_RemoveSingularReportFromPostThirdConstructor_PostThirdConstructorShouldHaveNoReports()
        {
            thirdConstructorPost.AddReport(reportToAdd);
            Guid userId = reportToAdd.UserId;
            thirdConstructorPost.RemoveReport(userId);
            Assert.That(thirdConstructorPost.Reports, Has.Count.EqualTo(0));
        }

        [Test]
        public void ToggleLike_OneUserLikesThePostFirstConstructor_NumberOfUsersThatLikedPostFirstConstructorShouldBe1()
        {
            firstConstructorPost.ToggleLike(idUserToShare);
            Assert.That(firstConstructorPost.UsersThatLiked, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleLike_OneUserLikesThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe1()
        {
            secondConstructorPost.ToggleLike(idUserToShare);
            Assert.That(secondConstructorPost.UsersThatLiked, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleLike_OneUserLikesThePostThirdConstructor_NumberOfUsersThatLikedPostThirdConstructorShouldBe1()
        {
            thirdConstructorPost.ToggleLike(idUserToShare);
            Assert.That(thirdConstructorPost.UsersThatLiked, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleLike_OneUserDoesNotSharesAnymoreThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe0()
        {
            firstConstructorPost.ToggleLike(idUserToShare);
            firstConstructorPost.ToggleLike(idUserToShare);
            Assert.That(firstConstructorPost.UsersThatLiked, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleLike_OneUserDoesNotLikeAnymoreThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe0()
        {
            secondConstructorPost.ToggleLike(idUserToLike);
            secondConstructorPost.ToggleLike(idUserToLike);
            Assert.That(secondConstructorPost.UsersThatLiked, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleLike_OneUserDoesNotLikeAnymoreThePostThirdConstructor_NumberOfUsersThatLikedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPost.ToggleLike(idUserToLike);
            thirdConstructorPost.ToggleLike(idUserToLike);
            Assert.That(thirdConstructorPost.UsersThatLiked, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleFavorite_OneUserFavoritesThePostFirstConstructor_NumberOfUsersThatFavoritedPostFirstConstructorShouldBe1()
        {
            firstConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(firstConstructorPost.UsersThatFavorited, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleFavorites_OneUserFavoritesThePostSecondConstructor_NumberOfUsersThatFavoritedPostSecondConstructorShouldBe1()
        {
            secondConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(secondConstructorPost.UsersThatFavorited, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleFavorites_OneUserFavoritesThePostThirdConstructor_NumberOfUsersThatFavoritesPostThirdConstructorShouldBe1()
        {
            thirdConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(thirdConstructorPost.UsersThatFavorited, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleFavorites_OneUserDoesNotFavoriteAnymoreThePostFirstConstructor_NumberOfUsersThatFavoritedPostFirstConstructorShouldBe0()
        {
            firstConstructorPost.ToggleFavorite(idUserToFavorite);
            firstConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(firstConstructorPost.UsersThatFavorited, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleFavorite_OneUserDoesNotFavoriteAnymoreThePostSecondConstructor_NumberOfUsersThatLikedPostSecondConstructorShouldBe0()
        {
            secondConstructorPost.ToggleFavorite(idUserToFavorite);
            secondConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(secondConstructorPost.UsersThatFavorited, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleFavorite_OneUserDoesNotFavoriteAnymoreThePostThirdConstructor_NumberOfUsersThatFavoritedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPost.ToggleFavorite(idUserToFavorite);
            thirdConstructorPost.ToggleFavorite(idUserToFavorite);
            Assert.That(thirdConstructorPost.UsersThatFavorited, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleShare_OneUserSharesThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe1()
        {
            firstConstructorPost.ToggleShare(idUserToShare);
            Assert.That(firstConstructorPost.UsersThatShared, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleShare_OneUserSharesThePostSecondConstructor_NumberOfUsersThatSharedPostSecondConstructorShouldBe1()
        {
            secondConstructorPost.ToggleShare(idUserToShare);
            Assert.That(secondConstructorPost.UsersThatShared, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleShare_OneUserSharesThePostThirdConstructor_NumberOfUsersThatSharedPostThirdConstructorShouldBe1()
        {
            thirdConstructorPost.ToggleShare(idUserToShare);
            Assert.That(thirdConstructorPost.UsersThatShared, Has.Count.EqualTo(1));
        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostFirstConstructor_NumberOfUsersThatSharedPostFirstConstructorShouldBe0()
        {
            firstConstructorPost.ToggleShare(idUserToShare);
            firstConstructorPost.ToggleShare(idUserToShare);
            Assert.That(firstConstructorPost.UsersThatShared, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostSecondConstructor_NumberOfUsersThatSharedPostSecondConstructorShouldBe0()
        {
            secondConstructorPost.ToggleShare(idUserToShare);
            secondConstructorPost.ToggleShare(idUserToShare);
            Assert.That(secondConstructorPost.UsersThatShared, Has.Count.EqualTo(0));
        }
        [Test]
        public void ToggleShare_OneUserDoesNotSharesAnymoreThePostThirdConstructor_NumberOfUsersThatSharedPostThirdConstructorShouldBe0()
        {
            thirdConstructorPost.ToggleShare(idUserToShare);
            thirdConstructorPost.ToggleShare(idUserToShare);
            Assert.That(thirdConstructorPost.UsersThatShared, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddComment_CommentIsAddedToPostFirstConstructor_ThereShouldBeOneCommentInPostFirstConstructor()
        {
            firstConstructorPost.AddComment(commentToAdd1);
            Assert.That(firstConstructorPost.Comments, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddComment_CommentIsAddedToPostSecondConstructor_ThereShouldBeOneCommentInPostSecondConstructor()
        {
            secondConstructorPost.AddComment(commentToAdd2);
            Assert.That(secondConstructorPost.Comments, Has.Count.EqualTo(1));
        }
        [Test]
        public void AddComment_CommentIsAddedToPostThirdConstructor_ThereShouldBeOneCommentInPostThirdConstructor()
        {
            thirdConstructorPost.AddComment(commentToAdd3);
            Assert.That(thirdConstructorPost.Comments, Has.Count.EqualTo(1));
        }

        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromPostFirstConstructor_NoMoreCommentsInPostFirstConstructor()
        {
            firstConstructorPost.AddComment(commentToAdd1);
            firstConstructorPost.RemoveComment(commentToAdd1);
            Assert.That(firstConstructorPost.Comments, Has.Count.EqualTo(0));
        }
        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromSecondConstructor_NoMoreCommentsInPostSecondConstructor()
        {
            secondConstructorPost.AddComment(commentToAdd2);
            secondConstructorPost.RemoveComment(commentToAdd2);
            Assert.That(secondConstructorPost.Comments, Has.Count.EqualTo(0));
        }

        [Test]
        public void RemoveComment_SingularCommentIsRemovedFromThirdConstructor_NoMoreCommentsInPostThirdConstructor()
        {
            thirdConstructorPost.AddComment(commentToAdd3);
            thirdConstructorPost.RemoveComment(commentToAdd3);
            Assert.That(thirdConstructorPost.Comments, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddInterestUser_InterestStatus1andInterestStatus4AddedToPostFirstConstructor_ThereShouldBeTwoInterestStatusesInPostFirstConstructor()
        {
            firstConstructorPost.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPost.AddInterestStatus(interestStatusToAdd4);
            Assert.That(firstConstructorPost.InterestStatuses, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddInterestUser_InterestStatus2AddedToPostSecondConstructor_ThereShouldBeOneInterestStatusInPostSecondConstructor()
        {
            secondConstructorPost.AddInterestStatus(interestStatusToAdd2);
            Assert.That(secondConstructorPost.InterestStatuses, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddInterestUser_InterestStatus3AddedToPostThirdConstructor_ThereShouldBeOneInterestStatusInPostThirdConstructor()
        {
            thirdConstructorPost.AddInterestStatus(interestStatusToAdd3);
            Assert.That(thirdConstructorPost.InterestStatuses, Has.Count.EqualTo(1));
        }

        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostFirstConstructor_SwitchedTheInterestStatusesOfPostFirstConstructorToTheOppositeValues()
        {
            firstConstructorPost.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPost.AddInterestStatus(interestStatusToAdd4);
            firstConstructorPost.ToggleInterestStatus(interestStatusToAdd1.InterestedUserId);
            firstConstructorPost.ToggleInterestStatus(interestStatusToAdd4.InterestedUserId);
            Assert.False(firstConstructorPost.InterestStatuses[0].Interested);
            Assert.True(firstConstructorPost.InterestStatuses[1].Interested);
        }
        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostSecondConstructor_SwitchedTheInterestStatusesOfPostSecondConstructorToTheOppositeValue()
        {
            secondConstructorPost.AddInterestStatus(interestStatusToAdd2);
            secondConstructorPost.ToggleInterestStatus(interestStatusToAdd2.InterestedUserId);
            Assert.False(secondConstructorPost.InterestStatuses[0].Interested);
        }
        [Test]
        public void ToggleInterestStatus_AddInterestStatusToPostThirdConstructor_SwitchedTheInterestStatusesOfPostThirdConstructorToTheOppositeValue()
        {
            thirdConstructorPost.AddInterestStatus(interestStatusToAdd3);
            thirdConstructorPost.ToggleInterestStatus(interestStatusToAdd3.InterestedUserId);
            Assert.True(thirdConstructorPost.InterestStatuses[0].Interested);
        }
        [Test]
        public void ToggleInterestStatus_TryToToggleInterestStatusOfAUserThatWasNotInterestedForPostThirdConstructor_ThereShouldBeAnError()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { thirdConstructorPost.ToggleInterestStatus(interestStatusToAdd3.InterestedUserId); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Interest status not found"));
        }

        [Test]
        public void RemoveInterestUser_InterestStatusRemovedFromPost()
        {
            thirdConstructorPost.AddInterestStatus(interestStatusToAdd3);
            thirdConstructorPost.RemoveInterestStatus(interestStatusToAdd3.InterestedUserId);
            Assert.That(thirdConstructorPost.InterestStatuses, Has.Count.EqualTo(0));
        }
        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostFirstConstructor_TheInterestShouldBeEqualWith0()
        {
            firstConstructorPost.AddInterestStatus(interestStatusToAdd1);
            firstConstructorPost.AddInterestStatus(interestStatusToAdd4);
            Assert.True(firstConstructorPost.InterestLevel() == 0);
        }

        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostSecondConstructor_TheInterestShouldBeEqualWith1()
        {
            secondConstructorPost.AddInterestStatus(interestStatusToAdd2);
            Assert.True(secondConstructorPost.InterestLevel() == 1);
        }
        [Test]
        public void InterestLevel_InterestLevelProperlyCalculatedForPostThirdConstructor_TheInterestShouldBeEqualWithMinus1()
        {
            thirdConstructorPost.AddInterestStatus(interestStatusToAdd3);
            Assert.True(thirdConstructorPost.InterestLevel() == -1);
        }
    }
}
