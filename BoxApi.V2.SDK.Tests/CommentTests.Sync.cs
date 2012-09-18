using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class CommentTestsSync : BoxApiTestHarness
    {
        [Test]
        public void AddComment()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string message = "the message";
            try
            {
                Comment comment = Client.AddComment(file, message);
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
        public void GetComment()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string message = "the message";
            try
            {
                Comment expected = Client.AddComment(file, message);
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
        public void GetComments()
        {
            File file = Client.CreateFile(RootId, TestItemName());
            string firstComment = "first comment";
            string secondComment = "second comment";
            try
            {
                Client.AddComment(file, firstComment);
                Client.AddComment(file, secondComment);
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
    }
}
