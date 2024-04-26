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
        UserService userService;
        Mock<IUserRepository> userRepository;
        Mock<IPostRepository> postRepository;

        [SetUp]
        public void setUp()
        {
            userRepository = new Mock<IUserRepository>();
            postRepository = new Mock<IPostRepository>();

            userService = new UserService(userRepository.Object, postRepository.Object);
        }

        [Test]
        public void AddUser_AnyUser_UserRepositoryAddUserIsCalled()
        {
            Guid userId = Guid.NewGuid();
            User addedUser = new User(userId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);

            userService.AddUser(addedUser);

            userRepository.Verify(repository => repository.AddUser(addedUser), Times.Once);
        }

        [Test]
        public void RemoveUser_AnyUser_UserRepositoryRemoveUserIsCalled()
        {
            User toBeRemoved = new User();

            userService.RemoveUser(toBeRemoved);

            userRepository.Verify(repository => repository.DeleteUser(toBeRemoved.Id), Times.Once);
        }

        [Test]
        public void GetUserById_UserDoesNotExist_ThrowsException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { userService.GetUserById(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void GetUserById_UserExists_ThrowsException()
        {
            User userToBeReturned = new User();

            userRepository.Setup(repository => repository.GetById(It.IsAny<Guid>())).Returns(userToBeReturned);

            Assert.That(userService.GetUserById(Guid.NewGuid()), Is.EqualTo(userToBeReturned));
        }

        [Test]
        public void GetUsers_Any_UserRepositoryGetAllIsCalled()
        {
            userService.GetUsers();

            userRepository.Verify(repository => repository.GetAll(), Times.Once);
        }

        [Test]
        public void UpdateUserUsername_Any_UserRepositoryUpdateUserUsernameIsCalled()
        {
            userService.UpdateUserUsername(Guid.NewGuid(), "user");

            userRepository.Verify(repository => repository.UpdateUserUsername(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddReview_Any_UserRepositoryAddReviewIsCalled()
        {
            userService.AddReview(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "content", DateTime.Now, 3);

            userRepository.Verify(repository => repository.AddReview(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public void AddPostToCart_Any_UserRepositoryAddPostToCartIsCalled()
        {
            userService.AddPostToCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            userRepository.Verify(repository => repository.AddPostToCart(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void RemovePostFromCart_Any_UserRepositoryRemovePostFromCartIsCalled()
        {
            userService.RemovePostFromCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            userRepository.Verify(repository => repository.RemoveFromCart(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void AddPostToFavorites_Any_UserRepositoryAddToFavoritesIsCalled()
        {
            userService.AddPostToFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            userRepository.Verify(repository => repository.AddToFavorites(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void RemovePostFromFavorites_Any_UserRepositoryRemoveFromFavoritesIsCalled()
        {
            userService.RemovePostFromFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            userRepository.Verify(repository => repository.RemoveFromFavorites(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}
