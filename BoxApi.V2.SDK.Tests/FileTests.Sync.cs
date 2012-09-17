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
    }
}
