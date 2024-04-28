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
    internal class UserServiceTests
    {
        private UserService userService;
        private Mock<IUserRepository> mockedUserRepository;
        private Mock<IPostRepository> mockedPostRepository;

        [SetUp]
        public void SetUp()
        {
            mockedUserRepository = new Mock<IUserRepository>();
            mockedPostRepository = new Mock<IPostRepository>();

            userService = new UserService(mockedUserRepository.Object, mockedPostRepository.Object);
        }

        [Test]
        public void AddUser_AnyUser_UserRepositoryAddUserIsCalled()
        {
            Guid userId = Guid.NewGuid();
            User addedUser = new User(userId, string.Empty, string.Empty, DateOnly.Parse("10.10.2020"), string.Empty, string.Empty, DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), new List<Cart>(), new List<UsersFavoritePosts>(), new List<Guid>(), new List<Review>(), 0);

            userService.AddUser(addedUser);

            mockedUserRepository.Verify(repository => repository.AddUser(addedUser), Times.Once);
        }

        [Test]
        public void RemoveUser_AnyUser_UserRepositoryRemoveUserIsCalled()
        {
            User toBeRemoved = new User();

            userService.RemoveUser(toBeRemoved);

            mockedUserRepository.Verify(repository => repository.DeleteUser(toBeRemoved.Id), Times.Once);
        }

        [Test]
        public void GetUserById_UserDoesNotExist_ThrowsException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { userService.GetUserById(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void GetUserById_UserExists_ReturnsUser()
        {
            User userToBeReturned = new User();

            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(userToBeReturned);

            Assert.That(userService.GetUserById(Guid.NewGuid()), Is.EqualTo(userToBeReturned));
        }

        [Test]
        public void GetUsers_Any_UserRepositoryGetAllIsCalled()
        {
            userService.GetUsers();

            mockedUserRepository.Verify(repository => repository.GetAll(), Times.Once);
        }

        [Test]
        public void UpdateUserUsername_Any_UserRepositoryUpdateUserUsernameIsCalled()
        {
            userService.UpdateUserUsername(Guid.NewGuid(), "user");

            mockedUserRepository.Verify(repository => repository.UpdateUserUsername(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddReview_Any_UserRepositoryAddReviewIsCalled()
        {
            userService.AddReview(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "content", DateTime.Now, 3);

            mockedUserRepository.Verify(repository => repository.AddReview(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public void AddPostToCart_Any_UserRepositoryAddPostToCartIsCalled()
        {
            userService.AddPostToCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            mockedUserRepository.Verify(repository => repository.AddPostToCart(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void RemovePostFromCart_Any_UserRepositoryRemovePostFromCartIsCalled()
        {
            userService.RemovePostFromCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            mockedUserRepository.Verify(repository => repository.RemoveFromCart(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void AddPostToFavorites_Any_UserRepositoryAddToFavoritesIsCalled()
        {
            userService.AddPostToFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            mockedUserRepository.Verify(repository => repository.AddToFavorites(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void RemovePostFromFavorites_Any_UserRepositoryRemoveFromFavoritesIsCalled()
        {
            userService.RemovePostFromFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            mockedUserRepository.Verify(repository => repository.RemoveFromFavorites(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void IsUserInGroup_UserBelongsToTheGroup_ReturnsTrue()
        {
            User userToBeReturnedByRepositoryGetById = new User();
            Guid groupWhichUserBelongsToId = Guid.NewGuid();
            userToBeReturnedByRepositoryGetById.AddGroup(groupWhichUserBelongsToId);
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(userToBeReturnedByRepositoryGetById);

            Assert.That(userService.IsUserInGroup(userToBeReturnedByRepositoryGetById.Id, groupWhichUserBelongsToId), Is.True);
        }

        [Test]
        public void IsUserInGroup_UserDoesNotBelongToTheGroup_ReturnsFalse()
        {
            User userToBeReturnedByRepositoryGetById = new User();
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(userToBeReturnedByRepositoryGetById);

            Assert.That(userService.IsUserInGroup(userToBeReturnedByRepositoryGetById.Id, Guid.NewGuid()), Is.False);
        }

        [Test]
        public void GetFavoritePosts_NoFavoritePostsForThatUserAndGroup_ReturnsEmptyList()
        {
            User theOnlyUser = new User();
            Guid idOfTheOnlyUser = theOnlyUser.Id;
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(theOnlyUser);

            Assert.That(userService.GetFavoritePosts(Guid.NewGuid(), idOfTheOnlyUser), Is.Empty);
        }

        [Test]
        public void GetFavoritePosts_AtLeastOneFavoritePostForThatUserAndGroup_ReturnsTheFavoritePosts()
        {
            User theOnlyUser = new User();
            Guid idOfTheOnlyUser = theOnlyUser.Id;
            Guid groupForWhichTheUserHasFavoritePosts = Guid.NewGuid();
            List<Guid> expectedFavoritePostsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            Post postAlwaysReturnedByRepositoryInGetPostById = new Post();
            theOnlyUser.AddFavorites(new UsersFavoritePosts(idOfTheOnlyUser, groupForWhichTheUserHasFavoritePosts, expectedFavoritePostsIds));
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(theOnlyUser);
            mockedPostRepository.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(postAlwaysReturnedByRepositoryInGetPostById);

            List<Post> actualFavoritePosts = userService.GetFavoritePosts(groupForWhichTheUserHasFavoritePosts, idOfTheOnlyUser);

            Assert.That(actualFavoritePosts,
                Is.EquivalentTo(new List<Post> { postAlwaysReturnedByRepositoryInGetPostById, postAlwaysReturnedByRepositoryInGetPostById }));
        }

        [Test]
        public void GetPostsFromCart_NoCartedPostsForThatUserAndGroup_ReturnsEmptyList()
        {
            User theOnlyUser = new User();
            Guid idOfTheOnlyUser = theOnlyUser.Id;
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(theOnlyUser);

            Assert.That(userService.GetPostsFromCart(Guid.NewGuid(), idOfTheOnlyUser), Is.Empty);
        }

        [Test]
        public void GetPostsFromCart_AtLeastOneCartedPostForThatUserAndGroup_ReturnsTheCartedPosts()
        {
            User theOnlyUser = new User();
            Guid idOfTheOnlyUser = theOnlyUser.Id;
            Guid groupForWhichTheUserHasCartedPosts = Guid.NewGuid();
            List<Guid> expectedCartedPostsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            Post postAlwaysReturnedByRepositoryInGetPostById = new Post();
            theOnlyUser.AddCart(new Cart(idOfTheOnlyUser, groupForWhichTheUserHasCartedPosts, expectedCartedPostsIds));
            mockedUserRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(theOnlyUser);
            mockedPostRepository.Setup(repository => repository.GetPostById(It.IsAny<Guid>())).Returns(postAlwaysReturnedByRepositoryInGetPostById);

            List<Post> actualCartedPosts = userService.GetPostsFromCart(groupForWhichTheUserHasCartedPosts, idOfTheOnlyUser);

            Assert.That(actualCartedPosts,
                Is.EquivalentTo(new List<Post> { postAlwaysReturnedByRepositoryInGetPostById, postAlwaysReturnedByRepositoryInGetPostById }));
        }
    }
}
