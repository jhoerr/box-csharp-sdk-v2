using System.Linq;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class CommentTestsSync : BoxApiTestHarness
    {
        [Test]
        public void Add()
        {
            var file = Client.CreateFile(RootId, TestItemName());
            var message = "the message";
            try
            {
                var comment = Client.CreateComment(file, message);
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
            var file = Client.CreateFile(RootId, TestItemName());
            var message = "the message";
            try
            {
                var expected = Client.CreateComment(file, message);
                var actual = Client.GetComment(expected);
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
            var file = Client.CreateFile(RootId, TestItemName());
            var firstComment = "first comment";
            var secondComment = "second comment";
            try
            {
                Client.CreateComment(file, firstComment);
                Client.CreateComment(file, secondComment);
                var comments = Client.GetComments(file);
                Assert.That(comments.TotalCount, Is.EqualTo(2));
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
            var file = Client.CreateFile(RootId, TestItemName());
            var originalComment = "originalComment";
            var newComment = "newComment";
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
            var file = Client.CreateFile(RootId, TestItemName());
            var originalComment = "originalComment";
            try
            {
                var comment = Client.CreateComment(file, originalComment);
                Client.Delete(comment);
                var commentCollection = Client.GetComments(file);
                Assert.That(commentCollection.TotalCount, Is.EqualTo(0));
            }
            finally
            {
                Client.Delete(file);
            }
        }
    }
}