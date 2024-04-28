using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.Model.Repositories;
using Moq;

namespace Tests.Model.Repositories
{
    internal class UserRepositoryTests
    {
        public UserRepository userRepository;

        [SetUp]
        public void SetUp()
        {
            userRepository = new UserRepository();
        }

        [Test]
        public void FindAllUsers_NoUsers_ReturnsEmptyList()
        {
            Assert.That(userRepository.GetAll(), Is.Empty);
        }

        [Test]
        public void FindAllUsers_AtLeastOneUser_ReturnsTheUsers()
        {
            User firstAddedUser = new User();
            User secondAddedUser = new User();
            userRepository.AddUser(firstAddedUser);
            userRepository.AddUser(secondAddedUser);

            Assert.That(userRepository.GetAll(), Is.EquivalentTo(new List<User> { firstAddedUser, secondAddedUser }));
        }

        [Test]
        public void FindById_IdExists_TheUserIsReturned()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedUser);

            Assert.That(userRepository.GetById(addedUserId), Is.EqualTo(addedUser));
        }

        [Test]
        public void FindById_IdDoesNotExist_ExceptionThrown()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedUser);

            var exceptionMessage = Assert.Throws<Exception>(() => { userRepository.GetById(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User does not exist"));
        }

        [Test]
        public void AddUser_AnyUser_UserAppearsInGetAll()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);

            userRepository.AddUser(addedUser);

            Assert.That(userRepository.GetAll(), Is.EqualTo(new List<User> { addedUser }));
        }

        [Test]
        public void UpdateUserUsername_ExistingUser_UsersUsernameIsUpdated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            string newUsername = "newName";
            userRepository.AddUser(addedUser);

            userRepository.UpdateUserUsername(addedUserId, newUsername);

            Assert.That(addedUser.Username, Is.EqualTo(newUsername));
        }

        [Test]
        public void UpdateUserDateOfBirth_ExistingUser_UsersDateOfBirthIsUpdated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            DateOnly newDateOfBirth = DateOnly.FromDateTime(DateTime.Now);
            userRepository.AddUser(addedUser);

            userRepository.UpdateUserDateOfBirth(addedUserId, newDateOfBirth);

            Assert.That(addedUser.DateOfBirth, Is.EqualTo(newDateOfBirth));
        }

        [Test]
        public void AddReview_ExistingUser_ReviewAppearsInUsersReviews()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Review addedReview = new Review();
            addedReview.SellerId = addedUserId;
            userRepository.AddUser(addedUser);

            userRepository.AddReview(addedReview);

            Assert.That(addedUser.Reviews, Does.Contain(addedReview));
        }

        [Test]
        public void AddReview_NonexistingUser_ExceptionThrown()
        {
            Review addedReview = new Review();

            var exceptionMessage = Assert.Throws<NullReferenceException>(() => { userRepository.AddReview(addedReview); });
        }

        [Test]
        public void UpdateUserProfilePicture_ExistingUser_UsersProfilePictureIsUpdated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            string newProfilePicture = "pic";
            userRepository.AddUser(addedUser);

            userRepository.UpdateUserProfilePicture(addedUserId, newProfilePicture);

            Assert.That(addedUser.ProfilePicture, Is.EqualTo(newProfilePicture));
        }

        [Test]
        public void UpdateUserPassword_ExistingUser_UsersPasswordIsUpdated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            string newPassword = "pass";
            userRepository.AddUser(addedUser);

            userRepository.UpdateUserPassword(addedUserId, newPassword);

            Assert.That(addedUser.Password, Is.EqualTo(newPassword));
        }

        [Test]
        public void UpdateUserNumberOfSells_ExistingUser_UsersNumberOfSellsIsUpdated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            int newNumberOfSells = 123;
            userRepository.AddUser(addedUser);

            userRepository.UpdateUserNumberOfSells(addedUserId, newNumberOfSells);

            Assert.That(addedUser.NumberOfSales, Is.EqualTo(newNumberOfSells));
        }

        [Test]
        public void DeleteUser_ExistingUser_TheUserDoesNotAppearInGetAll()
        {
            Guid addedThenRemovedUserId = Guid.NewGuid();
            User addedThenRemovedUser = new User(addedThenRemovedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedThenRemovedUser);

            userRepository.DeleteUser(addedThenRemovedUserId);

            Assert.That(userRepository.GetAll(), Is.Empty);
        }

        [Test]
        public void DeleteUser_NonexistingUser_NoUserIsDeleted()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedUser);

            userRepository.DeleteUser(Guid.NewGuid());

            Assert.That(userRepository.GetAll(), Is.EqualTo(new[] { addedUser }));
        }

        [Test]
        public void AddPostToCart_NonexistingUser_ExceptionThrown()
        {
            Assert.Throws<Exception>(() =>
            {
                userRepository.AddPostToCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            });
        }

        [Test]
        public void AddPostToCart_NonexistingCart_CartIsCreated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedUser);

            userRepository.AddPostToCart(Guid.NewGuid(), addedUserId, Guid.NewGuid());

            Assert.That(addedUser.Carts.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void AddPostToCart_PostAlreadySaved_NoPostIsAddedToCart()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid postId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            userRepository.AddUser(addedUser);
            userRepository.AddPostToCart(groupId, addedUserId, postId);

            userRepository.AddPostToCart(groupId, addedUserId, postId);

            Assert.That(addedUser.Carts.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void AddPostToCart_PostNotAlreadySaved_PostIsAddedToCart()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid postId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            userRepository.AddUser(addedUser);

            userRepository.AddPostToCart(groupId, addedUserId, postId);

            Assert.That(addedUser.Carts.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveFromCart_ExistingUserAndGroup_PostIsRemovedFromUsersPostsSavedInCart()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid groupId = Guid.NewGuid();
            addedUser.AddGroup(groupId);
            Cart cart = new Cart(groupId, addedUserId);
            Guid postId = Guid.NewGuid();
            cart.AddPostToCart(postId);
            addedUser.AddCart(cart);
            userRepository.AddUser(addedUser);

            userRepository.RemoveFromCart(groupId, addedUserId, postId);

            Assert.That(cart.PostsSavedInCart.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveFromCart_NonexistingUser_ExceptionThrown()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                userRepository.RemoveFromCart(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            });
        }

        [Test]
        public void AddToFavorites_NonexistingUser_ExceptionThrown()
        {
            Assert.Throws<Exception>(() =>
            {
                userRepository.AddToFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            });
        }

        [Test]
        public void AddToFavorites_NonexistingGroupFavorites_CartIsCreated()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            userRepository.AddUser(addedUser);

            userRepository.AddToFavorites(Guid.NewGuid(), addedUserId, Guid.NewGuid());

            Assert.That(addedUser.Favorites.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddToFavorites_PostAlreadySaved_NoPostIsAddedToFavorites()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid postId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            userRepository.AddUser(addedUser);
            userRepository.AddToFavorites(groupId, addedUserId, postId);

            userRepository.AddToFavorites(groupId, addedUserId, postId);

            Assert.That(addedUser.Favorites.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddToFavorites_PostNotAlreadySaved_PostIsAddedToFavorites()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid postId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            userRepository.AddUser(addedUser);

            userRepository.AddToFavorites(groupId, addedUserId, postId);

            Assert.That(addedUser.Favorites.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveFromFavorites_ExistingUserAndGroup_PostIsRemovedFromUsersFavorites()
        {
            Guid addedUserId = Guid.NewGuid();
            User addedUser = new User(addedUserId, "", "", DateOnly.Parse("10.10.2020"), "", "", DateTime.Now, new List<Guid>(),
                new List<Guid>(), new List<SellingUserScore>(), [], [], [], [], 0);
            Guid groupId = Guid.NewGuid();
            addedUser.AddGroup(groupId);
            UsersFavoritePosts favorites = new UsersFavoritePosts(groupId, addedUserId);
            Guid postId = Guid.NewGuid();
            favorites.AddPost(postId);
            addedUser.AddFavorites(favorites);
            userRepository.AddUser(addedUser);

            userRepository.RemoveFromFavorites(groupId, addedUserId, postId);

            Assert.That(favorites.Posts.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveFromFavorites_NonexistingUser_ExceptionThrown()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                userRepository.RemoveFromFavorites(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            });
        }
    }
}