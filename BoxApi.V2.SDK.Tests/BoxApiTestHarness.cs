using System;
using BoxApi.V2.SDK.Model;
using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    public class BoxApiTestHarness
    {
        protected readonly BoxManager Client = new BoxManager(TestCredentials.ApiKey, null, TestCredentials.AuthorizationToken);
        protected string RootId = "0";

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
            Assert.That(folder, Is.Not.Null);
            Assert.That(folder.Type, Is.EqualTo("folder"));
            Assert.That(folder.Name, Is.EqualTo(expectedName));
            
            if (expectedParentId == null)
            {
                Assert.That(folder.Parent, Is.Null);
            }
            else
            {
                Assert.That(folder.Parent.Id, Is.EqualTo(expectedParentId));
            }

            if (expectedId != null)
            {
                Assert.That(folder.Id, Is.EqualTo(expectedId));
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