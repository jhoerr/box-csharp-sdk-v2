using System;
using System.Linq;
using System.Threading;
using BoxApi.V2;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class FolderTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            Client.GetFolder(RootId, folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, "All Files", null, RootId);
                }, AbortOnFailure);

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
            Client.GetFolder("abc", folder => { }, () => { failureOccured = true; });

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

            Client.CreateFolder(RootId, folderName, folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, folderName, RootId);
                    // clean up 
                    Client.DeleteFolder(folder.Id, true);
                }, AbortOnFailure);

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

            Client.CreateFolder(RootId, "\\bad name:", folder => { }, () => failureOccured = true);

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
            var folder = Client.CreateFolder(RootId, folderName);
            Client.DeleteFolder(folder.Id, true, response => callbackHit = true, AbortOnFailure);

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
                Client.Get(folder);
                Assert.Fail("Should not be able to fetch the deleted folder.");
            }
            catch (BoxException e)
            {
            }
        }

        [Test]
        public void DeleteNonExistentFolderAsync()
        {
            var failureOccured = false;
            Client.DeleteFolder("abc", true, response => { }, () => { failureOccured = true; });

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
            var folder = Client.CreateFolder(RootId, folderName);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder");
            var copyName = TestItemName();
            Client.CopyFolder(folder.Id, RootId, copiedFolder =>
                {
                    callbackHit = true;
                    Assert.That(copiedFolder.ItemCollection.TotalCount, Is.EqualTo("1"));
                    Client.DeleteFolder(folder.Id, true);
                    Client.DeleteFolder(copiedFolder.Id, true);
                }, AbortOnFailure, copyName );

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
            var folder = Client.CreateFolder(RootId, folderName);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions { Preview = true, Download = true });

            Client.ShareFolderLink(folder.Id, sharedLink, copiedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(folder, folderName, RootId);
                AssertSharedLink(folder.SharedLink, sharedLink);
                Client.DeleteFolder(folder.Id, true);
            }, AbortOnFailure);

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
            Folder folder = Client.CreateFolder(RootId, folderName);
            var targetFolderName = TestItemName();
            Folder targetFolder = Client.CreateFolder(RootId, targetFolderName);

            Client.MoveFolder(folder.Id, targetFolder.Id, movedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(movedFolder, folderName, targetFolder.Id, folder.Id);
                Client.DeleteFolder(targetFolder.Id, true);
            }, AbortOnFailure);

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
            Folder folder = Client.CreateFolder(RootId, folderName);
            var newName = TestItemName();

            Client.Rename(folder, newName, renamedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(renamedFolder, newName, RootId, folder.Id);
                Client.DeleteFolder(renamedFolder.Id, true);
            }, AbortOnFailure);

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

            var testFolder = Client.CreateFolder(RootId, TestItemName());
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName());
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName());
       
            Client.GetItems(testFolder.Id, contents =>
            {
                callbackHit = true;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents.TotalCount, Is.EqualTo("2"));
                Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);
                Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
                Client.DeleteFolder(testFolder.Id, true);
            }, AbortOnFailure);

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
            var folder = Client.CreateFolder(RootId, TestItemName());
            string newDescription = "new description";

            // Act
            folder.Description = newDescription;
            Client.UpdateDescription(folder, newDescription, updatedFolder =>
            {
                // Assert
                AssertFolderConstraints(updatedFolder, folder.Name, folder.Parent.Id, folder.Id);
                Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
                callbackHit = true;
            }, AbortOnFailure);

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
            string fileName = TestItemName();
            var folder = Client.CreateFolder(RootId, fileName);
            string newDescription = "new description";
            string newFolder = TestItemName();
            var newParent = Client.CreateFolder(RootId, newFolder);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions() { Download = true, Preview = true });
            string newName = TestItemName();

            // Act
            folder.Name = newName;
            folder.Description = newDescription;
            folder.Parent.Id = newParent.Id;
            folder.SharedLink = sharedLink;
            Client.Update(folder, updatedFolder =>
            {
                // Assert
                AssertFolderConstraints(updatedFolder, newName, newParent.Id, folder.Id);
                AssertSharedLink(sharedLink, updatedFolder.SharedLink);
                Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
                callbackHit = true;
            }, AbortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            // Cleanup
            Client.Delete(newParent, true);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

    }
}