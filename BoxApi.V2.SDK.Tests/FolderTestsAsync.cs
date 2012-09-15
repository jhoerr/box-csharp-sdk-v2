using System;
using System.Threading;
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

            Client.GetFolderAsync("0", folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, "All Files", "0");
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

            Client.CreateFolderAsync("0", folderName, folder =>
                {
                    callbackHit = true;
                    AssertFolderConstraints(folder, folderName);
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

            Client.CreateFolderAsync("0", "\\bad name:", folder => { }, () => failureOccured = true);

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
            var folder = Client.CreateFolder("0", folderName);
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
            var folder = Client.CreateFolder("0", folderName);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder");
            var copyName = TestItemName();
            Client.CopyFolderAsync(folder.Id, "0", copiedFolder =>
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
    }
}