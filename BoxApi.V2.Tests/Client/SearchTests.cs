using System;
using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class SearchTests : BoxApiTestHarness
    {
        [Test, Ignore("Not currently passing... maybe it takes a while for the search index to be updated?")]
        public void Search()
        {
            Folder folder = Client.CreateFolder(Folder.Root, TestItemName());
            // should find these
            Folder shouldFindThisFolder = Client.CreateFolder(folder, "shouldFindThisFolder");
            Folder shouldFindThisSubfolder = Client.CreateFolder(shouldFindThisFolder, "shouldFindThisSubfolder");
            File shouldFindThisFile = Client.CreateFile(folder, "shouldFindThisFile");
            // Should not find these
            Folder shouldNotFindFolder = Client.CreateFolder(folder, "shouldNotFindThisFolder");
            File shouldNotFindFile = Client.CreateFile(folder, "shouldNotFindThisFile");

            try
            {
                SearchResultCollection results = Client.Search("shouldFind");

                Assert.That(results.TotalCount, Is.EqualTo(3));
                Assert.That(results.Offset, Is.EqualTo(0));
                Assert.That(results.Entries.Select(e => e.Name), Is.EquivalentTo(new[] { shouldFindThisFolder.Name, shouldFindThisSubfolder.Name, shouldFindThisFile.Name }));
            }
            finally
            {
                Client.Delete(folder, true);
            }
        }

        [TestCase((uint)0, (uint)0)]
        [TestCase((uint)1, (uint)0)]
        [TestCase((uint)1, (uint)1)]
        [TestCase((uint)1, (uint)2)]
        [TestCase((uint)2, (uint)0)]
        [TestCase((uint)2, (uint)2)]
        [TestCase((uint)2, (uint)4)]
        [TestCase((uint)2, (uint)1, ExpectedException = typeof(ArgumentException))]
        [TestCase((uint)2, (uint)3, ExpectedException = typeof(ArgumentException))]
        public void OffsetAndLimitAreMultiplesIfSpecified(uint limit, uint offset)
        {
            BoxManager.EnsureOffsetIsMultipleOfLimit(limit, offset);
        }

        [Test, Ignore("This breaks.  The search API seems to be working improperly at the moment.")]
        public void SearchForFilepart()
        {
            uint fetched = 0;
            var results = new List<Entity>();
            SearchResultCollection searchResults;
            do
            {
                searchResults = Client.Search("basics", 5, fetched);
                results.AddRange(searchResults.Entries);
                fetched += (uint)searchResults.Entries.Count;
            } while (fetched < searchResults.TotalCount);
        }

        [Test, Ignore("This breaks.  The search API seems to be working improperly at the moment.")]
        public void SearchForExtension()
        {
            uint fetched = 0;
            var results = new List<Entity>();
            SearchResultCollection searchResults;
            do
            {
                searchResults = Client.Search("xlsx", 5, fetched);
                results.AddRange(searchResults.Entries);
                fetched += (uint)searchResults.Entries.Count;
            } while (fetched < searchResults.TotalCount);
        }
    }
}