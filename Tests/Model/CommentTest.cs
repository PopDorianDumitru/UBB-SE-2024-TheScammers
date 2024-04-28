using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class CommentTest
    {
        private Guid commentId;
        private Guid userId;
        private string content;
        private List<Comment> replies;

        private Comment commentEmpty;
        private Comment commentUserContent;
        private Comment commentAll;

        private Comment commentToAdd;

        [SetUp]
        public void SetUp()
        {
            commentId = Guid.NewGuid();
            userId = Guid.NewGuid();
            content = "text";
            replies = new List<Comment>();

            commentEmpty = new Comment();
            commentUserContent = new Comment(userId, content);
            commentAll = new Comment(commentId, userId, content, replies);
            commentToAdd = new Comment();
        }

        [Test]
        public void CommentIdGet_GetCommentIdFromEmptyComment_ShouldNotBeEmpty()
        {
            Assert.That(commentEmpty.CommentId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void CommentIdGet_GetCommentIdFromCommentUserContent_ShouldNotBeEmpty()
        {
            Assert.That(commentUserContent.CommentId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void CommentIdGet_GetCommentIdFromCommentAll_ShouldBeEqualToCommentId()
        {
            Assert.That(commentAll.CommentId, Is.EqualTo(commentId));
        }

        [Test]
        public void UserIdGet_GetUserIdFromEmptyComment_ShouldNotBeEmpty()
        {
            Assert.That(commentEmpty.UserId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void UserIdGet_GetUserIdFromCommentUserContent_ShouldBeEqualToUserId()
        {
            Assert.That(commentUserContent.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void UserIdGet_GetUserIdFromCommentAll_ShouldBeEqualToUserId()
        {
            Assert.That(commentAll.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void ContentGet_GetContentFromEmptyComment_ShouldBeEmptyString()
        {
            Assert.That(commentEmpty.Content, Is.Empty);
        }

        [Test]
        public void ContentGet_GetUserIdFromCommentUserContent_ShouldBeEqualToContent()
        {
            Assert.That(commentUserContent.Content, Is.EqualTo(content));
        }

        [Test]
        public void ContentGet_GetUserIdFromCommentAll_ShouldBeEqualToContent()
        {
            Assert.That(commentAll.Content, Is.EqualTo(content));
        }

        [Test]
        public void RepliesGet_GetRepliesFromEmptyComment_ShouldBeEmptyList()
        {
            Assert.That(commentEmpty.Replies, Is.Empty);
        }

        [Test]
        public void RepliesGet_GetRepliesFromCommentUserContent_ShouldBeEmptyList()
        {
            Assert.That(commentUserContent.Replies, Is.Empty);
        }

        [Test]
        public void RepliesGet_GetRepliesFromCommentAll_ShouldBeEqualToReplies()
        {
            Assert.That(commentAll.Replies, Is.EqualTo(replies));
        }

        [Test]
        public void ContentSet_ChangeContentForEmptyComment_ContentShouldBeEqualToNewValue()
        {
            string newValue = "hello";
            commentEmpty.Content = newValue;
            Assert.That(commentEmpty.Content, Is.EqualTo(newValue));
        }

        [Test]
        public void ContentSet_ChangeContentForCommentUserContent_ContentShouldBeEqualToNewValue()
        {
            string newValue = "hello";
            commentUserContent.Content = newValue;
            Assert.That(commentUserContent.Content, Is.EqualTo(newValue));
        }

        [Test]
        public void ContentSet_ChangeContentForCommentAll_ContentShouldBeEqualToNewValue()
        {
            string newValue = "hello";
            commentAll.Content = newValue;
            Assert.That(commentAll.Content, Is.EqualTo(newValue));
        }

        [Test]
        public void AddReply_AddReplyToEmptyComment_RepliesLengthShouldBe1()
        {
            commentEmpty.AddReply(commentToAdd);
            Assert.That(commentEmpty.Replies, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddReply_AddReplyToEmptyComment_ReplyShouldContainAddedComment()
        {
            commentEmpty.AddReply(commentToAdd);
            Assert.Contains(commentToAdd, commentEmpty.Replies);
        }

        [Test]
        public void AddReply_AddReplyToCommentUserContent_RepliesLengthShouldBe1()
        {
            commentUserContent.AddReply(commentToAdd);
            Assert.That(commentUserContent.Replies, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddReply_AddReplyToCommentUserContent_ReplyShouldContainAddedComment()
        {
            commentUserContent.AddReply(commentToAdd);
            Assert.Contains(commentToAdd, commentUserContent.Replies);
        }

        [Test]
        public void AddReply_AddReplyToCommentAll_RepliesLengthShouldBe1()
        {
            commentAll.AddReply(commentToAdd);
            Assert.That(commentAll.Replies, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddReply_AddReplyToCommentAll_ReplyShouldContainAddedComment()
        {
            commentAll.AddReply(commentToAdd);
            Assert.Contains(commentToAdd, commentAll.Replies);
        }
    }
}
