using System;
using System.Linq;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class FolderTestsSync : BoxApiTestHarness
    {
        [Test]
        public void GetFolder()
        {
            var folder = Client.GetFolder(RootId);
            AssertFolderConstraints(folder, "All Files", null, RootId);
        }

        [Test]
        public void GetFolderItems()
        {
            var testFolder = Client.CreateFolder(RootId, TestItemName());
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName());
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName());
            try
            {
                var items = Client.GetItems(testFolder);
                Assert.That(items, Is.Not.Null);
                Assert.That(items.TotalCount, Is.EqualTo("2"));
                Assert.That(items.Entries.SingleOrDefault(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);
                Assert.That(items.Entries.SingleOrDefault(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
            }
            finally
            {
                Client.Delete(testFolder, true);
            }
        }

        [Test]
        public void GetFolderItemsWithParticularFields()
        {
            var testFolder = Client.CreateFolder(RootId, TestItemName());
            Client.CreateFolder(testFolder.Id, TestItemName());
            Client.CreateFolder(testFolder.Id, TestItemName());

            try
            {
                var items = Client.GetItems(testFolder, new[] {Field.CreatedAt, Field.Name, });
                Assert.That(items, Is.Not.Null);
                Assert.That(items.TotalCount, Is.EqualTo("2"));
                // expected present
                Assert.That(items.Entries.All(e => e.Id != null));
                Assert.That(items.Entries.All(e => e.Type != null));
                Assert.That(items.Entries.All(e => e.Name != null));
                Assert.That(items.Entries.All(e => e.CreatedAt != null));
                // expected empty
                Assert.That(items.Entries.All(e => e.CreatedBy == null));
                Assert.That(items.Entries.All(e => e.OwnedBy == null));
            }
            finally
            {
                Client.Delete(testFolder, true);
            }
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void GetNonExistentFolder()
        {
            Client.GetFolder("abc");
        }

        [Test]
        public void CreateFolder()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            AssertFolderConstraints(folder, folderName, RootId);
            // clean up 
            Client.Delete(folder, true);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CreateFolderWithIllegalName()
        {
            Client.CreateFolder(RootId, "\\bad name:");
        }

        [Test]
        public void DeleteFolder()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            Client.Delete(folder, true);
        }

        [Test]
        public void DeleteFolderRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var subFolder = Client.CreateFolder(folder.Id, "subfolder");
            Client.Delete(folder, true);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void DeleteNonEmptyFolderWithoutRecursiveBitSet()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var subFolder = Client.CreateFolder(folder.Id, "subfolder");
            try
            {
                // Should barf.
                Client.Delete(folder, false);
            }
            finally
            {
                // clean up.
                Client.Delete(folder, true);
            }
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void DeleteNonExistentFolder()
        {
            Client.DeleteFolder("1234123", true);
        }

        [Test]
        public void CopyFolderToSameParentWithDifferentName()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var copyName = TestItemName();
            var copy = Client.Copy(folder, RootId, copyName);
            AssertFolderConstraints(copy, copyName, RootId);
            Assert.That(copy.Parent.Id, Is.EqualTo(RootId));
            Client.Delete(folder, true);
            Client.Delete(copy, true);
        }

        [Test]
        public void CopyFolderToDifferentParentWithSameName()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var destinationName = TestItemName();
            var destination = Client.CreateFolder(RootId, destinationName);

            var copy = Client.Copy(folder, destination);
            AssertFolderConstraints(copy, folderName, destination.Id);
            Assert.That(copy.Parent.Id, Is.EqualTo(destination.Id));
            Client.Delete(folder, true);
            Client.Delete(destination, true);
        }

        [Test]
        public void CopyRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder");
            var copyName = TestItemName();
            var copy = Client.Copy(folder, RootId, copyName);
            Assert.That(copy.ItemCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameNotProvided()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            try
            {
                Client.Copy(folder, RootId);
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameIsSame()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            try
            {
                Client.Copy(folder, RootId, folder.Name);
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }

        [Test]
        public void CreateSharedLink()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {Preview = true, Download = true});
            var update = Client.ShareLink(folder, sharedLink);
            AssertFolderConstraints(update, folderName, RootId, folder.Id);
            AssertSharedLink(update.SharedLink, sharedLink);
            Client.Delete(update, true);
        }

        [Test]
        public void MoveFolder()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var targetFolderName = TestItemName();
            var targetFolder = Client.CreateFolder(RootId, targetFolderName);
            var moved = Client.Move(folder, targetFolder);
            AssertFolderConstraints(moved, folderName, targetFolder.Id, folder.Id);
            Client.Delete(targetFolder, true);
        }

        [Test, Description("This fails, but eventually won't.  See http://stackoverflow.com/questions/12439723/moving-folder-to-same-parent-returns-400-bad-request")]
        public void MoveFolderToSameParent()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            try
            {
                var moved = Client.Move(folder, RootId);
                AssertFolderConstraints(moved, folderName, RootId, folder.Id);
            }
            finally 
            {
                Client.DeleteFolder(folder.Id, true);
            }
        }

        [Test]
        public void RenameFolder()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var newName = TestItemName();
            var moved = Client.Rename(folder, newName);
            AssertFolderConstraints(moved, newName, RootId, folder.Id);
            Client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void RenameFolderToSameName()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var moved = Client.Rename(folder, folderName);
            AssertFolderConstraints(moved, folderName, RootId, folder.Id);
            Client.Delete(folder, true);
        }

        [Test]
        public void UpdateDescription()
        {
            var folderName = TestItemName();
            var newDescription = "new description";
            var folder = Client.CreateFolder(RootId, folderName);
            // Act
            try
            {
                var updatedFolder = Client.UpdateDescription(folder, newDescription);
                // Assert
                AssertFolderConstraints(updatedFolder, folderName, RootId, folder.Id);
                Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void UpdateEverything()
        {
            var folder = Client.CreateFolder(RootId, TestItemName());
            var newDescription = "new description";
            var newParent = Client.CreateFolder(RootId, TestItemName());
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {Download = true, Preview = true});
            var newName = TestItemName();
            // Act
            try
            {
                folder.Parent.Id = newParent.Id;
                folder.Description = newDescription;
                folder.Name = newName;
                folder.SharedLink = sharedLink;
                var updatedFolder = Client.Update(folder);
                // Assert
                AssertFolderConstraints(updatedFolder, newName, newParent.Id, folder.Id);
                AssertSharedLink(sharedLink, folder.SharedLink);
                Assert.That(updatedFolder.Description, Is.EqualTo(newDescription));
            }
            finally
            {
                Client.Delete(newParent);
            }
        }
    }
}