using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class DiscussionTestsSync : BoxApiTestHarness
    {
        [Test, ExpectedException(typeof(BoxException))]
        public void CantCreateDisussionOnRootFolder()
        {
            Client.CreateDiscussion(Folder.Root, "foo");
        }

        [Test]
        public void CreateDiscussion()
        {
            const string name = "name";
            const string description = "description";
            Folder folder = null;
            Discussion discussion = null;
            try
            {
                folder = Client.CreateFolder(Folder.Root, TestItemName());
                discussion = Client.CreateDiscussion(folder.Id, name, description);
                Assert.That(discussion, Is.Not.Null);
                Assert.That(discussion.Parent.Id, Is.EqualTo(folder.Id));
                Assert.That(discussion.Name, Is.EqualTo(name));
                Assert.That(discussion.Description, Is.EqualTo(description));
            }
            finally
            {
                Client.Delete(folder);
                if (discussion != null)
                {
                    Client.Delete(discussion);
                }
            }
        }

        [Test]
        public void GetDiscussion()
        {
            const string name = "name";
            const string description = "description";
            Folder folder = null;
            Discussion discussion = null;
            try
            {
                folder = Client.CreateFolder(Folder.Root, TestItemName());
                discussion = Client.CreateDiscussion(folder.Id, name, description);
                var actual = Client.GetDiscussion(discussion.Id);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Parent.Id, Is.EqualTo(folder.Id));
                Assert.That(actual.Name, Is.EqualTo(name));
                Assert.That(actual.Description, Is.EqualTo(description));
            }
            finally
            {
                Client.Delete(folder);
                if (discussion != null)
                {
                    Client.Delete(discussion);
                }
            }
        }

        [Test, Description("Should result in a 404 on the Discussion"), ExpectedException(typeof(BoxException))]
        public void DeleteDiscussion()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            Discussion discussion = Client.CreateDiscussion(folder.Id, "foo");
            Client.Delete(discussion);

            try
            {
                var deleted = Client.GetDiscussion(discussion.Id);
                Assert.That(deleted, Is.Null);
            }
            finally
            {
                Client.Delete(folder);
            }
        }
    }
}