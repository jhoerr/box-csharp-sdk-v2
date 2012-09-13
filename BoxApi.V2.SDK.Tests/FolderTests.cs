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

        [Test]
        public void GetNonExistentFolder()
        {
            var folder = _client.GetFolder("abc");
            Assert.That(folder, Is.Null);
        }

        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            _client.GetFolderAsync("0", folder =>
                {
                    callbackHit = true;
                    AssertGetFolderConstraints(folder);
                });

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

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
                });

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

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
            AssertCreateFolderConstraints(folder, folderName);
            _client.DeleteFolder(folder.Id, true);
            folder = _client.GetFolder(folder.Id);
            Assert.That(folder, Is.Null);
        }

        private static string TestItemName()
        {
            return string.Format("test_{0}", DateTime.UtcNow.Ticks.ToString());
        }

        //Todo: Verify that DeleteFolder fails if 'recursive' is false and the folder has content.
    }
}