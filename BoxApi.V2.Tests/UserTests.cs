using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
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
            Assert.That(userCollection.TotalCount, Is.EqualTo("2"));
        }

        [Test]
        [Ignore("You'll need to change the user's ID to something meaningful.")]
        public void GetSingleUser()
        {
            var user = Client.GetUser("182238740");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("john hoerr"));
        }

        [Test]
        [Ignore("You'll need to change the filter term to something meaningful.")]
        public void FilterUsers()
        {
            var userCollection = Client.GetUsers("john hoerr");
            Assert.That(userCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test,
         ExpectedException(typeof (BoxException)),
         Description("Be careful with this test, it does what it says!"),
        ]
        [Ignore("You'll need to change the user's ID to something meaningful (and hopefully one you won't miss.)")]
        public void DeleteUser()
        {
            var user = Client.GetUser("186819348");
            Client.Delete(user);
            // Should fail when trying to re-fetch the user!
            Client.GetUser("186819348");
        }

        [Test]
        [Ignore("You'll need to change the current owner and new owner ID to something meaningful.")]
        public void MoveRootFolderToAnotherUser()
        {
            var currentOwner = Client.GetUser("186819348");
            var newOwner = Client.GetUser("182238740");
            var folder = Client.MoveRootFolderToAnotherUser(currentOwner, newOwner);
            Assert.That(folder.OwnedBy.Id, Is.EqualTo(newOwner.Id));
            Client.Delete(folder);
        }

        [TestCase("john hoerr")]
        public void Me(string username)
        {
            var user = Client.Me();
            Assert.That(user.Name, Is.EqualTo(username));
        }

        [Test]
        public void CreateEnterpriseUser()
        {
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";
            const string expectedAddress = "some address";
            var expectedSpaceAmount = 2*(long) Math.Pow(2, 30); // 2 GB

            var managedUser = new ManagedUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    Status = UserStatus.Inactive,
                    Address = expectedAddress,
                    SpaceAmount = expectedSpaceAmount,
                };

            var user = Client.CreateUser(managedUser);

            try
            {
                Assert.That(user.Name, Is.EqualTo(expectedName));
                Assert.That(user.Login, Is.EqualTo(expectedLogin));
                Assert.That(user.Role, Is.EqualTo(UserRole.User));
                Assert.That(user.Status, Is.EqualTo(UserStatus.Inactive));
                Assert.That(user.SpaceAmount, Is.EqualTo(expectedSpaceAmount));
                Assert.That(user.SpaceUsed, Is.EqualTo(0));
            }
            finally
            {
                Client.Delete(user, false, true);
            }
        }
    }
}