using System;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
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
            Client.DeleteFolder(folder.Id, true);
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
            Client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void DeleteFolderRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var subFolder = Client.CreateFolder(folder.Id, "subfolder");
            Client.DeleteFolder(folder.Id, true);
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
                Client.DeleteFolder(folder.Id, false);
            }
            finally
            {
                // clean up.
                Client.DeleteFolder(folder.Id, true);
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
            var copy = Client.CopyFolder(folder.Id, RootId, copyName);
            AssertFolderConstraints(copy, copyName, RootId);
            Assert.That(copy.Parent.Id, Is.EqualTo(RootId));
            Client.DeleteFolder(folder.Id, true);
            Client.DeleteFolder(copy.Id, true);
        }

        [Test]
        public void CopyFolderToDifferentParentWithSameName()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var destinationName = TestItemName();
            var destination = Client.CreateFolder(RootId, destinationName);

            var copy = Client.CopyFolder(folder.Id, destination.Id);
            AssertFolderConstraints(copy, folderName, destination.Id);
            Assert.That(copy.Parent.Id, Is.EqualTo(destination.Id));
            Client.DeleteFolder(folder.Id, true);
            Client.DeleteFolder(destination.Id, true);
        }

        [Test]
        public void CopyRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder");
            var copyName = TestItemName();
            var copy = Client.CopyFolder(folder.Id, RootId, copyName);
            Assert.That(copy.ItemCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameNotProvided()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            try
            {
                Client.CopyFolder(folder.Id, RootId);
            }
            finally
            {
                Client.DeleteFolder(folder.Id, true);
            }
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameIsSame()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            try
            {
                Client.CopyFolder(folder.Id, RootId, folder.Name);
            }
            finally
            {
                Client.DeleteFolder(folder.Id, true);
            }
        }

        [Test]
        public void CreateSharedLink()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {Preview = true, Download = true});
            Folder update = Client.ShareFolderLink(folder.Id, sharedLink);
            AssertFolderConstraints(update, folderName, RootId, folder.Id);
            AssertSharedLink(update.SharedLink, sharedLink);
            Client.DeleteFolder(update, true);
        }

        [Test]
        public void MoveFolder()
        {
            var folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName);
            var targetFolderName = TestItemName();
            Folder targetFolder = Client.CreateFolder(RootId, targetFolderName);
            Folder moved = Client.MoveFolder(folder.Id, targetFolder.Id);
            AssertFolderConstraints(moved, folderName, targetFolder.Id, folder.Id);
            Client.DeleteFolder(targetFolder.Id, true);
        }

        [Test, Ignore("This fails, but probably shouldn't.  http://stackoverflow.com/questions/12439723/moving-folder-to-same-parent-returns-400-bad-request")]
        public void MoveFolderToSameParent()
        {
            var folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName);
            Folder moved = Client.MoveFolder(folder.Id, RootId);
            AssertFolderConstraints(moved, folderName, RootId, folder.Id);
            Client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void RenameFolder()
        {
            var folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName);
            var newName = TestItemName();
            Folder moved = Client.RenameFolder(folder.Id, newName);
            AssertFolderConstraints(moved, newName, RootId, folder.Id);
            Client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void RenameFolderToSameName()
        {
            var folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName);
            Folder moved = Client.RenameFolder(folder.Id, folderName);
            AssertFolderConstraints(moved, folderName, RootId, folder.Id);
            Client.DeleteFolder(folder.Id, true);
        }

    }
}