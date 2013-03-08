using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class SharedItemTests : BoxApiTestHarness
    {
        private const string SharedFileLink = "https://www.box.com/s/amilne3xg32auzk9vqga";
        private const string SharedFolderLink = "https://www.box.com/s/w26z5ev4bnfteaylf8xh";
        private const string SharedFileId = "3954425794";
        private const string SharedFolderId = "482632436";
        private const string SharedSubfileId = "3954498704";

        /* Test tree hierarchy in the Box account of 'box csharp sdk' (box.csharp.sdk@gmail.com)
            ---------------------------
            shared file.txt (3954425794)
            / shared folder (482632436)
                shared subfile.txt (3954498704)
            ---------------------------
         */

        [Test, ExpectedException(typeof(BoxException))]
        public void CanNotGetSharedFileWithIdAlone()
        {
            Client.GetFile(SharedFileId);
        }

        [Test]
        public void GetSharedFile()
        {
            var file = Client.GetSharedItem<File>(SharedFileLink, new[]{FileField.Name, FileField.OwnedBy});
            // File.Parent is 'null' on a shared file so that information about the sharing user's Box is not exposed.
            AssertFileConstraints(file, "shared file.txt", null, SharedFileId);
        }

        [Test]
        public void GetSharedFileWithFields()
        {
            var file = Client.GetSharedItem<File>(SharedFileLink, new[]{FileField.Name});
            Assert.That(file.CreatedAt, Is.Null);
            // File.Parent is 'null' on a shared file so that information about the sharing user's Box is not exposed.
            AssertFileConstraints(file, "shared file.txt", null, SharedFileId);
        }


        [Test, ExpectedException(typeof(BoxItemNotModifiedException))]
        public void GetSharedFileThrowsNotModifiedOnSubsequentRequest()
        {
            var file = Client.GetSharedItem<File>(SharedFileLink);
            Client.GetSharedItem<File>(SharedFileLink, null, file.Etag);
            Assert.Fail();
        }

        [Test]
        public void ReadSharedFile()
        {
            var bytes = Client.Read(SharedFileId, SharedFileLink);
            // Contents are the following 29 bytes:
            // This is a shared file.  Neat!
            Assert.That(bytes.Length, Is.EqualTo(29));
        }

        [Test]
        public void GetSharedFolder()
        {
            var folder = Client.GetSharedItem<Folder>(SharedFolderLink);
            // File.Parent is 'null' on a shared file so that information about the sharing user's Box is not exposed.
            AssertFolderConstraints(folder, "shared folder", null, SharedFolderId);
            // An ItemCollection is not returned with GetSharedItem() -- you have to do a GetFolder() for that.
            Assert.That(folder.ItemCollection, Is.Null);
        }

        [Test]
        public void GetSharedFolderWithField()
        {
            var folder = Client.GetSharedItem<Folder>(SharedFolderLink, new[]{FolderField.Name});
            Assert.That(folder.CreatedAt, Is.Null);
            // File.Parent is 'null' on a shared file so that information about the sharing user's Box is not exposed.
            AssertFolderConstraints(folder, "shared folder", null, SharedFolderId);
            // An ItemCollection is not returned with GetSharedItem() -- you have to do a GetFolder() for that.
            Assert.That(folder.ItemCollection, Is.Null);
        }

        [Test, ExpectedException(typeof(BoxItemNotModifiedException))]
        public void GetSharedFolderThrowsNotModifiedOnSubsequentRequest()
        {
            var folder = Client.GetSharedItem<Folder>(SharedFolderLink);
            Client.GetSharedItem<Folder>(SharedFolderLink, null, folder.Etag);
            Assert.Fail();
        }

        [Test]
        public void GetSharedFolderWithItems()
        {
            var folder = Client.GetFolder(SharedFolderId, SharedFolderLink);
            // File.Parent is 'null' on a shared file so that information about the sharing user's Box is not exposed.
            AssertFolderConstraints(folder, "shared folder", null, SharedFolderId);
            // Thus illustrating the caveat in the GetSharedFolder() test...
            Assert.That(folder.Files.SingleOrDefault(), Is.Not.Null);
        }

        [Test]
        public void GetFileWithinSharedFolder()
        {
            var actual = Client.GetFile(SharedSubfileId, SharedFolderLink);
            // File.Parent is set on a file contained within a shared folder.
            AssertFileConstraints(actual, "shared subfile.txt", SharedFolderId, SharedSubfileId);
        }

        [Test]
        public void CopySharedFileToMyBox()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            try
            {
                var copiedFile = Client.CopyFile(SharedFileId, SharedFileLink, folder.Id, null, null);
                AssertFileConstraints(copiedFile, "shared file.txt", folder.Id);
                Assert.That(copiedFile.Id, Is.Not.EqualTo(SharedFileId));
                // Should not be shared just because the original is shared.
                Assert.That(copiedFile.SharedLink, Is.Null);
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }

        [Test]
        public void CopySharedFolderToMyBox()
        {
            var folder = Client.CreateFolder(Folder.Root, TestItemName());
            try
            {
                var copiedFolder = Client.CopyFolder(SharedFolderId, SharedFolderLink, folder.Id, null, null);
                AssertFolderConstraints(copiedFolder, "shared folder", folder.Id);
                Assert.That(copiedFolder.Id, Is.Not.EqualTo(SharedFolderId));
                // Should not be shared just because the original is shared.
                Assert.That(copiedFolder.SharedLink, Is.Null);
                Assert.That(copiedFolder.ItemCollection.TotalCount, Is.EqualTo(1));
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }
    }
}