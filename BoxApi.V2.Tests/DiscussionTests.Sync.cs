using System.Linq;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class DiscussionTestsSync : BoxApiTestHarness
    {
        private Folder _folder;

        [Test, ExpectedException(typeof (BoxException))]
        public void CantCreateDisussionOnRootFolder()
        {
            Client.CreateDiscussion(Folder.Root, "foo");
        }

        [SetUp]
        public void SetUp()
        {
            _folder = null;
        }

        [TearDown]
        public void TearDown()
        {
            if (_folder != null)
            {
                Client.Delete(_folder);
            }
        }

        [Test]
        public void CreateDiscussion()
        {
            const string name = "name";
            const string description = "description";
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);

            var discussion = Client.CreateDiscussion(_folder.Id, name, description);

            Assert.That(discussion, Is.Not.Null);
            Assert.That(discussion.Parent.Id, Is.EqualTo(_folder.Id));
            Assert.That(discussion.Name, Is.EqualTo(name));
            Assert.That(discussion.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetDiscussion()
        {
            const string name = "name";
            const string description = "description";
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var discussion = Client.CreateDiscussion(_folder.Id, name, description);

            var actual = Client.GetDiscussion(discussion.Id);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Parent.Id, Is.EqualTo(_folder.Id));
            Assert.That(actual.Name, Is.EqualTo(name));
            Assert.That(actual.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetAFoldersDiscussions()
        {
            const string firstName = "name the first";
            const string secondName = "name the second";
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            Client.CreateDiscussion(_folder.Id, firstName);
            Client.CreateDiscussion(_folder.Id, secondName);

            var actual = Client.GetDiscussions(_folder);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
            Assert.That(actual.Entries.Any(), Is.True);
            Assert.That(actual.Entries.Count(), Is.EqualTo(2));
            Assert.That(actual.Entries.Any(d => d.Name.Equals(firstName)), Is.True);
            Assert.That(actual.Entries.Any(d => d.Name.Equals(secondName)), Is.True);
        }


        [Test, Description("Should result in a 404 on the Discussion"), ExpectedException(typeof (BoxException))]
        public void DeleteDiscussion()
        {
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var discussion = Client.CreateDiscussion(_folder.Id, "foo");

            Client.Delete(discussion);

            var deleted = Client.GetDiscussion(discussion.Id);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void AddCommentToDiscussion()
        {
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var discussion = Client.CreateDiscussion(_folder.Id, "foo");
            const string message = "message";

            var comment = Client.CreateComment(discussion, message);

            Assert.That(comment, Is.Not.Null);
            Assert.That(comment.IsReplyComment, Is.False);
            Assert.That(comment.Message, Is.EqualTo(message));
        }

        [Test]
        public void GetDiscussionComments()
        {
            const string firstMessage = "message the first";
            const string secondMessage = "message the second";
            _folder = Client.CreateFolder(Folder.Root, TestItemName(), null);
            var discussion = Client.CreateDiscussion(_folder.Id, "foo");
            Client.CreateComment(discussion, firstMessage);
            Client.CreateComment(discussion, secondMessage);

            var comments = Client.GetComments(discussion);

            Assert.That(comments, Is.Not.Null);
            Assert.That(comments.TotalCount, Is.EqualTo(2));
            Assert.That(comments.Entries.Any(), Is.True);
            Assert.That(comments.Entries.Count(), Is.EqualTo(2));
            Assert.That(comments.Entries.Any(c => c.Message.Equals(firstMessage)), Is.True);
            Assert.That(comments.Entries.Any(c => c.Message.Equals(secondMessage)), Is.True);
        }
    }
}