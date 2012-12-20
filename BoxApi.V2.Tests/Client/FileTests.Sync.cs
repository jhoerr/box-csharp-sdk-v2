using System;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class FileTestsSync : BoxApiTestHarness
    {
        [Test]
        public void CreateAndDeleteFile()
        {
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            AssertFileConstraints(file, testItemName, RootId);
            Client.Delete(file);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CreateFileWithSameNameInSameParentFails()
        {
            var testItemName = TestItemName();
            File file = null;
            try
            {
                file = Client.CreateFile(RootId, testItemName);
                Client.CreateFile(RootId, testItemName);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void ReadFile()
        {
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName, expected);
            var actual = Client.Read(file);
            Assert.That(actual, Is.EqualTo(expected));
            Client.Delete(file);
        }

        [Test]
        public void WriteFile()
        {
            // Arrange
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var testItemName = TestItemName();
            var file = Client.CreateFile(RootId, testItemName);
            // Act
            Client.Write(file, expected);
            // Assert
            var actual = Client.Read(file);
            Assert.That(actual, Is.EqualTo(expected));
            // Cleanup
            file = Client.GetFile(file.Id);
            Client.Delete(file);
        }

        [Test]
        public void CopyFile()
        {
            var testItemName = TestItemName();
            var originalFile = Client.CreateFile(RootId, testItemName);
            // Act
            var newName = TestItemName();
            var copyFile = Client.Copy(originalFile, RootId, newName, null);
            // Assert
            AssertFileConstraints(copyFile, newName, RootId);
            Assert.That(copyFile.Id, Is.Not.EqualTo(originalFile.Id));
            // Cleanup
            Client.Delete(originalFile);
            Client.Delete(copyFile);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CopyFileFailsWhenParentAndNameAreSame()
        {
            var testItemName = TestItemName();
            var originalFile = Client.CreateFile(RootId, testItemName);
            // Act
            try
            {
                Client.Copy(originalFile, RootId, null, null);
            }
            finally
            {
                Client.Delete(originalFile);
            }
        }

        [Test]
        public void CopyFileToDifferentFolder()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName, null);
            // Act
            try
            {
                var copyFile = Client.Copy(file, folder, null, null);
                // Assert
                AssertFileConstraints(copyFile, file.Name, folder.Id);
                Assert.That(copyFile.Id, Is.Not.EqualTo(file.Id));
            }
            finally
            {
                Client.Delete(file);
                Client.Delete(folder, true);
            }
        }

        [Test]
        public void ShareLink()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            // Act
            var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions { CanDownload = true, CanPreview = true });
            var linkedFile = Client.ShareLink(file, expectedLink);
            Client.Delete(linkedFile);
            // Assert
            AssertFileConstraints(linkedFile, file.Name, RootId, file.Id);
            AssertSharedLink(linkedFile.SharedLink, expectedLink);
        }

        [Test]
        public void RenameFile()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var newName = TestItemName();
            // Act
            var renamedFile = Client.Rename(file, newName);
            Client.Delete(renamedFile);
            // Assert
            AssertFileConstraints(renamedFile, newName, RootId, file.Id);
        }

        [Test]
        public void RenameFileToSameName()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                var renamedFile = Client.Rename(file, file.Name);
                // Assert
                AssertFileConstraints(renamedFile, file.Name, RootId, file.Id);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void MoveFile()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var newParent = TestItemName();
            var folder = Client.CreateFolder(RootId, newParent, null);
            // Act
            try
            {
                var movedFile = Client.Move(file, folder);
                // Assert
                AssertFileConstraints(movedFile, fileName, folder.Id, file.Id);
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }

        [Test]
        public void MoveFileToSameParent()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                var movedFile = Client.Move(file, RootId);
                // Assert
                AssertFileConstraints(movedFile, fileName, RootId, file.Id);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void UpdateDescription()
        {
            var fileName = TestItemName();
            var newDescription = "new description";
            var file = Client.CreateFile(RootId, fileName);
            // Act
            var updatedFile = Client.UpdateDescription(file, newDescription);
            Client.Delete(updatedFile);
            // Assert
            AssertFileConstraints(updatedFile, fileName, RootId, file.Id);
            Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void UpdateEverything()
        {
            var fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            var newDescription = "new description";
            var newFolder = TestItemName();
            var folder = Client.CreateFolder(RootId, newFolder, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            var newName = TestItemName();
            // Act
            try
            {
                file.Parent.Id = folder.Id;
                file.Description = newDescription;
                file.Name = newName;
                file.SharedLink = sharedLink;
                var updatedFile = Client.Update(file);
                // Assert
                AssertFileConstraints(updatedFile, newName, folder.Id, file.Id);
                AssertSharedLink(sharedLink, file.SharedLink);
                Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }
    }
}