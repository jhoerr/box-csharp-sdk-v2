using System.Threading;
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
            var folder = boxManager.GetFolder(0);
            Assert.That(folder, Is.Not.Null);
        }

        [Test]
        public void GetFolderAsync()
        {
            var callbackHit = false;
            var condition = false;

            var boxManager = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
            boxManager.GetFolderAsync(0, folder =>
                {
                    callbackHit = true;
                    condition = folder != null;
                });

            do
            {
                Thread.Sleep(1000);
            } while (!callbackHit && --MaxWaitInSeconds > 0);

            Assert.True(condition);
        }
    }
}