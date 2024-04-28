using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class CartTest
    {
        private Cart cartInitializedWithEmptyConstructor;
        private Cart cartInitializedWithGroupUser;
        private Cart cartInitializedWithGroupUserPosts;

        private Guid groupId;
        private Guid userId;
        private List<Guid> postsSavedInCart;

        private Guid postToSave;

        [SetUp]
        public void SetUp()
        {
            postToSave = Guid.NewGuid();
            groupId = Guid.NewGuid();
            userId = Guid.NewGuid();
            postsSavedInCart = new List<Guid>();
            cartInitializedWithEmptyConstructor = new Cart();
            cartInitializedWithGroupUser = new Cart(groupId, userId);
            cartInitializedWithGroupUserPosts = new Cart(groupId, userId, postsSavedInCart);
        }

        [Test]
        public void GroupIdGet_EmptyConstructorCart_ShouldNotBeEmpty()
        {
            Assert.That(cartInitializedWithEmptyConstructor.GroupId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GroupIdGet_GroupUserConstructorCart_ShouldBeEqualToGroupId()
        {
            Assert.That(cartInitializedWithGroupUser.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void GroupIdGet_GroupUserPostsConstructorCart_ShouldBeEqualToGroupId()
        {
            Assert.That(cartInitializedWithGroupUserPosts.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void UserIdGet_UserIdFromEmptyCart_ShouldNotBeEmpty()
        {
            Assert.That(cartInitializedWithEmptyConstructor.UserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void UserIdGet_UserIdFromCartGroupUser_ShouldBeEqualToUserId()
        {
            Assert.That(cartInitializedWithGroupUser.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void UserIdGet_UserIdFromCartAll_ShouldBeEqualToUserId()
        {
            Assert.That(cartInitializedWithGroupUserPosts.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void PostsSavedInCartGet_FromCartEmpty_ShouldBeEmptyList()
        {
            Assert.That(cartInitializedWithEmptyConstructor.PostsSavedInCart, Is.Empty);
        }

        [Test]
        public void PostsSavedInCartGet_FromCartGroupUser_ShouldBeEmptyList()
        {
            Assert.That(cartInitializedWithGroupUser.PostsSavedInCart, Is.Empty);
        }

        [Test]
        public void PostsSavedInCartGet_FromCartAll_ShouldBeEqualToPostsSavedInCart()
        {
            Assert.That(cartInitializedWithGroupUserPosts.PostsSavedInCart, Is.EqualTo(postsSavedInCart));
        }

        [Test]
        public void AddPostToCart_AddingPostThatDoesntAlreadyExistInTheCart_PostsListShouldContainPost()
        {
            cartInitializedWithGroupUserPosts.AddPostToCart(postToSave);
            Assert.Contains(postToSave, cartInitializedWithGroupUserPosts.PostsSavedInCart);
        }

        [Test]
        public void AddPostToCart_AddingPostThatAlreadyExists_ShouldThrowException()
        {
            cartInitializedWithGroupUserPosts.AddPostToCart(postToSave);
            var exceptionMessage = Assert.Throws<Exception>(() => { cartInitializedWithGroupUserPosts.AddPostToCart(postToSave); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post already in cart"));
        }

        [Test]
        public void RemovePostFromCart_RemovePostThatExists_PostsListShouldNotContainPostAnymore()
        {
            cartInitializedWithGroupUserPosts.AddPostToCart(postToSave);
            cartInitializedWithGroupUserPosts.RemovePostFromCart(postToSave);
            Assert.IsFalse(cartInitializedWithGroupUserPosts.PostsSavedInCart.Contains(postToSave));
        }

        [Test]
        public void RemovePostFromCart_RemovePostThatDoesntExist_ShouldThrowException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { cartInitializedWithGroupUserPosts.RemovePostFromCart(postToSave); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not in cart"));
        }
    }
}
