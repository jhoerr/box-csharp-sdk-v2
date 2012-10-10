using System;
using System.Linq;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class CollaborationTests : BoxApiTestHarness
    {
        [Test]
        public void Create()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            try
            {
                var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, Role.Viewer);
                Assert.That(collaboration, Is.Not.Null);
                Assert.That(collaboration.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(collaboration.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(collaboration.Role, Is.EqualTo(Role.Viewer.Description()));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void Get()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, Role.Viewer);
            try
            {
                Collaboration actual = Client.Get(collaboration);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(actual.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(actual.Role, Is.EqualTo(Role.Viewer.Description()));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void GetFolderCollaborations()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            var view = Client.CreateCollaboration(folder, CollaboratingUser, Role.Viewer);
            
            try
            {
                CollaborationCollection actual = Client.GetCollaborations(folder);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.TotalCount, Is.EqualTo("1"));
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
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, Role.Viewer);
            
            try
            {
                Collaboration updated = Client.Update(collaboration, Role.Editor);
                Assert.That(updated, Is.Not.Null);
                Assert.That(updated.Item.Id, Is.EqualTo(folder.Id));
                Assert.That(updated.AccessibleBy.Id, Is.EqualTo(CollaboratingUser));
                Assert.That(updated.Role, Is.EqualTo(Role.Editor.Description()));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void Delete()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            var collaboration = Client.CreateCollaboration(folder, CollaboratingUser, Role.Viewer);

            try
            {
                Client.Delete(collaboration);
                var collaborations = Client.GetCollaborations(folder);
                Assert.That(collaborations.TotalCount, Is.EqualTo("0"));
            }
            finally
            {
                Client.Delete(folder);
            }
        }
    }
}