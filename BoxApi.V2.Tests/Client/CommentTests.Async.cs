using System.Threading;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class CommentTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void Add()
        {
            // Arrange
            var callbackHit = false;
            var comment = "my comment!";
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);

            // Act
            Client.CreateComment(newComment =>
                {
                    // Assert
                    Assert.That(newComment, Is.Not.Null);
                    Assert.That(newComment.Message, Is.EqualTo(comment));
                    callbackHit = true;
                }, AbortOnFailure, file, comment, null);

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
        public void Get()
        {
            // Arrange
            var callbackHit = false;
            var message = "my comment!";
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var comment = Client.CreateComment(file, message);

            // Act
            Client.GetComment(gotComment =>
                {
                    // Assert
                    Assert.That(gotComment, Is.Not.Null);
                    Assert.That(gotComment.Message, Is.EqualTo(message));
                    callbackHit = true;
                }, AbortOnFailure, comment, null);

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
        public void GetAll()
        {
            // Arrange
            var callbackHit = false;
            var message1 = "my comment!";
            var message2 = "my other comment!";
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            Client.CreateComment(file, message1);
            Client.CreateComment(file, message2);

            // Act
            Client.GetComments(comments =>
                {
                    // Assert
                    Assert.That(comments, Is.Not.Null);
                    Assert.That(comments.TotalCount, Is.EqualTo(2));
                    callbackHit = true;
                }, AbortOnFailure, file, null);

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
        public void Update()
        {
            var callbackHit = false;
            var file = Client.CreateFile(RootId, TestItemName());
            var newComment = "newComment";
            var comment = Client.CreateComment(file, "originalComment");
            comment.Message = newComment;
            // Act
            Client.Update(updatedComment =>
                {
                    // Assert
                    Client.Delete(file);
                    Assert.That(updatedComment, Is.Not.Null);
                    Assert.That(updatedComment.Message, Is.EqualTo(newComment));
                    callbackHit = true;
                }, AbortOnFailure, comment, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void Delete()
        {
            var callbackHit = false;
            var file = Client.CreateFile(RootId, TestItemName());
            var comment = Client.CreateComment(file, "originalComment");

            // Act
            Client.Delete(response =>
                {
                    var commentCollection = Client.GetComments(file);
                    Client.Delete(file);
                    Assert.That(commentCollection.TotalCount, Is.EqualTo(0));
                    callbackHit = true;
                }, AbortOnFailure, comment);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }
    }
}