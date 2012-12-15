using System;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using NUnit.Framework;

namespace BoxApi.V2.Tests
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
            Client.CreateFile(file =>
                {
                    actual = file;
                    AssertFileConstraints(file, testItemName, RootId);
                    callbackHit = true;
                }, AbortOnFailure, RootId, testItemName, null);

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

            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName, expected);

            Client.Read(readBytes =>
                {
                    Assert.That(readBytes, Is.EqualTo(expected));
                    callbackHit = true;
                }, AbortOnFailure, file);

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
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            // Act
            Client.Write(updatedFile =>
                {
                    var actual = Client.Read(file.Id);
                    Assert.That(actual, Is.EqualTo(expected));
                    callbackHit = true;
                }, AbortOnFailure, file, expected);

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
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var newItemName = TestItemName();
            // Act
            Client.Copy(copiedFile =>
                {
                    // Assert
                    AssertFileConstraints(copiedFile, newItemName, RootId);
                    Assert.That(copiedFile.Id, Is.Not.EqualTo(file.Id));
                    callbackHit = true;
                }, AbortOnFailure, file, RootId, newItemName, null);

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
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});

            // Act
            Client.ShareLink(sharedFile =>
                {
                    // Assert
                    Client.Delete(sharedFile);
                    AssertFileConstraints(sharedFile, file.Name, RootId, file.Id);
                    AssertSharedLink(sharedFile.SharedLink, expectedLink);
                    callbackHit = true;
                }, AbortOnFailure, file, expectedLink, null);

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
        public void RenameFileAsync()
        {
            // Arrange
            var callbackHit = false;
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var newItemName = TestItemName();
            // Act
            Client.Rename(renamedFile =>
                {
                    // Assert
                    AssertFileConstraints(renamedFile, newItemName, RootId);
                    callbackHit = true;
                }, AbortOnFailure, file, newItemName, null);

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
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            // Act
            Client.Move(movedFile =>
                {
                    // Assert
                    AssertFileConstraints(movedFile, testItemName, folder.Id, file.Id);
                    callbackHit = true;
                }, AbortOnFailure, file, folder, null);

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
        public void UpdateDescriptionAsync()
        {
            // Arrange
            var callbackHit = false;
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var newDescription = "new description";

            // Act
            Client.UpdateDescription(updatedFile =>
                {
                    // Assert
                    Client.Delete(updatedFile);
                    AssertFileConstraints(updatedFile, file.Name, RootId, file.Id);
                    Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
                    callbackHit = true;
                }, AbortOnFailure, file, newDescription, null);

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
        public void UpdateAsync()
        {
            // Arrange
            var callbackHit = false;
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var newDescription = "new description";
            var newFolder = TestItemName();
            var folder = Client.CreateFolder(RootId, newFolder, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            var newName = TestItemName();

            // Act
            file.Name = newName;
            file.Description = newDescription;
            file.Parent.Id = folder.Id;
            file.SharedLink = sharedLink;
            Client.Update(updatedFile =>
                {
                    // Assert
                    AssertFileConstraints(updatedFile, newName, folder.Id, file.Id);
                    AssertSharedLink(sharedLink, updatedFile.SharedLink);
                    Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
                    callbackHit = true;
                }, AbortOnFailure, file, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(folder, true);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }
    }
}