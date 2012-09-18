using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    [TestFixture]
    public class FileTestsSync : BoxApiTestHarness
    {
        [Test]
        public void CreateAndDeleteFile()
        {
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            AssertFileConstraints(file, testItemName, RootId);
            Client.Delete(file);
        }

        [Test, ExpectedException(typeof(BoxException))]
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

        [Test]
        public void CopyFile()
        {
            string testItemName = TestItemName();
            File originalFile = Client.CreateFile(RootId, testItemName);
            // Act
            string newName = TestItemName();
            File copyFile = Client.Copy(originalFile, RootId, newName);
            // Assert
            AssertFileConstraints(copyFile, newName, RootId);
            Assert.That(copyFile.Id, Is.Not.EqualTo(originalFile.Id));
            // Cleanup
            Client.Delete(originalFile);
            Client.Delete(copyFile);
        }

        [Test, ExpectedException(typeof(BoxException))]
        public void CopyFileFailsWhenParentAndNameAreSame()
        {
            string testItemName = TestItemName();
            File originalFile = Client.CreateFile(RootId, testItemName);
            // Act
            try
            {
                Client.Copy(originalFile, RootId);
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
            var file = Client.CreateFile(RootId, fileName);
            string folderName = TestItemName();
            var folder = Client.CreateFolder(RootId, folderName);
            // Act
            try
            {
                File copyFile = Client.Copy(file, folder);
                // Assert
                AssertFileConstraints(copyFile, file.Name, folder.Id);
                Assert.That(copyFile.Id, Is.Not.EqualTo(file.Id));
            }
            finally
            {
                Client.Delete(file);
                Client.Delete(folder);
            }
        }

        [Test]
        public void ShareLink()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                var expectedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions() {Download = true, Preview = true});
                File linkedFile = Client.ShareLink(file, expectedLink);
                // Assert
                AssertFileConstraints(linkedFile, file.Name, RootId, file.Id);
                AssertSharedLink(linkedFile.SharedLink, expectedLink);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void RenameFile()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            string newName = TestItemName();
            // Act
            try
            {
                File renamedFile = Client.Rename(file, newName);
                // Assert
                AssertFileConstraints(renamedFile, newName, RootId, file.Id);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void RenameFileToSameName()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
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
        public void MoveFile()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            string newParent = TestItemName();
            var folder = Client.CreateFolder(RootId, newParent);
            // Act
            try
            {
                File movedFile = Client.Move(file, folder);
                // Assert
                AssertFileConstraints(movedFile, fileName, folder.Id, file.Id);
            }
            finally
            {
                Client.Delete(folder);
            }
        }

        [Test]
        public void MoveFileToSameParent()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
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
        public void UpdateDescription()
        {
            string fileName = TestItemName();
            string newDescription = "new description";
            var file = Client.CreateFile(RootId, fileName);
            // Act
            try
            {
                File updatedFile = Client.UpdateDescription(file, newDescription);
                // Assert
                AssertFileConstraints(updatedFile, fileName, RootId, file.Id);
                Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void UpdateEverything()
        {
            string fileName = TestItemName();
            var file = Client.CreateFile(RootId, fileName);
            string newDescription = "new description";
            string newFolder = TestItemName();
            var folder = Client.CreateFolder(RootId, newFolder);
            var sharedLink = new SharedLink(Access.Open, DateTime.UtcNow.AddDays(3), new Permissions() { Download = true, Preview = true });
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

    }
}
