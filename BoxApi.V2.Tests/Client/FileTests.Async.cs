using System;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class FileTestsAsync : BoxApiTestHarness
    {
        [Test]
        public void CreateFileAsync()
        {
            File actual = null;
            var testItemName = TestItemName();
            Client.CreateFile(file =>
                {
                    actual = file;
                }, AbortOnFailure, RootId, testItemName, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual);
            AssertFileConstraints(actual, testItemName, RootId);
        }

        [Test]
        public void ReadFileAsync()
        {

            byte[] actual = null;
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName, expected);

            Client.Read(readBytes =>
                {
                    actual = readBytes;
                }, AbortOnFailure, file);

          AssertActionComplete(ref actual);
          Client.Delete(file);
          Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void WriteFileAsync()
        {
            // Arrange
            byte[] actual = null;
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
     
            // Act
            Client.Write(updatedFile =>
                {
                    actual = Client.Read(file.Id);
                    Assert.That(actual, Is.EqualTo(expected));
                }, AbortOnFailure, file, expected);

            AssertActionComplete(ref actual);
            Client.Delete(file);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CopyFileAsync()
        {
            // Arrange
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            File actual = null;
            var newItemName = TestItemName();
            // Act
            Client.Copy(copiedFile =>
                {
                    actual = copiedFile;
                }, AbortOnFailure, file, RootId, newItemName, null);

            AssertActionComplete(ref actual);

            Client.Delete(file);
            Client.Delete(actual);
            AssertFileConstraints(actual, newItemName, RootId);
            Assert.That(actual.Id, Is.Not.EqualTo(file.Id));
        }

        [Test]
        public void ShareFileLinkAsync()
        {
            // Arrange
            File actual = null;
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});

            // Act
            Client.ShareLink(sharedFile =>
                {
                    actual = sharedFile;
                }, AbortOnFailure, file, expectedLink, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual);
            AssertFileConstraints(actual, file.Name, RootId, file.Id);
            AssertSharedLink(actual.SharedLink, expectedLink);
        }

        [Test]
        public void RenameFileAsync()
        {
            // Arrange
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            var newItemName = TestItemName();
            File actual = null;
            // Act
            Client.Rename(renamedFile =>
                {
                    actual = renamedFile;
                }, AbortOnFailure, file, newItemName, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual);
            AssertFileConstraints(actual, newItemName, RootId);
        }

        [Test]
        public void MoveFileAsync()
        {
            // Arrange
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            File actual = null;
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            // Act
            Client.Move(movedFile =>
                {
                    actual = movedFile;
                }, AbortOnFailure, file, folder, null);

            AssertActionComplete(ref actual);
            Client.Delete(folder, recursive: true);
            AssertFileConstraints(actual, testItemName, folder.Id, file.Id);
        }

        [Test]
        public void UpdateDescriptionAsync()
        {
            // Arrange
            File actual = null;
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            const string newDescription = "new description";

            // Act
            Client.UpdateDescription(updatedFile =>
                {
                    actual = updatedFile;
                }, AbortOnFailure, file, newDescription, null);

            AssertActionComplete(ref actual);
            Client.Delete(actual);
            AssertFileConstraints(actual, file.Name, RootId, file.Id);
            Assert.That(actual.Description, Is.EqualTo(newDescription));
        }


        [Test]
        public void UpdateAsync()
        {
            // Arrange
            File actual = null;
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            const string newDescription = "new description";
            var newFolder = TestItemName();
            var folder = Client.CreateFolder(RootId, newFolder, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            var newName = TestItemName();

            // Act
            file.Name = newName;
            file.Description = newDescription;
            file.Parent.Id = folder.Id;
            file.SharedLink = sharedLink;
            Client.Update(updatedFile =>
                {
                    actual = updatedFile;
                    
                }, AbortOnFailure, file, null);

            AssertActionComplete(ref actual);
            Client.Delete(folder, recursive:true);
            AssertFileConstraints(actual, newName, folder.Id, file.Id);
            AssertSharedLink(sharedLink, actual.SharedLink);
            Assert.That(actual.Description, Is.EqualTo(newDescription));
        }

        private void AssertActionComplete(ref File actual)
        {
            do
            {
                Thread.Sleep(250);
            } while (actual == null && --MaxQuarterSecondIterations > 0);

            if (actual == null)
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }

        private void AssertActionComplete(ref byte[] actual)
        {
            do
            {
                Thread.Sleep(250);
            } while (actual == null && --MaxQuarterSecondIterations > 0);

            if (actual == null)
            {
                Assert.Fail("Async operation did not complete in alloted time.");
            }
        }
    }
}