using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.Model.Repositories;
using Moq;

namespace Tests.Model.Repositories
{
    public class PostRepositoryTests
    {
        public PostRepository postRepository;

        [SetUp]
        public void SetUp()
        {
            postRepository = new PostRepository();
        }

        [Test]
        public void AddPost_AnyPost_ThePostIsAdded()
        {
            Post post = new Post("media_name", Guid.NewGuid(), Guid.NewGuid(), "Cluj", "at Cluj", "My Post", "mom", "food", false);

            postRepository.AddPost(post);

            Assert.That(postRepository.GetAllPosts().Count, Is.EqualTo(1));
            Assert.That(postRepository.GetAllPosts()[0], Is.EqualTo(post));
        }

        [Test]
        public void RemovePost_PostExists_ThePostIsRemoved()
        {
            Guid postGuid = Guid.NewGuid();
            Post post = new Post(postGuid, new List<Guid>(), new List<Guid>(), new List<Comment>(), "", new DateTime(), Guid.NewGuid(),
                Guid.NewGuid(), false, new List<Guid>(), "", "", "", new List<InterestStatus>(), "", new List<Report>(), "", false, 0);
            postRepository.AddPost(post);

            postRepository.RemovePost(postGuid);

            Assert.That(postRepository.GetAllPosts().Count, Is.EqualTo(0));
        }

        [Test]
        public void RemovePost_PostDoesNotExist_NoPostsAreRemoved()
        {
            Guid postGuid = Guid.NewGuid();
            Post post = new Post(postGuid, new List<Guid>(), new List<Guid>(), new List<Comment>(), "", new DateTime(), Guid.NewGuid(),
                Guid.NewGuid(), false, new List<Guid>(), "", "", "", new List<InterestStatus>(), "", new List<Report>(), "", false, 0);
            postRepository.AddPost(post);

            postRepository.RemovePost(Guid.NewGuid());

            Assert.That(postRepository.GetAllPosts().Count, Is.EqualTo(1));
            Assert.That(postRepository.GetAllPosts()[0], Is.EqualTo(post));
        }

        [Test]
        public void GetPostById_ValidId_ThePostIsReturned()
        {
            Guid postGuid = Guid.NewGuid();
            Post post = new Post(postGuid, new List<Guid>(), new List<Guid>(), new List<Comment>(), "", new DateTime(), Guid.NewGuid(),
                Guid.NewGuid(), false, new List<Guid>(), "", "", "", new List<InterestStatus>(), "", new List<Report>(), "", false, 0);
            postRepository.AddPost(post);

            Post gotByIdPost = postRepository.GetPostById(postGuid);

            Assert.That(gotByIdPost, Is.EqualTo(post));
        }

        [Test]
        public void GetPostById_InvalidId_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { postRepository.GetPostById(Guid.NewGuid()); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Post does not exist!"));
        }

        [Test]
        public void GetAllPosts_NoPosts_ReturnsEmptyList()
        {
            var allPosts = postRepository.GetAllPosts();

            Assert.That(allPosts.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllPosts_AtLeastOnePost_ReturnsThePosts()
        {
            Post firstPost = new Post(Guid.NewGuid(), new List<Guid>(), new List<Guid>(), new List<Comment>(), "", new DateTime(), Guid.NewGuid(),
                Guid.NewGuid(), false, new List<Guid>(), "", "", "", new List<InterestStatus>(), "", new List<Report>(), "", false, 0);
            Post secondPost = new Post(Guid.NewGuid(), new List<Guid>(), new List<Guid>(), new List<Comment>(), "2", new DateTime(), Guid.NewGuid(),
                Guid.NewGuid(), false, new List<Guid>(), "2", "2", "2", new List<InterestStatus>(), "2", new List<Report>(), "2", true, 2);
            postRepository.AddPost(firstPost);
            postRepository.AddPost(secondPost);

            var allPosts = postRepository.GetAllPosts();

            Assert.That(allPosts.Count, Is.EqualTo(2));
            Assert.That(allPosts, Is.EquivalentTo(new List<Post> { firstPost, secondPost }));
        }
    }
}
