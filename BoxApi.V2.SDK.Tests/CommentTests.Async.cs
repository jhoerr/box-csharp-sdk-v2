using System.Threading;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class CommentTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void AddComment()
        {
            // Arrange
            var callbackHit = false;
            string comment = "my comment!";
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);

            // Act
            Client.AddComment(file, comment, newComment =>
            {
                // Assert
                Assert.That(newComment, Is.Not.Null);
                Assert.That(newComment.Message, Is.EqualTo(comment));
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(file);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void GetComment()
        {
            // Arrange
            var callbackHit = false;
            string message = "my comment!";
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var comment = Client.AddComment(file, message);

            // Act
            Client.GetComment(comment, gotComment =>
            {
                // Assert
                Assert.That(gotComment, Is.Not.Null);
                Assert.That(gotComment.Message, Is.EqualTo(message));
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(file);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void GetComments()
        {
            // Arrange
            var callbackHit = false;
            string message1 = "my comment!";
            string message2 = "my other comment!";
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            Client.AddComment(file, message1);
            Client.AddComment(file, message2);

            // Act
            Client.GetComments(file, comments =>
            {
                // Assert
                Assert.That(comments, Is.Not.Null);
                Assert.That(comments.TotalCount, Is.EqualTo("2"));
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(file);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }
    }
}