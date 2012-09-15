using System;
using System.Linq;
using System.Threading;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FolderTestsAsync : BoxApiTestHarness
    {
        private Action _abortOnFailure = () => Assert.Fail("operation failed");

        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            Client.GetFolderAsync(RootId, folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, "All Files", null, RootId);
                }, null);

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
            Client.GetFolderAsync("abc", folder => { }, () => { failureOccured = true; });

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

            Client.CreateFolderAsync(RootId, folderName, folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, folderName, RootId);
                    // clean up 
                    Client.DeleteFolder(folder.Id, true);
                }, null);

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

            Client.CreateFolderAsync(RootId, "\\bad name:", folder => { }, () => failureOccured = true);

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
            Client.DeleteFolderAsync(folder.Id, true, () => callbackHit = true, null);

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
                Client.GetFolder(folder.Id);
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
            Client.DeleteFolderAsync("abc", true, () => { }, () => { failureOccured = true; });

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
            Client.CopyFolderAsync(folder.Id, RootId, copiedFolder =>
                {
                    callbackHit = true;
                    Assert.That(copiedFolder.ItemCollection.TotalCount, Is.EqualTo("1"));
                    Client.DeleteFolder(folder.Id, true);
                    Client.DeleteFolder(copiedFolder.Id, true);
                }, _abortOnFailure, copyName );

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

            Client.ShareFolderLinkAsync(folder.Id, sharedLink, copiedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(folder, folderName, RootId);
                AssertSharedLink(folder.SharedLink, sharedLink);
                Client.DeleteFolder(folder.Id, true);
            }, _abortOnFailure);

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

            Client.MoveFolderAsync(folder.Id, targetFolder.Id, movedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(movedFolder, folderName, targetFolder.Id, folder.Id);
                Client.DeleteFolder(targetFolder.Id, true);
            }, _abortOnFailure);

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

            Client.RenameFolderAsync(folder.Id, newName, renamedFolder =>
            {
                callbackHit = true;
                AssertFolderConstraints(renamedFolder, newName, RootId, folder.Id);
                Client.DeleteFolder(renamedFolder.Id, true);
            }, _abortOnFailure);

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
       
            Client.GetFolderItemsAsync(testFolder.Id, contents =>
            {
                callbackHit = true;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents.TotalCount, Is.EqualTo("2"));
                Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);
                Assert.That(contents.Entries.SingleOrDefault(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
                Client.DeleteFolder(testFolder.Id, true);
            }, _abortOnFailure);

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }


    }
}