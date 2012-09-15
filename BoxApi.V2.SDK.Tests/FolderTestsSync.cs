using System;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FolderTestsSync : BoxApiTestHarness
    {
        
        [Test]
        public void GetFolder()
        {
            var folder = Client.GetFolder("0");
            AssertFolderConstraints(folder, "All Files", "0");
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
            var folder = Client.CreateFolder("0", folderName);
            AssertFolderConstraints(folder, folderName);
            // clean up 
            Client.DeleteFolder(folder.Id, true);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CreateFolderWithIllegalName()
        {
            Client.CreateFolder("0", "\\bad name:");
        }

        [Test]
        public void DeleteFolder()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            Client.DeleteFolder(folder.Id, true);
        }

        [Test]
        public void DeleteFolderRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            var subFolder = Client.CreateFolder(folder.Id, "subfolder");
            Client.DeleteFolder(folder.Id, true);
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void DeleteNonEmptyFolderWithoutRecursiveBitSet()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
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
            var folder = Client.CreateFolder("0", folderName);
            var copyName = TestItemName();
            var copy = Client.CopyFolder(folder.Id, "0", copyName);
            AssertFolderConstraints(copy, copyName);
            Assert.That(copy.Parent.Id, Is.EqualTo("0"));
            Client.DeleteFolder(folder.Id, true);
            Client.DeleteFolder(copy.Id, true);
        }

        [Test]
        public void CopyFolderToDifferentParentWithSameName()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            var destinationName = TestItemName();
            var destination = Client.CreateFolder("0", destinationName);
            
            var copy = Client.CopyFolder(folder.Id, destination.Id);
            AssertFolderConstraints(copy, folderName);
            Assert.That(copy.Parent.Id, Is.EqualTo(destination.Id));
            Client.DeleteFolder(folder.Id, true);
            Client.DeleteFolder(destination.Id, true);
        }

        [Test]
        public void CopyRecursive()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            var subfolder = Client.CreateFolder(folder.Id, "subfolder");
            var copyName = TestItemName();
            var copy = Client.CopyFolder(folder.Id, "0", copyName);
            Assert.That(copy.ItemCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameNotProvided()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            try
            {
                Client.CopyFolder(folder.Id, "0");
            }
            finally
            {
                Client.DeleteFolder(folder.Id, true);
            }
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void CopyFolderFailsWhenSameParentAndNewNameIsSame()
        {
            var folderName = TestItemName();
            var folder = Client.CreateFolder("0", folderName);
            try
            {
                Client.CopyFolder(folder.Id, "0", folder.Name);
            }
            finally
            {
                Client.DeleteFolder(folder.Id, true);
            }
        }

        //Todo: Verify that DeleteFolder fails if 'recursive' is false and the folder has content.
    }
}