using System;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    public class BoxApiTestHarness
    {
        protected readonly BoxManager Client = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
        protected const string RootId = "0";
        protected readonly Action AbortOnFailure = () => { throw new Exception("Operation failed");};

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

        protected static void AssertFolderConstraints(Folder folder, string expectedName, string expectedParentId, string expectedId = null)
        {
            AssertEntityConstraints(folder, "folder", expectedName, expectedParentId, expectedId);
        }

        protected static void AssertFileConstraints(File file, string expectedName, string expectedParentId, string expectedId = null)
        {
            AssertEntityConstraints(file, "file", expectedName, expectedParentId, expectedId);
        }

        private static void AssertEntityConstraints(HierarchyEntity item, string expectedType, string expectedName, string expectedParentId, string expectedId)
        {
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Type, Is.EqualTo(expectedType));
            Assert.That(item.Name, Is.EqualTo(expectedName));

            if (expectedParentId == null)
            {
                Assert.That(item.Parent, Is.Null);
            }
            else
            {
                Assert.That(item.Parent.Id, Is.EqualTo(expectedParentId));
            }

            if (expectedId != null)
            {
                Assert.That(item.Id, Is.EqualTo(expectedId));
            }
        }

        protected static void AssertSharedLink(SharedLink actual, SharedLink sharedLink)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Access, Is.EqualTo(sharedLink.Access));
            Assert.That(actual.UnsharedAt, Is.GreaterThan(DateTime.MinValue));
            Assert.That(actual.UnsharedAt, Is.LessThan(DateTime.MaxValue));
            Assert.That(actual.Permissions.Download, Is.True);
            Assert.That(actual.Permissions.Preview, Is.True);
        }
    }
}