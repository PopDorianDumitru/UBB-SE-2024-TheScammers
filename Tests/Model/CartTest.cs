using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class CartTest
    {
        public Cart cartEmpty;
        public Cart cartGroupUser;
        public Cart cartAll;

        public Guid groupId;
        public Guid userId;
        public List<Guid> postsSavedInCart;

        Guid postToSave;

        [SetUp]
        public void SetUp()
        {
            postToSave = Guid.NewGuid();
            groupId = Guid.NewGuid();
            userId = Guid.NewGuid();
            postsSavedInCart = new List<Guid>();
            cartEmpty = new Cart();
            cartGroupUser = new Cart(groupId, userId);
            cartAll = new Cart(groupId, userId, postsSavedInCart);

        }

        [Test]
        public void GroupIdGet_GroupIdFromEmptyCart_ShouldNotBeEmpty()
        {
            Assert.That(cartEmpty.GroupId, Is.Not.EqualTo(Guid.Empty));
        }
        [Test]
        public void GroupIdGet_GroupIdFromCartGroupUser_ShouldBeEqualToGroupId()
        {
            Assert.That(cartGroupUser.GroupId, Is.EqualTo(groupId));
        }
        [Test]
        public void GroupIdGet_GroupIdFromCartAll_ShouldBeEqualToGroupId()
        {
            Assert.That(cartAll.GroupId, Is.EqualTo(groupId));
        }
        [Test]
        public void UserIdGet_UserIdFromEmptyCart_ShouldNotBeEmpty()
        {
            Assert.That(cartEmpty.UserId, Is.Not.EqualTo(Guid.Empty));
        }
        [Test]
        public void UserIdGet_UserIdFromCartGroupUser_ShouldBeEqualToUserId()
        {
            Assert.That(cartGroupUser.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void UserIdGet_UserIdFromCartAll_ShouldBeEqualToUserId()
        {
            Assert.That(cartAll.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void PostsSavedInCartGet_FromCartEmpty_ShouldBeEmptyList()
        {
            Assert.That(cartEmpty.PostsSavedInCart, Is.Empty);
        }

        [Test]
        public void PostsSavedInCartGet_FromCartGroupUser_ShouldBeEmptyList()
        {
            Assert.That(cartGroupUser.PostsSavedInCart, Is.Empty);
        }

        [Test]
        public void PostsSavedInCartGet_FromCartAll_ShouldBeEqualToPostsSavedInCart()
        {
            Assert.That(cartAll.PostsSavedInCart, Is.EqualTo(postsSavedInCart));
        }

        [Test]
        public void AddPostToCart_AddingPostThatDoesntAlreadyExistInTheCart_PostsListShouldContainPost()
        {
            cartAll.AddPostToCart(postToSave);
            Assert.Contains(postToSave, cartAll.PostsSavedInCart);
        }

        [Test]
        public void AddPostToCart_AddingPostThatAlreadyExists_ShouldThrowException()
        {
            cartAll.AddPostToCart(postToSave);
            var exceptionMessage = Assert.Throws<Exception>(() => { cartAll.AddPostToCart(postToSave); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post already in cart"));
        }

        [Test]
        public void RemovePostFromCart_RemovePostThatExists_PostsListShouldNotContainPostAnymore()
        {
            cartAll.AddPostToCart(postToSave);
            cartAll.RemovePostFromCart(postToSave);
            Assert.IsFalse(cartAll.PostsSavedInCart.Contains(postToSave));
        }

        [Test]
        public void RemovePostFromCart_RemovePostThatDoesntExist_ShouldThrowException()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { cartAll.RemovePostFromCart(postToSave); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not in cart"));
        }
    }
}
