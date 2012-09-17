using System;
using System.Threading;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FileTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void CreateFileAsync()
        {
            var callbackHit = false;

            var testItemName = TestItemName();
            File actual = null;
            Client.CreateFileAsync(RootId, testItemName, file =>
                {
                    actual = file;
                    AssertFileConstraints(file, testItemName, RootId);
                    callbackHit = true;
                }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
            else if (actual != null)
            {
                Client.Delete(actual);
            }
        }

        [Test]
        public void ReadFileAsync()
        {
            var callbackHit = false;

            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName, expected);
            
            Client.ReadAsync(file, readBytes =>
            {
                Assert.That(readBytes, Is.EqualTo(expected));
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            Client.Delete(file);
            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void WriteFileAsync()
        {
            // Arrange
            var callbackHit = false;
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            // Act
            Client.WriteAsync(file, expected, updatedFile =>
                {
                    byte[] actual = Client.Read(file.Id);
                    Assert.That(actual, Is.EqualTo(expected));
                    callbackHit = true;
                }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(Client.GetFile(file.Id));

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void CopyFileAsync()
        {
            // Arrange
            var callbackHit = false;
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            string newItemName = TestItemName();
            // Act
            Client.CopyAsync(file, RootId, copiedFile =>
            {
                // Assert
                AssertFileConstraints(copiedFile, newItemName, RootId);
                Assert.That(copiedFile.Id, Is.Not.EqualTo(file.Id)); 
                callbackHit = true;
            }, AbortOnFailure, newItemName);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(Client.GetFile(file.Id));

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void ShareFileLinkAsync()
        {
            // Arrange
            var callbackHit = false;
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions() {Download = true, Preview = true});
            
            // Act
            Client.ShareLinkAsync(file, expectedLink, sharedFile =>
            {
                // Assert
                AssertFileConstraints(sharedFile, file.Name, RootId, file.Id);
                AssertSharedLink(sharedFile.SharedLink, expectedLink);
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(Client.GetFile(file.Id));

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void RenameFileAsync()
        {
            // Arrange
            var callbackHit = false;
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            string newItemName = TestItemName();
            // Act
            Client.RenameAsync(file, newItemName, renamedFile =>
            {
                // Assert
                AssertFileConstraints(renamedFile, newItemName, RootId);
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(Client.GetFile(file.Id));

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void MoveFileAsync()
        {
            // Arrange
            var callbackHit = false;
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            string folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName);
            // Act
            Client.MoveAsync(file, folder, movedFile =>
            {
                // Assert
                AssertFileConstraints(movedFile, testItemName, folder.Id, file.Id);
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(Client.GetFile(file.Id));

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }
    }
}