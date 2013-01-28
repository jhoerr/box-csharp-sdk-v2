using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class FileTestsSync : BoxApiTestHarness
    {
        private File PostFileStream(string fileName)
        {
            const string resource = "BoxApi.V2.Tests.Resources.corgilobster.jpg";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                return Client.CreateFile(Folder.Root, fileName, stream);
            }
        }

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

        [Test]
        public void CreateFileFromStream()
        {
            File file = null;
            try
            {
                file = PostFileStream("testimage.jpg");
                Assert.That(file.Size, Is.Not.EqualTo(0));
            }
            finally
            {
                Client.Delete(file);
            }
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

        [Test, ExpectedException(typeof (BoxItemModifiedException))]
        public void DeleteFailsIfEtagIsStale()
        {
            // Arrange
            string testItemName = TestItemName();
            File original = Client.CreateFile(RootId, testItemName);
            File current = Client.Write(original, new byte[] {1, 2, 3});
            try
            {
                // Act
                Client.Delete(original, original.Etag);
                Assert.Fail();
            }
            finally
            {
                Client.Delete(current);
            }
        }

        [Test]
        public void EtagsAreValid()
        {
            string testItemName = TestItemName();
            File file = null;
            try
            {
                file = Client.CreateFile(RootId, testItemName);
                string firstEtag = file.Etag;
                Assert.That(firstEtag, Is.Not.Null);
                file = Client.Write(file, new byte[] {1, 2, 3});
                string secondEtag = file.Etag;
                Assert.That(secondEtag, Is.Not.Null);
                Assert.That(secondEtag, Is.Not.EqualTo(firstEtag));
                file = Client.Write(file, new byte[] {1, 2, 3, 4, 5, 6});
                string thirdEtag = file.Etag;
                Assert.That(thirdEtag, Is.Not.Null);
                Assert.That(thirdEtag, Is.Not.EqualTo(firstEtag));
                Assert.That(thirdEtag, Is.Not.EqualTo(secondEtag));
            }
            finally
            {
                Client.DeleteFile(file.Id);
            }
        }

        [Test]
        public void GenericThumbnail()
        {
            File file = null;
            try
            {
                file = PostFileStream(TestItemName());
                byte[] thumbnail = Client.GetThumbnail(file);
                Assert.That(thumbnail.Length, Is.Not.EqualTo(0));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void GetFile()
        {
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            File actual = Client.Get(file);
            AssertFileConstraints(actual, testItemName, RootId);
            Client.Delete(actual);
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
        public void MultiLineDescription()
        {
            string fileName = TestItemName();
            string newDescription = "new\ndescription";
            File file = Client.CreateFile(RootId, fileName);
            // Act
            File updatedFile = Client.UpdateDescription(file, newDescription);
            Client.Delete(updatedFile);
            // Assert
            AssertFileConstraints(updatedFile, fileName, RootId, file.Id);
            Assert.That(updatedFile.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void PathCollection()
        {
            // Arrange
            Folder folder = Client.CreateFolder(RootId, TestItemName());
            Folder subFolder = Client.CreateFolder(folder.Id, TestItemName());
            Folder subsubFolder = Client.CreateFolder(subFolder.Id, TestItemName());
            File file = Client.CreateFile(subsubFolder.Id, TestItemName(), new[] {Field.PathCollection,});

            try
            {
                //Assert
                Assert.That(file.PathCollection.TotalCount, Is.EqualTo(4));
                Assert.That(file.PathCollection.Entries.Select(e => e.Id), Is.EqualTo(new[] {Folder.Root, folder.Id, subFolder.Id, subsubFolder.Id}));
            }
            finally
            {
                //Cleanup
                Client.Delete(folder, true);
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

        [Test, ExpectedException(typeof (BoxItemNotModifiedException))]
        public void SubsequentGetThrowsNotModifiedException()
        {
            string testItemName = TestItemName();
            File file = Client.CreateFile(RootId, testItemName);
            try
            {
                file = Client.Get(file, null, file.Etag);
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void GetThumbnail()
        {
            File file = null;
            try
            {
                file = PostFileStream("testimage.jpg");
                var thumbnail = GetThumbnail(file);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Width, Is.GreaterThanOrEqualTo(32));
                Assert.That(thumbnail.Height, Is.GreaterThanOrEqualTo(32));
            }
            finally
            {
                Client.Delete(file);
            }
        }
        /*
        [TestCase(32)]
        [TestCase(64)]
        [TestCase(128)]
        [TestCase(256)]
        public void ThumbnailMinHeight(int minHeight)
        {
            File file = null;
            try
            {
                file = PostFileStream("testimage.jpg");
                var thumbnail = GetThumbnail(file);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Width, Is.GreaterThanOrEqualTo(32));
                Assert.That(thumbnail.Height, Is.GreaterThanOrEqualTo(32));
            }
            finally
            {
                Client.Delete(file);
            }
        }
        */
        private Image GetThumbnail(File file)
        {
            while(true)
            {
                try
                {
                    var thumbnail = Client.GetThumbnail(file);
                    using (var stream = new MemoryStream(thumbnail))
                    {
                        return Image.FromStream(stream);
                    }
                }
                catch (BoxDownloadNotReadyException e)
                {
                    Console.Out.WriteLine("Waiting {0} seconds before retrying...", e.RetryAfter);
                    Thread.Sleep(e.RetryAfter*1000);
                }
            }
        }

        [Test, ExpectedException(typeof (BoxDownloadNotReadyException))]
        public void ThumbnailNotReady()
        {
            File file = null;
            try
            {
                file = PostFileStream("testimage.jpg");
                byte[] thumbnail = Client.GetThumbnail(file);
                Assert.That(thumbnail.Length, Is.Not.EqualTo(0));
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

        [Test, ExpectedException(typeof (BoxItemModifiedException))]
        public void UpdateFailsIfEtagIsStale()
        {
            string fileName = TestItemName();
            File original = Client.CreateFile(RootId, fileName);
            File current = Client.Write(original, new byte[] {1, 2, 3});

            // Act
            try
            {
                original.Description = "some new description";
                Client.Update(original, null, original.Etag);
                Assert.Fail();
            }
            finally
            {
                Client.Delete(current);
            }
        }

        [Test]
        public void UpdateSucceedsIfEtagIsStaleAndNotSpecified()
        {
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            Client.Write(file, new byte[] {1, 2, 3});

            // Act
            try
            {
                file.Description = "some new description";
                file = Client.Update(file);
                Assert.That(file.Description, Is.EqualTo("some new description"));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test, ExpectedException(typeof (BoxItemModifiedException))]
        public void WriteFailsIfEtagIsStale()
        {
            // Arrange
            string testItemName = TestItemName();
            File original = Client.CreateFile(RootId, testItemName);
            File current = Client.Write(original, new byte[] {1, 2, 3});
            try
            {
                // Act
                Client.Write(original, new byte[] {4, 5, 6}, original.Etag);
                Assert.Fail();
            }
            finally
            {
                Client.Delete(current);
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
            file = Client.Write(file, expected);
            // Assert
            byte[] actual = Client.Read(file);
            Assert.That(actual, Is.EqualTo(expected));
            // Cleanup
            file = Client.GetFile(file.Id);
            Client.Delete(file);
        }
    }
}