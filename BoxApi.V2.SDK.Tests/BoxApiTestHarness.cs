using System;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    public class BoxApiTestHarness
    {
        protected readonly BoxManager Client = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);

        protected int MaxWaitInSeconds { get; set; }

        [SetUp]
        public void Setup()
        {
            MaxWaitInSeconds = TestCredentials.MaxAsyncWaitInSeconds;
        }

        protected static string TestItemName()
        {
            return string.Format("test_{0}", DateTime.UtcNow.Ticks.ToString());
        }

        protected static void AssertGetFolderConstraints(Folder folder)
        {
            Assert.That(folder, Is.Not.Null);
            Assert.That(folder.Id, Is.EqualTo("0"));
            Assert.That(folder.Type, Is.EqualTo("folder"));
            Assert.That(folder.Name, Is.EqualTo("All Files"));
        }

        protected static void AssertCreateFolderConstraints(Folder folder, string folderName)
        {
            Assert.That(folder, Is.Not.Null);
            Assert.That(folder.Name, Is.EqualTo(folderName));
            Assert.That(folder.Type, Is.EqualTo("folder"));
        }
    }
}