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
            
            Client.ReadFileAsync(file.Id, readBytes =>
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
    }
}