using ISSLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Model
{
    internal class FavoritesTests
    {
        private Favorites _favorites;

        [SetUp]
        public void SetUp()
        {
            _favorites = new Favorites();
        }

        [Test]
        public void AddPost_AnyPost_AddsPost()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            _favorites.AddPost(guidOfFirstFavorite);
            _favorites.AddPost(guidOfSecondFavorite);

            List<Guid> expectedGuids = new List<Guid>
            {
                guidOfFirstFavorite,
                guidOfSecondFavorite,
            };
            List<Guid> actualGuids = _favorites.Posts;

            Assert.That(actualGuids, Is.EqualTo(expectedGuids));
            Assert.That(actualGuids.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddPost_AnyPost_ExceptionThrown()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = guidOfFirstFavorite;
            _favorites.AddPost(guidOfFirstFavorite);
            
            var exceptionMessage = Assert.Throws<Exception>(() => { _favorites.AddPost(guidOfSecondFavorite); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post already in favorites"));
        }

        [Test]
        public void RemovePost_AnyPost_RemovesPost()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            _favorites.AddPost(guidOfFirstFavorite);
            _favorites.AddPost(guidOfSecondFavorite);

            List<Guid> listOfGuidsToVerifySuccessfulRemoval = new List<Guid>()
            {
                guidOfSecondFavorite,
            };

            _favorites.RemovePost(guidOfFirstFavorite);
            List<Guid> actualGuids = _favorites.Posts;

            Assert.That(actualGuids, Is.EqualTo(listOfGuidsToVerifySuccessfulRemoval));
            Assert.That(actualGuids.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemovePost_AnyPost_ExceptionThrown()
        {
            Guid guidOfFirstFavorite = Guid.NewGuid();
            Guid guidOfSecondFavorite = Guid.NewGuid();
            _favorites.AddPost(guidOfFirstFavorite);
            _favorites.AddPost(guidOfSecondFavorite);

            _favorites.RemovePost(guidOfFirstFavorite);

            var exceptionMessage = Assert.Throws<Exception>(() => { _favorites.RemovePost(guidOfFirstFavorite); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post not in favorites"));
        }
    }
}
