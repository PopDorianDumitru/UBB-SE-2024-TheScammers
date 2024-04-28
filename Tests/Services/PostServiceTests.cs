using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.Model.Repositories;
using ISSLab.Services;
using Moq;

namespace Tests.Services
{
    internal class PostServiceTests
    {
        private PostService _postService;
        private Mock<IPostRepository> _postRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _postService = new PostService(_postRepositoryMock.Object);
        }

        [Test]
        public void GetPosts_OnePost_ReturnsListWithThatPost()
        {
            var post = new Post();
            var expectedPosts = new List<Post> { post };
            _postRepositoryMock.Setup(repository => repository.GetAllPosts()).Returns(expectedPosts);

            var result = _postService.GetPosts();

            Assert.That(result, Is.EqualTo(expectedPosts));
        }

        [Test]
        public void AddPost_AnyPost_PostAdded()
        {
            Guid authorId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            Post addedPost = new Post("", authorId, groupId, "", "", "", "", "", true);

            _postService.AddPost(addedPost);

            _postRepositoryMock.Verify(repository => repository.AddPost(addedPost), Times.Once);
        }

        [Test]
        public void RemovePost_AnyPost_PostRemoved()
        {
            Guid authorId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            Post removedPost = new Post("", authorId, groupId, "", "", "", "", "", true);

            _postService.RemovePost(removedPost);

            _postRepositoryMock.Verify(repository => repository.RemovePost(removedPost.Id), Times.Once);
        }

        [Test]
        public void GetPostById_PostDoesNotExist_ThrowsException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.GetPostById(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void GetPostById_PostExists_PostReturned()
        {
            Post postToBeReturned = new Post();

            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(postToBeReturned);

            Assert.That(_postService.GetPostById(Guid.NewGuid()), Is.EqualTo(postToBeReturned));
        }

        [Test]
        public void GetPosts_Any_PostRepositoryGetAllIsCalled()
        {
            _postService.GetPosts();

            _postRepositoryMock.Verify(repository => repository.GetAllPosts(), Times.Once);
        }

        [Test]
        public void CheckIfNeedConfirmationTest_ExceptionThrown()
        {
            Guid guid = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.GetPostById(guid); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void AddReport_AnyReport_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.AddReport(Guid.NewGuid(), Guid.NewGuid(), ""); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void RemoveConfirmation_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveConfirmation(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void RemoveConfirmation_PostDoesExist_PostIsNoLongerConfirmed()
        {
            Post theOnlyPost = new Post();
            theOnlyPost.Confirmed = true;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);

            _postService.RemoveConfirmation(theOnlyPost.Id);

            Assert.That(theOnlyPost.Confirmed, Is.False);
        }

        [Test]
        public void ConfirmPost_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.ConfirmPost(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void ConfirmPost_PostDoesExist_PostIsConfirmed()
        {
            Post theOnlyPost = new Post();
            theOnlyPost.Confirmed = false;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);

            _postService.ConfirmPost(theOnlyPost.Id);

            Assert.That(theOnlyPost.Confirmed, Is.True);
        }

        [Test]
        public void AddReport_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.AddReport(Guid.NewGuid(), Guid.NewGuid(), "reason"); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void AddReport_PostDoesExist_ReportIsAdded()
        {
            Post theOnlyPost = new Post();
            Guid idOfTheOnlyPost = theOnlyPost.Id;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);
            Guid userId = Guid.NewGuid();
            string reason = "reason";

            _postService.AddReport(idOfTheOnlyPost, userId, reason);

            Assert.That(theOnlyPost.Reports.Count, Is.EqualTo(1));
            Assert.That(theOnlyPost.Reports[0].PostId, Is.EqualTo(idOfTheOnlyPost));
            Assert.That(theOnlyPost.Reports[0].UserId, Is.EqualTo(userId));
            Assert.That(theOnlyPost.Reports[0].ReasonForReporting, Is.EqualTo(reason));
        }

        [Test]
        public void RemoveReport_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveReport(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void RemoveReport_PostDoesExist_ReportIsRemoved()
        {
            Post theOnlyPost = new Post();
            Guid idOfTheOnlyPost = theOnlyPost.Id;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);
            Guid userId = Guid.NewGuid();
            theOnlyPost.AddReport(new Report(userId, idOfTheOnlyPost, "reason"));

            _postService.RemoveReport(idOfTheOnlyPost, userId);

            Assert.That(theOnlyPost.Reports, Is.Empty);
        }

        [Test]
        public void FavoritePost_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveReport(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void FavoritePost_PostDoesExist_UserIsAddedToPostsFavoritersUsers()
        {
            Post theOnlyPost = new Post();
            Guid idOfTheOnlyPost = theOnlyPost.Id;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);
            Guid userId = Guid.NewGuid();

            _postService.FavoritePost(idOfTheOnlyPost, userId);

            Assert.That(theOnlyPost.UsersThatFavorited.Count, Is.EqualTo(1));
        }

        [Test]
        public void UnfavoritePost_PostDoesNotExist_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveReport(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void UnfavoritePost_PostDoesExist_UserIsNotInPostsFavoritersUsers()
        {
            Post theOnlyPost = new Post();
            Guid idOfTheOnlyPost = theOnlyPost.Id;
            _postRepositoryMock.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(theOnlyPost);
            Guid userId = Guid.NewGuid();
            theOnlyPost.UsersThatFavorited.Add(userId);

            _postService.UnfavoritePost(idOfTheOnlyPost, userId);

            Assert.That(theOnlyPost.UsersThatFavorited, Is.Empty);
        }
    }
}
