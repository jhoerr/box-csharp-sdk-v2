using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class CommentTestsSync : BoxApiTestHarness
    {
        [Test]
        public void Add()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string message = "the message";
            try
            {
                Comment comment = Client.CreateComment(file, message);
                Assert.That(comment, Is.Not.Null);
                Assert.That(comment.Item.Id, Is.EqualTo(file.Id));
                Assert.That(comment.Message, Is.EqualTo(message));
                Assert.That(comment.IsReplyComment, Is.False);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void Get()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string message = "the message";
            try
            {
                Comment expected = Client.CreateComment(file, message);
                Comment actual = Client.GetComment(expected);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Item.Id, Is.EqualTo(expected.Item.Id));
                Assert.That(actual.Message, Is.EqualTo(expected.Message));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void GetAll()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string firstComment = "first comment";
            string secondComment = "second comment";
            try
            {
                Client.CreateComment(file, firstComment);
                Client.CreateComment(file, secondComment);
                var comments = Client.GetComments(file);
                Assert.That(comments.TotalCount, Is.EqualTo("2"));
                Assert.That(comments.Entries.Any(c => c.Message.Equals(firstComment)), Is.True);
                Assert.That(comments.Entries.Any(c => c.Message.Equals(secondComment)), Is.True);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void Update()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string originalComment = "originalComment";
            string newComment = "newComment";
            try
            {
                var comment = Client.CreateComment(file, originalComment);
                comment.Message = newComment;
                var updatedComment = Client.Update(comment);
                Assert.That(updatedComment, Is.Not.Null);
                Assert.That(updatedComment.Message, Is.EqualTo(newComment));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void Delete()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string originalComment = "originalComment";
            try
            {
                var comment = Client.CreateComment(file, originalComment);
                Client.Delete(comment);
                var commentCollection = Client.GetComments(file);
                Assert.That(commentCollection.TotalCount, Is.EqualTo("0"));
            }
            finally
            {
                Client.Delete(file);
            }
        }
    }
}
