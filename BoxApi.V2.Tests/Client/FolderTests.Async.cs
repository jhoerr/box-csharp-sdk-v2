using System;
using System.Linq;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class FolderTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            Client.GetFolder(folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, "All Files", null, RootId);
                }, AbortOnFailure, RootId);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void GetNonExistentFolderAsync()
        {
            var failureOccured = false;
            Client.GetFolder(folder => { }, (error) => { failureOccured = true; }, "abc");

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void CreateFolderAsync()
        {
            var folderName = TestItemName();
            var callbackHit = false;

            Client.CreateFolder(folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, folderName, RootId);
                    // clean up 
                    Client.DeleteFolder(folder.Id, true);
                }, AbortOnFailure, RootId, folderName, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void CreateFolderWithIllegalNameAsync()
        {
            var failureOccured = false;

            Client.CreateFolder(folder => { }, (error) => failureOccured = true, RootId, "\\bad name:", null);

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void DeleteFolderAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            Client.DeleteFolder(response => callbackHit = true, AbortOnFailure, folder.Id, true);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }

            try
            {
                Client.Get(folder, null);
                Assert.Fail("Should not be able to fetch the deleted folder.");
            }
            catch
            {
            }
        }

        [Test]
        public void DeleteNonExistentFolderAsync()
        {
            var failureOccured = false;
            Client.DeleteFolder(response => { }, (error) => { failureOccured = true; }, "abc", true);

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void CopyFolderAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder", null);
            var copyName = TestItemName();
            Client.CopyFolder(copiedFolder =>
                {
                    callbackHit = true;
                    Assert.That(copiedFolder.ItemCollection.TotalCount, Is.EqualTo(1));
                    Client.DeleteFolder(folder.Id, true);
                    Client.DeleteFolder(copiedFolder.Id, true);
                }, AbortOnFailure, folder.Id, RootId, copyName, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void ShareFolderLinkAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanPreview = true, CanDownload = true});

            Client.ShareFolderLink(copiedFolder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, folderName, RootId);
                    AssertSharedLink(folder.SharedLink, sharedLink);
                    Client.DeleteFolder(folder.Id, true);
                }, AbortOnFailure, folder.Id, sharedLink, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void MoveFolderAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var targetFolderName = TestItemName();
            var targetFolder = Client.CreateFolder(RootId, targetFolderName, null);

            Client.MoveFolder(movedFolder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(movedFolder, folderName, targetFolder.Id, folder.Id);
                    Client.DeleteFolder(targetFolder.Id, true);
                }, AbortOnFailure, folder.Id, targetFolder.Id, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void RenameFolderAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var newName = TestItemName();

            Client.Rename(renamedFolder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(renamedFolder, newName, RootId, folder.Id);
                    Client.DeleteFolder(renamedFolder.Id, true);
                }, AbortOnFailure, folder, newName, null);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        [Test]
        public void GetFolderItemsAsync()
        {
            var callbackHit = false;

            var testFolder = Client.CreateFolder(RootId, TestItemName(), null);
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName(), null);
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName(), null);

            Client.GetItems(contents =>
                {
                    callbackHit = true;
                    Assert.That(contents, Is.Not.Null);
                    Assert.That(contents.TotalCount, Is.EqualTo(2));
                    Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);
                    Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
                    Client.DeleteFolder(testFolder.Id, true);
                }, AbortOnFailure, testFolder.Id, new[] { FolderField.Name,});

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

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
            var folder = Client.CreateFolder(RootId, TestItemName(), null);
            var newDescription = "new description";

            // Act
            folder.Description = newDescription;
            Client.UpdateDescription(updatedFolder =>
                {
                    // Assert
                    AssertFolderConstraints(updatedFolder, folder.Name, folder.Parent.Id, folder.Id);
                    Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
                    callbackHit = true;
                }, AbortOnFailure, folder, newDescription, null);

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


        [Test]
        public void UpdateEverythingAsync()
        {
            // Arrange
            var callbackHit = false;
            var fileName = TestItemName();
            var folder = Client.CreateFolder(RootId, fileName, null);
            var newDescription = "new description";
            var newFolder = TestItemName();
            var newParent = Client.CreateFolder(RootId, newFolder, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            var newName = TestItemName();

            // Act
            folder.Name = newName;
            folder.Description = newDescription;
            folder.Parent.Id = newParent.Id;
            folder.SharedLink = sharedLink;
            Client.Update(updatedFolder =>
                {
                    // Assert
                    Client.Delete(updatedFolder, true);
                    AssertFolderConstraints(updatedFolder, newName, newParent.Id, folder.Id);
                    AssertSharedLink(sharedLink, updatedFolder.SharedLink);
                    Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
                    callbackHit = true;
                }, AbortOnFailure, folder, null);

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