using System;
using System.Threading;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FolderTests : BoxApiTestHarness
    {
        private readonly BoxManager _client = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);

        [Test]
        public void GetFolder()
        {
            var folder = _client.GetFolder("0");
            AssertGetFolderConstraints(folder);
        }

        [Test, ExpectedException(typeof(BoxException)) ]
        public void GetNonExistentFolder()
        {
            _client.GetFolder("abc");
        }

        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            _client.GetFolderAsync("0", folder =>
                {
                    callbackHit = true;
                    AssertGetFolderConstraints(folder);
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
            bool failureOccured = false;
            _client.GetFolderAsync("abc", folder => { }, () => { failureOccured = true; });

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        private static void AssertGetFolderConstraints(Folder folder)
        {
            Assert.That(folder, Is.Not.Null);
            Assert.That(folder.Id, Is.EqualTo("0"));
            Assert.That(folder.Type, Is.EqualTo("folder"));
            Assert.That(folder.Name, Is.EqualTo("All Files"));
        }

        [Test]
        public void CreateFolder()
        {
            var folderName = TestItemName();
            var folder = _client.CreateFolder("0", folderName);
            AssertCreateFolderConstraints(folder, folderName);
            // clean up 
            _client.DeleteFolder(folder.Id, true);
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void CreateFolderWithIllegalName()
        {
            _client.CreateFolder("0", "\\bad name:");
        }

        [Test]
        public void CreateFolderAsync()
        {
            var folderName = TestItemName();
            var callbackHit = false;

            _client.CreateFolderAsync("0", folderName, folder =>
                {
                    callbackHit = true;
                    AssertCreateFolderConstraints(folder, folderName);
                    // clean up 
                    _client.DeleteFolder(folder.Id, true);
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

            _client.CreateFolderAsync("0", "\\bad name:", folder => { }, () => failureOccured = true);

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        private static void AssertCreateFolderConstraints(Folder folder, string folderName)
        {
            Assert.That(folder, Is.Not.Null);
            Assert.That(folder.Name, Is.EqualTo(folderName));
            Assert.That(folder.Type, Is.EqualTo("folder"));
        }

        [Test]
        public void DeleteFolder()
        {
            var folderName = TestItemName();
            var folder = _client.CreateFolder("0", folderName);
            _client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void DeleteFolderAsync()
        {
            var callbackHit = false;
            var folderName = TestItemName();
            var folder = _client.CreateFolder("0", folderName);
            _client.DeleteFolderAsync(folder.Id, true, () => callbackHit = true, null);

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
                _client.GetFolder(folder.Id);
                Assert.Fail("Should not be able to fetch the deleted folder.");
            }
            catch (BoxException e)
            {
            }
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void DeleteNonExistentFolder()
        {
            _client.DeleteFolder("1234123", true);
        }

        [Test]
        public void DeleteNonExistentFolderAsync()
        {
            bool failureOccured = false;
            _client.DeleteFolderAsync("abc", true, () => { }, () => { failureOccured = true; });

            do
            {
                Thread.Sleep(1000);
            } while (!failureOccured && --MaxWaitInSeconds > 0);

            if (MaxWaitInSeconds.Equals(0))
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        private static string TestItemName()
        {
            return string.Format("test_{0}", DateTime.UtcNow.Ticks.ToString());
        }

        //Todo: Verify that DeleteFolder fails if 'recursive' is false and the folder has content.
    }
}