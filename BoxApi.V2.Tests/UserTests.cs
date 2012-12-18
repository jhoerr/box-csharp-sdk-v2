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
        [TestCase("john hoerr")]
        public void Me(string username)
        {
            User user = Client.Me();
            Assert.That(user.Name, Is.EqualTo(username));
        }

        [Test]
        public void CreateEnterpriseUser()
        {
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";
            const string expectedAddress = "some address";
            long expectedSpaceAmount = 2*(long) Math.Pow(2, 30); // 2 GB

            var managedUser = new ManagedUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    Status = UserStatus.Inactive,
                    Address = expectedAddress,
                    SpaceAmount = expectedSpaceAmount,
                };

            User user = Client.CreateUser(managedUser);

            try
            {
                Assert.That(user.Name, Is.EqualTo(expectedName));
                Assert.That(user.Address, Is.EqualTo(expectedAddress));
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

        [Test,
         ExpectedException(typeof (BoxException)),
         Description("Be careful with this test, it does what it says!"),
        ]
        [Ignore("You'll need to change the user's ID to something meaningful (and hopefully one you won't miss.)")]
        public void DeleteUser()
        {
            User user = Client.GetUser("186819348");
            Client.Delete(user);
            // Should fail when trying to re-fetch the user!
            Client.GetUser("186819348");
        }

        [Test]
        [Ignore("You'll need to change the filter term to something meaningful.")]
        public void FilterUsers()
        {
            UserCollection userCollection = Client.GetUsers("john hoerr");
            Assert.That(userCollection.TotalCount, Is.EqualTo("1"));
        }

        [Test]
        public void GetAllUsers()
        {
            UserCollection userCollection = Client.GetUsers();
            // This could be more than one, depending on how your account is set up.
            // If you're an enterprise admin, the user collection will contain all users in the enterprise.
            Assert.That(userCollection.TotalCount, Is.EqualTo(1));
        }

        [Test]
        public void GetLargeNumberOfUsers()
        {
//            var totalCount = Client.GetUsers();
            UserCollection users = Client.GetUsers(null, 1, 500);
        }

        [Test]
        [Ignore("You'll need to change the user's ID to something meaningful.")]
        public void GetSingleUser()
        {
            User user = Client.GetUser("182238740");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("john hoerr"));
        }

        [Test]
        [Ignore("You'll need to change the current owner and new owner ID to something meaningful.")]
        public void MoveRootFolderToAnotherUser()
        {
            User currentOwner = Client.GetUser("186819348");
            User newOwner = Client.GetUser("182238740");
            Folder folder = Client.MoveRootFolderToAnotherUser(currentOwner, newOwner);
            Assert.That(folder.OwnedBy.Id, Is.EqualTo(newOwner.Id));
            Client.Delete(folder);
        }

        [Test]
        public void TrackingCodes()
        {
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";

            var expectedTrackingCodes = new List<TrackingCode>
                {
                    new TrackingCode("key1", "value1"),
                    new TrackingCode("key2", "value2"), 
                };

            var managedUser = new ManagedUser
            {
                Name = expectedName,
                Login = expectedLogin,
//                TrackingCodes = expectedTrackingCodes,
            };

            User user = Client.CreateUser(managedUser, new[] { Field.TrackingCodes, Field.Name, Field.Login, });

            try
            {
                Assert.That(user.Name, Is.EqualTo(expectedName));
                Assert.That(user.Login, Is.EqualTo(expectedLogin));
//                Assert.That(user.TrackingCodes, Is.EqualTo(expectedTrackingCodes));
            }
            finally
            {
                Client.Delete(user, false, true);
            }
        }

        [Test]
        public void UpdateEnterpriseUser()
        {
            var managedUser = new ManagedUser
                {
                    Name = "will change",
                    Login = "ajoioejwofiwej@gmail.com",
                    Status = UserStatus.Inactive,
                    Address = "will change street",
                    SpaceAmount = -1,
                };

            User user = Client.CreateUser(managedUser);

            const string expectedName = "foo bar";
            const string expectedAddress = "some address";
            long expectedSpaceAmount = 2*(long) Math.Pow(2, 30); // 2 GB

            user.Name = expectedName;
            user.Address = expectedAddress;
            user.Status = UserStatus.Active;
            user.SpaceAmount = expectedSpaceAmount;

            try
            {
                User updatedUser = Client.UpdateUser(user);
                Assert.That(updatedUser.Name, Is.EqualTo(expectedName));
                Assert.That(updatedUser.Address, Is.EqualTo(expectedAddress));
                Assert.That(updatedUser.Status, Is.EqualTo(UserStatus.Active));
                Assert.That(updatedUser.SpaceAmount, Is.EqualTo(expectedSpaceAmount));
            }
            finally
            {
                Client.Delete(user, false, true);
            }
        }
    }
}