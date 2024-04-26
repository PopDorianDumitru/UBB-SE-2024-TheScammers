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
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _postService = new PostService(_postRepositoryMock.Object, _userRepositoryMock.Object, _groupRepositoryMock.Object);
        }

        [Test]
        public void GetPosts_ReturnsAllPostsFromRepository()
        {
            var expectedPosts = new List<Post>();
            _postRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedPosts);

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

            _postRepositoryMock.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(postToBeReturned);

            Assert.That(_postService.GetPostById(Guid.NewGuid()), Is.EqualTo(postToBeReturned));
        }

        [Test]
        public void GetPosts_Any_PostRepositoryGetAllIsCalled()
        {
            _postService.GetPosts();

            _postRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
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
        public void RemoveConfirmationTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveConfirmation(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void ConfirmPostTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.ConfirmPost(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void AddReportTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.AddReport(Guid.NewGuid(), Guid.NewGuid(), ""); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void RemovePostTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.RemoveReport(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void FavoritePostTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.FavoritePost(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }

        [Test]
        public void UnfavoritePostTest_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { _postService.UnfavoritePost(Guid.NewGuid(), Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not found"));
        }
    }
}
