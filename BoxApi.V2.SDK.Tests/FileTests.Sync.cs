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
            byte[] actual = Client.Read(file.Id);
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
            byte[] actual = Client.Read(file.Id);
            Assert.That(actual, Is.EqualTo(expected));
            // Cleanup
            string newEtag = Client.GetFile(file.Id).Etag;
            Client.DeleteFile(file.Id, newEtag);
        }
    }
}
