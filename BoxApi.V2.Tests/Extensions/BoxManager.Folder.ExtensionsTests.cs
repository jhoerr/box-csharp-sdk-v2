using BoxApi.V2.Extensions;
using BoxApi.V2.Model;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Extensions
{
    [TestFixture]
    internal class BoxManagerFolderExtensionsTests : BoxApiTestHarness
    {
        [Test]
        public void GetTotalItemsCount()
        {
          
            Folder testFolder = Client.CreateFolder("0", TestItemName());
            try
            {
                ConfigureTestTree(testFolder);
                long totalItemsInFolder = Client.TotalItemsInFolder(testFolder.Id);
                Assert.That(totalItemsInFolder, Is.EqualTo(8));
            }
            finally
            {
                Client.Delete(testFolder, true);
            }
        }

        [Test]
        public void GetTotalFilesCount()
        {
            Folder testFolder = Client.CreateFolder("0", TestItemName());
            try
            {
                ConfigureTestTree(testFolder);
                long totalItemsInFolder = Client.TotalFilesInFolder(testFolder.Id);
                Assert.That(totalItemsInFolder, Is.EqualTo(5));
            }
            finally
            {
                Client.Delete(testFolder, true);
            }
        }

        private void ConfigureTestTree(Folder testFolder)
        {
            /*
           * testFolder/
           *   file1
           *   testSubFolder1/
           *     file2
           *   testSubFolder2/
           *     file3
           *     testSubSubFolder/
           *       file4
           *       file5
           */

            Folder testSubFolder1 = Client.CreateFolder(testFolder.Id, TestItemName());
            Folder testSubFolder2 = Client.CreateFolder(testFolder.Id, TestItemName());
            Folder testSubSubFolder = Client.CreateFolder(testSubFolder2.Id, TestItemName());
            Client.CreateFile(testFolder.Id, TestItemName());
            Client.CreateFile(testSubFolder1.Id, TestItemName());
            Client.CreateFile(testSubFolder2.Id, TestItemName());
            Client.CreateFile(testSubSubFolder.Id, TestItemName());
            Client.CreateFile(testSubSubFolder.Id, TestItemName());
        }
    }
}