using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class EventTestsSync : BoxApiTestHarness
    {
        [Test]
        public void CanGetCurrentStreamPosition()
        {
            var latestPosition = Client.GetCurrentStreamPosition();
            Assert.That(latestPosition, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetEventForUser()
        {
            var latestPosition = Client.GetCurrentStreamPosition();
            var testFolder = Client.CreateFolder(RootId, TestItemName());
            try
            {
                Thread.Sleep(5000);
                var events = Client.GetUserEvents(latestPosition);
                Assert.That(events.ChunkSize, Is.EqualTo(1));
                Assert.That(events.Entries.Count, Is.EqualTo(1));
                var entry = events.Entries.Single();
                Assert.That(entry.EventType, Is.EqualTo(StandardEventType.ItemCreate));
                Assert.That(entry.Source.Id, Is.EqualTo(testFolder.Id));
                Assert.That(entry.Source.CreatedAt, Is.EqualTo(testFolder.CreatedAt));
            }
            finally
            {
                Client.Delete(testFolder);
            }
        }

        [Test]
        public void NonHierarchyEventsCanBeExcluded()
        {
            var testFile = Client.CreateFile(RootId, TestItemName());
            var latestPosition = Client.GetCurrentStreamPosition();
            Client.CreateComment(testFile, "comment!");
            try
            {
                Thread.Sleep(5000);
                var events = Client.GetUserEvents(latestPosition, StreamType.Changes);
                Assert.That(events.ChunkSize, Is.EqualTo(0));
                Assert.That(events.Entries.Count, Is.EqualTo(0));
            }
            finally
            {
                Client.Delete(testFile);
            }
        }

        [Test, Ignore("Requires Box enterprise admin account.")]
        public void CanGetEventForEnterprise()
        {
            // Not even really sure what to look for here...
            var events = Client.GetEnterpriseEvents(0, 100, DateTime.Now.AddDays(-1), DateTime.Now);
        }

        [Test, Ignore("Requires Box enterprise admin account.")]
        public void GetEventsOfTypeForEnterprise()
        {
            Console.Out.WriteLine("** Invited **");
            PrintUsersDoingAction(EnterpriseEventType.CollaborationInvite);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("** Accepted **");
            PrintUsersDoingAction(EnterpriseEventType.CollaborationAccept);
        }

        private void PrintUsersDoingAction(EnterpriseEventType enterpriseEventType)
        {
            const int pageSize = 500;
            var events = new List<EnterpriseEvent>();

            EnterpriseEventCollection page;
            do
            {
                page = Client.GetEnterpriseEvents(0, pageSize, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, new[] {enterpriseEventType,});
                events.AddRange(page.Entries);
            } while (page.ChunkSize == pageSize);
            var count = events.Select(e => e.CreatedBy.Login).Distinct().OrderBy(l => l).Count();
            Console.Out.WriteLine(string.Join("\n", count));
        }
    }
}