using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class CollaborationTests : BoxApiTestHarness
    {
        [TestCase(CollaborationRole.CoOwner)]
        [TestCase(CollaborationRole.Editor)]
        [TestCase(CollaborationRole.Previewer)]
        [TestCase(CollaborationRole.PreviewerUploader)]
        [TestCase(CollaborationRole.Uploader)]
        [TestCase(CollaborationRole.Viewer)]
        [TestCase(CollaborationRole.ViewerUploader)]
        public void CreateById(CollaborationRole role)
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            try
            {
                var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, role);
                Assert.That(collaboration, Is.Not.Null);
                Assert.That(collaboration.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(collaboration.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(collaboration.AccessibleBy.Login, Is.EqualTo(CollaboratingUserEmail));
                Assert.That(collaboration.RoleValue, Is.EqualTo(role));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [TestCase(CollaborationRole.CoOwner)]
        [TestCase(CollaborationRole.Editor)]
        [TestCase(CollaborationRole.Previewer)]
        [TestCase(CollaborationRole.PreviewerUploader)]
        [TestCase(CollaborationRole.Uploader)]
        [TestCase(CollaborationRole.Viewer)]
        [TestCase(CollaborationRole.ViewerUploader)]
        public void CreateByEmail(CollaborationRole role)
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            try
            {
                var collaboration = Client.CreateCollaborationByEmail(folder, CollaboratingUserEmail, role);
                Assert.That(collaboration, Is.Not.Null);
                Assert.That(collaboration.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(collaboration.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(collaboration.AccessibleBy.Login, Is.EqualTo(CollaboratingUserEmail));
                Assert.That(collaboration.RoleValue, Is.EqualTo(role));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void Get()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, CollaborationRole.Viewer);
            try
            {
                var actual = Client.Get(collaboration);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(actual.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(actual.RoleValue, Is.EqualTo(CollaborationRole.Viewer));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void GetFolderCollaborations()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var view = Client.CreateCollaboration(folder, CollaboratingUser, CollaborationRole.Viewer);

            try
            {
                var actual = Client.GetCollaborations(folder);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.TotalCount, Is.EqualTo(1));
                Assert.That(actual.Entries.Any(c => c.Id.Equals(view.Id)), Is.True);
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void Update()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, CollaborationRole.Viewer);

            try
            {
                var updated = Client.Update(collaboration, CollaborationRole.Editor);
                Assert.That(updated, Is.Not.Null);
                Assert.That(updated.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(updated.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(updated.RoleValue, Is.EqualTo(CollaborationRole.Editor));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void Delete()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, CollaborationRole.Viewer);

            try
            {
                Client.Delete(collaboration);
                var collaborations = Client.GetCollaborations(folder);
                Assert.That(collaborations.TotalCount, Is.EqualTo(0));
            }
            finally
            {
                Client.Delete(folder);
            }
        }
    }
}