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
    }
}
