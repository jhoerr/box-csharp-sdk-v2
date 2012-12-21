using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [Ignore("These tests are by necessity geared towards an enterprise account.  You'll need to change things about them to run them for yourself.")]
    public class EnterpriseUserTests : BoxApiTestHarness
    {
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

            User user = Client.CreateUser(managedUser, new Field[] { Field.Name, Field.Address, Field.Login, Field.Role, Field.Status, Field.SpaceAmount, Field.SpaceUsed,  });

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

        [Test]
        public void CreateCoAdminUser()
        {
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";

            var managedUser = new ManagedUser
            {
                Name = expectedName,
                Login = expectedLogin,
                Status = UserStatus.Inactive,
                Role = UserRole.CoAdmin,
            };

            User user = Client.CreateUser(managedUser, new Field[]{Field.Name, Field.Login, Field.Role, Field.Status});

            try
            {
                Assert.That(user.Name, Is.EqualTo(expectedName));
                Assert.That(user.Login, Is.EqualTo(expectedLogin));
                Assert.That(user.Role, Is.EqualTo(UserRole.CoAdmin));
                Assert.That(user.Status, Is.EqualTo(UserStatus.Inactive));
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
        //You'll need to change the filter term to something meaningful.
        public void FilterUsers()
        {
            UserCollection userCollection = Client.GetUsers("john hoerr");
            Assert.That(userCollection.TotalCount, Is.EqualTo(1));
        }

        [Test]
        // This could be more than one, depending on how your account is set up.
        // If you're an enterprise admin, the user collection will contain all users in the enterprise.
        public void GetAllUsers()
        {
            UserCollection userCollection = Client.GetUsers();
            Assert.That(userCollection.TotalCount, Is.EqualTo(1));
        }

        [Test]
        public void GetLargeNumberOfUsers()
        {
            UserCollection users = Client.GetUsers(null, 1000, 0, new[] {Field.SpaceAmount,});
            Assert.That(users.TotalCount, Is.EqualTo(1000));
            Assert.That(users.Entries.Count, Is.EqualTo(1000));
        }

        [Test]
        // You'll need to change the current owner and new owner ID to something meaningful.
        public void MoveRootFolderToAnotherUser()
        {
            User currentOwner = Client.GetUser("186819348");
            User newOwner = Client.GetUser("182238740");
            Folder folder = Client.MoveRootFolderToAnotherUser(currentOwner, newOwner);
            Assert.That(folder.OwnedBy.Id, Is.EqualTo(newOwner.Id));
            Client.Delete(folder);
        }

        [Test]
        // "These tracking codes are particular to one enterprise account -- you'll need to change them for yours."
        public void TrackingCodes()
        {
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";

            var expectedTrackingCodes = new List<TrackingCode>
                {
                    new TrackingCode("Foo", "value1"),
                    new TrackingCode("Bar", "value2"),
                };

            var managedUser = new ManagedUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    TrackingCodes = expectedTrackingCodes,
                };

            User user = Client.CreateUser(managedUser, new[] {Field.Name, Field.Login, Field.TrackingCodes,});

            try
            {
                Assert.That(user.Name, Is.EqualTo(expectedName));
                Assert.That(user.Login, Is.EqualTo(expectedLogin));
                Assert.That(user.TrackingCodes, Is.EquivalentTo(expectedTrackingCodes));
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