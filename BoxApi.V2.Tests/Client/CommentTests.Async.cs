using BoxApi.V2.Model;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;
using RestSharp;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class CommentTestsAsync : BoxApiTestHarness
    {
        private const string Comment1 = "comment 1";
        private const string Comment2 = "comment 2";

        [Test]
        public void Add()
        {
            // Arrange
            Comment actual = null;
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);

            // Act
            Client.CreateComment(newComment => { actual = newComment; }, AbortOnFailure, file, Comment1, null);

            AssertActionComplete(ref actual);
            Client.Delete(file);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Message, Is.EqualTo(Comment1));
        }

        [Test]
        public void Delete()
        {
            IRestResponse actual = null;
            File file = Client.CreateFile(RootId, TestItemName());
            Comment comment = Client.CreateComment(file, Comment1);

            // Act
            Client.Delete(response => { actual = response; }, AbortOnFailure, comment);

            AssertActionComplete(ref actual);
            CommentCollection commentCollection = Client.GetComments(file);
            Client.Delete(file);
            Assert.That(commentCollection.TotalCount, Is.EqualTo(0));
        }

        [Test]
        public void Get()
        {
            // Arrange
            Comment actual = null;
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            Comment comment = Client.CreateComment(file, Comment1);

            // Act
            Client.GetComment(gotComment => { actual = gotComment; }, AbortOnFailure, comment, null);

            AssertActionComplete(ref actual);
            Client.Delete(file);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Message, Is.EqualTo(Comment1));
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            CommentCollection actual = null;
            string fileName = TestItemName();
            File file = Client.CreateFile(RootId, fileName);
            Client.CreateComment(file, Comment1);
            Client.CreateComment(file, Comment2);

            // Act
            Client.GetComments(comments => { actual = comments; }, AbortOnFailure, file, null);

            AssertActionComplete(ref actual);
            Client.Delete(file);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.TotalCount, Is.EqualTo(2));
        }

        [Test]
        public void Update()
        {
            Comment actual = null;
            File file = Client.CreateFile(RootId, TestItemName());
            Comment comment = Client.CreateComment(file, Comment1);
            comment.Message = Comment2;
            // Act
            Client.Update(updatedComment => { actual = updatedComment; }, AbortOnFailure, comment, null);

            AssertActionComplete(ref actual);
            Client.Delete(file);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Message, Is.EqualTo(Comment2));
        }
    }
}