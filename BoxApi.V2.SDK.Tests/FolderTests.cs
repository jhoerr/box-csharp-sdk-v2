using System;
using System.Threading;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FolderTests : BoxApiTestHarness
    {
        [Test]
        public void GetFolder()
        {
            var boxManager = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
            var folder = boxManager.GetFolder("0");
            AssertGetFolderConstraints(folder);
        }

        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;

            var boxManager = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
            boxManager.GetFolderAsync("0", folder =>
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
            string folderName = string.Format("test_{0}", DateTime.UtcNow.Ticks.ToString());
            var boxManager = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
            var folder = boxManager.CreateFolder("0", folderName);
            AssertCreateFolderConstraints(folder, folderName);
        }

        [Test]
        public void CreateFolderAsync()
        {
            string folderName = string.Format("test_{0}", DateTime.UtcNow.Ticks.ToString());
            var callbackHit = false;

            var boxManager = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
            boxManager.CreateFolderAsync("0", folderName, folder =>
                {
                    callbackHit = true;
                    AssertCreateFolderConstraints(folder, folderName);
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
    }
}