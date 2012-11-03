using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BoxApi.V2.Tests
{
    [TestFixture]
    public class UserTests : BoxApiTestHarness
    {
        [Test]
        public void GetAllUsers()
        {
            var userCollection = Client.GetUsers();
            // This could be more than one, depending on how your account is set up.
            // If you're an enterprise admin, the user collection will contain all users in the enterprise.
            Assert.That(userCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test, Ignore("You'll need to change the user's ID to get something meaningful here.")]
        public void GetSingleUser()
        {
            var user = Client.GetUser("186800768");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("box csharp sdk"));
        }

        [Test, Ignore("You'll need to change the filter term to get something meaningful here.")]
        public void FilterUsers()
        {
            var userCollection = Client.GetUsers("box csharp");
            Assert.That(userCollection.TotalCount, Is.EqualTo("1"));
        }
    }
}
