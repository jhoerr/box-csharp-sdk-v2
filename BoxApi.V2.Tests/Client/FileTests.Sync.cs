using System;
using System.Linq;
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
        public void CopyFile()
        {
            string testItemName = TestItemName();
            File originalFile = Client.CreateFile(RootId, testItemName);
            // Act
            string newName = TestItemName();
            File copyFile = Client.Copy(originalFile, RootId, newName, null);
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
            string testItemName = TestItemName();
            File originalFile = Client.CreateFile(RootId, testItemName);
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
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            string folderName = TestItemName();
            Folder folder = Client.CreateFolder(RootId, folderName, null);
            // Act
            try
            {
                File copyFile = Client.Copy(file, folder, null, null);
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
        public void CreateAndDeleteFile()
        {
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            AssertFileConstraints(file, testItemName, RootId);
            Client.Delete(file);
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CreateFileWithSameNameInSameParentFails()
        {
            string testItemName = TestItemName();
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
        public void MoveFile()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            string newParent = TestItemName();
            Folder folder = Client.CreateFolder(RootId, newParent, null);
            // Act
            try
            {
                File movedFile = Client.Move(file, folder);
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
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                File movedFile = Client.Move(file, RootId);
                // Assert
                AssertFileConstraints(movedFile, fileName, RootId, file.Id);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void PreviousVersions()
        {
            // Arrange
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName, new byte[] {1}); // 1st revision: 1 byte
            // Act
            file = Client.Write(file, new byte[] {1, 2}); // 2nd revision: 2 bytes
            file = Client.Write(file, new byte[] {1, 2, 3}); // 3rd revision (current): 3 bytes
            // Assert
            VersionCollection actual = Client.GetVersions(file);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
            Assert.That(actual.Entries.Select(e => e.Size), Is.EquivalentTo(new[] {1, 2}));
            // Cleanup
            Client.Delete(file);
        }

        [Test]
        public void ReadFile()
        {
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName, expected);
            byte[] actual = Client.Read(file);
            Assert.That(actual, Is.EqualTo(expected));
            Client.Delete(file);
        }

        [Test]
        public void RenameFile()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            string newName = TestItemName();
            // Act
            File renamedFile = Client.Rename(file, newName);
            Client.Delete(renamedFile);
            // Assert
            AssertFileConstraints(renamedFile, newName, RootId, file.Id);
        }

        [Test]
        public void RenameFileToSameName()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                File renamedFile = Client.Rename(file, file.Name);
                // Assert
                AssertFileConstraints(renamedFile, file.Name, RootId, file.Id);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void ShareLink()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            // Act
            var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            File linkedFile = Client.ShareLink(file, expectedLink);
            Client.Delete(linkedFile);
            // Assert
            AssertFileConstraints(linkedFile, file.Name, RootId, file.Id);
            AssertSharedLink(linkedFile.SharedLink, expectedLink);
        }

        [Test]
        public void UpdateDescription()
        {
            string fileName = TestItemName();
            string newDescription = "new description";
            File file = Client.CreateFile(RootId, fileName);
            // Act
            File updatedFile = Client.UpdateDescription(file, newDescription);
            Client.Delete(updatedFile);
            // Assert
            AssertFileConstraints(updatedFile, fileName, RootId, file.Id);
            Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void UpdateEverything()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            string newDescription = "new description";
            string newFolder = TestItemName();
            Folder folder = Client.CreateFolder(RootId, newFolder, null);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions {CanDownload = true, CanPreview = true});
            string newName = TestItemName();
            // Act
            try
            {
                file.Parent.Id = folder.Id;
                file.Description = newDescription;
                file.Name = newName;
                file.SharedLink = sharedLink;
                File updatedFile = Client.Update(file);
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

        [Test]
        public void WriteFile()
        {
            // Arrange
            var expected = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            // Act
            Client.Write(file, expected);
            // Assert
            byte[] actual = Client.Read(file);
            Assert.That(actual, Is.EqualTo(expected));
            // Cleanup
            file = Client.GetFile(file.Id);
            Client.Delete(file);
        }
    }
}