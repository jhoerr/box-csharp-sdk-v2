using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class SingleUserTests : BoxApiTestHarness
    {
        [TestCase("john hoerr")]
        // Replace with your username
        public void Me(string username)
        {
            User user = Client.Me();
            Assert.That(user.Name, Is.EqualTo(username));
        }

        [Test]
        public void SpaceUsedIsUpdated()
        {
            User user = Client.Me(new[] { Field.SpaceUsed, });
            long initialSpaceUsed = user.SpaceUsed;
            File file = Client.CreateFile(Folder.Root, TestItemName(), new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4 });
            user = Client.Me(new[] { Field.SpaceUsed, });
            long spaceUsed = user.SpaceUsed - initialSpaceUsed;
            Assert.That(spaceUsed, Is.EqualTo(file.Size));
            Client.Delete(file);
        }
    }
}