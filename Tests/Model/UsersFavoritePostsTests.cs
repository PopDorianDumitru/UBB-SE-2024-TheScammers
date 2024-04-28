using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class UsersFavoritePostsTests
    {
        private UsersFavoritePosts favorites;

        [SetUp]
        public void SetUp()
        {
            favorites = new UsersFavoritePosts();
        }

        [Test]
        public void UserId_Anything_ReturnsUserId()
        {
            Guid userId = Guid.NewGuid();
            UsersFavoritePosts otherFavorites = new UsersFavoritePosts(userId, Guid.NewGuid(), new List<Guid>());

            Assert.That(otherFavorites.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void GroupId_Anything_ReturnsGroupId()
        {
            Guid groupId = Guid.NewGuid();
            UsersFavoritePosts otherFavorites = new UsersFavoritePosts(Guid.NewGuid(), groupId, new List<Guid>());

            Assert.That(otherFavorites.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void Posts_Anything_ReturnsPosts()
        {
            var posts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            UsersFavoritePosts otherFavorites = new UsersFavoritePosts(Guid.NewGuid(), Guid.NewGuid(), posts);

            Assert.That(otherFavorites.Posts, Is.EqualTo(posts));
        }

        [Test]
        public void AddPost_TwoDifferentPosts_PostsAreAdded()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            favorites.AddPost(guidOfFirstFavorite);
            favorites.AddPost(guidOfSecondFavorite);

            List<Guid> expectedGuids = new List<Guid>
            {
                guidOfFirstFavorite,
                guidOfSecondFavorite,
            };
            List<Guid> actualGuids = favorites.Posts;

            Assert.That(actualGuids, Is.EqualTo(expectedGuids));
        }

        [Test]
        public void AddPost_TwoEqualPosts_ExceptionThrown()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = guidOfFirstFavorite;
            favorites.AddPost(guidOfFirstFavorite);

            var exceptionMessage = Assert.Throws<Exception>(() => { favorites.AddPost(guidOfSecondFavorite); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post already in favorites"));
        }

        [Test]
        public void RemovePost_ExistentPost_PostIsRemoved()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            favorites.AddPost(guidOfFirstFavorite);
            favorites.AddPost(guidOfSecondFavorite);

            List<Guid> listOfGuidsToVerifySuccessfulRemoval = new List<Guid>()
            {
                guidOfSecondFavorite,
            };

            favorites.RemovePost(guidOfFirstFavorite);
            List<Guid> actualGuids = favorites.Posts;

            Assert.That(actualGuids, Is.EqualTo(listOfGuidsToVerifySuccessfulRemoval));
            Assert.That(actualGuids.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemovePost_NonexistentPost_ExceptionThrown()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            favorites.AddPost(guidOfFirstFavorite);
            favorites.AddPost(guidOfSecondFavorite);

            favorites.RemovePost(guidOfFirstFavorite);

            var exceptionMessage = Assert.Throws<Exception>(() => { favorites.RemovePost(guidOfFirstFavorite); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not in favorites"));
        }
    }
}
