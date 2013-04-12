using System;
using System.Linq;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;
using RestSharp;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class FolderTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void GetRootFolderAsync()
        {
            Folder actual = null;

            Client.GetFolder(folder => { actual = folder; }, AbortOnFailure, RootId);

            AssertActionComplete(ref actual);
            AssertFolderConstraints(actual, "All Files", null, RootId);
        }

        [Test]
        public void GetNonExistentFolderAsync()
        {
            object failureToken = null;
            
            Client.GetFolder(folder => { }, (error) => failureToken = new object(), "invalid idS");
            
            AssertActionComplete(ref failureToken);
        }

        [Test]
        public void CreateFolderAsync()
        {
            Folder actual = null;
            var folderName = TestItemName();

            Client.CreateFolder(folder => { actual = folder; }, AbortOnFailure, RootId, folderName, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual, true);
            AssertFolderConstraints(actual, folderName, RootId);
        }

        [Test]
        public void CreateFolderWithIllegalNameAsync()
        {
            object failureToken = null;
            
            Client.CreateFolder(folder => { }, (error) => failureToken = new object(), RootId, "\\bad name:", null);

            AssertActionComplete(ref failureToken);
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void DeleteFolderAsync()
        {
            IRestResponse actual = null;
            var folder = Client.CreateFolder(RootId, TestItemName(), null);

            Client.Delete(response => actual = response, AbortOnFailure, folder, true);

            AssertActionComplete(ref actual);
            Client.Get(folder, null);
            Assert.Fail("Should not be able to fetch the deleted folder.");
        }

        [Test]
        public void DeleteNonExistentFolderAsync()
        {
            object failureToken = null;
            
            Client.DeleteFolder(response => { }, (error) => failureToken = new object(), "abc", true);

            AssertActionComplete(ref failureToken);
        }

        [Test]
        public void CopyFolderAsync()
        {
            Folder actual = null;
            Folder folder = Client.CreateFolder(RootId, TestItemName(), null);
            string copyName = TestItemName();
            
            Client.CopyFolder(copiedFolder => actual = copiedFolder, AbortOnFailure, folder.Id, RootId, copyName, null);

            AssertActionComplete(ref actual);
            Client.Delete(folder, true);
            Client.Delete(actual, true);
            AssertFolderConstraints(actual, copyName, RootId);
            Assert.That(actual.Id, Is.Not.EqualTo(folder.Id));
        }

        [Test]
        public void ShareFolderLinkAsync()
        {
            Folder actual = null;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanPreview = true, CanDownload = true});

            Client.ShareFolderLink(copiedFolder => actual = copiedFolder, AbortOnFailure, folder.Id, sharedLink, null);

            AssertActionComplete(ref actual);
            Client.Delete(folder, true);
            AssertFolderConstraints(actual, folderName, RootId);
            AssertSharedLink(actual.SharedLink, sharedLink);
        }

        [Test]
        public void MoveFolderAsync()
        {
            Folder actual = null;
            string folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName, null);
            Folder targetFolder = Client.CreateFolder(RootId, TestItemName(), null);

            Client.MoveFolder(movedFolder => actual = movedFolder, AbortOnFailure, folder.Id, targetFolder.Id, null);

            AssertActionComplete(ref actual);
            Client.Delete(targetFolder, true);
            AssertFolderConstraints(actual, folderName, targetFolder.Id, folder.Id);
        }

        [Test]
        public void RenameFolderAsync()
        {
            Folder actual = null;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            var newName = TestItemName();

            Client.Rename(renamedFolder => actual = renamedFolder, AbortOnFailure, folder, newName, null);

            AssertActionComplete(ref actual);
            AssertFolderConstraints(actual, newName, RootId, folder.Id);
            Client.Delete(actual, true);
        }

        [Test]
        public void GetFolderItemsAsync()
        {
            ItemCollection actual = null;
            var testFolder = Client.CreateFolder(RootId, TestItemName(), null);
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName(), null);
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName(), null);

            Client.GetItems(contents => actual = contents, AbortOnFailure, testFolder.Id, new[] {FolderField.Name,});

            AssertActionComplete(ref actual);
            Client.Delete(testFolder, true);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
            Assert.That(actual.Entries.SingleOrDefault(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);
            Assert.That(actual.Entries.SingleOrDefault(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
        }

        [Test]
        public void GetFolderItemsAsyncLimitOffset()
        {
            ItemCollection actual = null;
            var testFolder = Client.CreateFolder(RootId, TestItemName(), null);
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName(), null);
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName(), null);

            // Get foirst item
            Client.GetItems(contents => actual = contents, AbortOnFailure, testFolder.Id, new[] { FolderField.Name, }, 1, 0);

            AssertActionComplete(ref actual);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
            Assert.That(actual.Entries.Count(), Is.EqualTo(1));
            Assert.That(actual.Entries.Single(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);

            // Get second item
            Client.GetItems(contents => actual = contents, AbortOnFailure, testFolder.Id, new[] { FolderField.Name, }, 1, 1);

            AssertActionComplete(ref actual);
            Client.Delete(testFolder, true);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
            Assert.That(actual.Entries.Count(), Is.EqualTo(1));
            Assert.That(actual.Entries.Single(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
        }

        [Test]
        public void GetFolderAsyncLimitOffset()
        {
            Folder actual = null;

            var testFolder = Client.CreateFolder(RootId, TestItemName(), null);
            var subfolder1 = Client.CreateFolder(testFolder.Id, TestItemName(), null);
            var subfolder2 = Client.CreateFolder(testFolder.Id, TestItemName(), null);

            Client.GetFolder(contents => actual = contents, AbortOnFailure, testFolder.Id, new[] { FolderField.Name, }, limit: 1, offset: 0);

            AssertActionComplete(ref actual);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ItemCollection.TotalCount, Is.EqualTo(2));
            Assert.That(actual.ItemCollection.Entries.Count(), Is.EqualTo(1));
            Assert.That(actual.ItemCollection.Entries.Single(e => e.Name.Equals(subfolder1.Name)), Is.Not.Null);

            Client.GetFolder(contents => actual = contents, AbortOnFailure, testFolder.Id, new[] { FolderField.Name, }, limit: 1, offset: 1);

            AssertActionComplete(ref actual);
            Client.Delete(testFolder, true);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ItemCollection.TotalCount, Is.EqualTo(2));
            Assert.That(actual.ItemCollection.Entries.Count(), Is.EqualTo(1));
            Assert.That(actual.ItemCollection.Entries.Single(e => e.Name.Equals(subfolder2.Name)), Is.Not.Null);
        }

        [Test]
        public void UpdateDescriptionAsync()
        {
            // Arrange
            Folder actual = null;
            var folder = Client.CreateFolder(RootId, TestItemName(), null);
            const string newDescription = "new description";

            // Act
            folder.Description = newDescription;
            Client.UpdateDescription(updatedFolder => actual = updatedFolder, AbortOnFailure, folder, newDescription, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual, true);
            AssertFolderConstraints(actual, folder.Name, folder.Parent.Id, folder.Id);
            Assert.That(actual.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void UpdateEverythingAsync()
        {
            // Arrange
            Folder actual = null;
            const string newDescription = "new description";
            var folder = Client.CreateFolder(RootId, TestItemName(), null);
            var newParent = Client.CreateFolder(RootId, TestItemName(), null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            var newName = TestItemName();

            // Act
            folder.Name = newName;
            folder.Description = newDescription;
            folder.Parent.Id = newParent.Id;
            folder.SharedLink = sharedLink;
            Client.Update(updatedFolder => actual = updatedFolder, AbortOnFailure, folder, null);

            AssertActionComplete(ref actual);
            Client.Delete(folder, true);
            Client.Delete(newParent, true);
            AssertFolderConstraints(actual, newName, newParent.Id, folder.Id);
            AssertSharedLink(sharedLink, actual.SharedLink);
            Assert.That(actual.Description, Is.EqualTo(newDescription));

        }
    }
}