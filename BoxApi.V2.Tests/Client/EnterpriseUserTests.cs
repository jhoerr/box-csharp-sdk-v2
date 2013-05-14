using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
//    [Ignore("These tests are by necessity geared towards an enterprise account.  You'll need to change things about them to run them for yourself.")]
    [TestFixture]
    public class EnterpriseUserTests : BoxApiTestHarness
    {
        [Test]
        public void CreateEnterpriseUser()
        {
            Thread.Sleep(2000); // give any user deletes time to happen
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";
            const string expectedAddress = "some address";
            long expectedSpaceAmount = 2*(long) Math.Pow(2, 30); // 2 GB

            var user = new EnterpriseUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    Status = UserStatus.Active,
                    Address = expectedAddress,
                    SpaceAmount = expectedSpaceAmount,
                };

            EnterpriseUser actualUser = Client.CreateUser(user);

            try
            {
                Assert.That(actualUser.Name, Is.EqualTo(expectedName));
                Assert.That(actualUser.Address, Is.EqualTo(expectedAddress));
                Assert.That(actualUser.Login, Is.EqualTo(expectedLogin));
                Assert.That(actualUser.Role, Is.EqualTo(UserRole.User));
                Assert.That(actualUser.Status, Is.EqualTo(UserStatus.Active));
                Assert.That(actualUser.SpaceAmount, Is.EqualTo(expectedSpaceAmount));
                Assert.That(actualUser.SpaceUsed, Is.EqualTo(0));
            }
            finally
            {
                Client.Delete(actualUser, false, true);
            }
        }

        [Test]
        public void CreateCoAdminUser()
        {
            Thread.Sleep(2000); // give any user deletes time to happen
            const string expectedName = "foo bar";
            const string expectedLogin = "ajoioejwofiwej@gmail.com";

            var user = new EnterpriseUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    Status = UserStatus.Inactive,
                    Role = UserRole.CoAdmin,
                };

            EnterpriseUser actualUser = Client.CreateUser(user);

            try
            {
                Assert.That(actualUser.Name, Is.EqualTo(expectedName));
                Assert.That(actualUser.Login, Is.EqualTo(expectedLogin));
                Assert.That(actualUser.Role, Is.EqualTo(UserRole.CoAdmin));
                Assert.That(actualUser.Status, Is.EqualTo(UserStatus.Inactive));
            }
            finally
            {
                Client.Delete(actualUser, false, true);
            }
        }

        [Test,
         ExpectedException(typeof (BoxException)),
         Description("Be careful with this test, it does what it says!"),
        ]
        [Ignore("You'll need to change the user's ID to something meaningful (and hopefully one you won't miss.)")]
        public void DeleteUser()
        {
            EnterpriseUser user = Client.GetUser("186819348");
            Client.Delete(user);
            // Should fail when trying to re-fetch the user!
            Client.GetUser("186819348");
        }

        [Test]
        //You'll need to change the filter term to something meaningful.
        public void FilterUsersByName()
        {
            var name = "john hoerr";
            EnterpriseUserCollection enterpriseUserCollection = Client.GetUsers(name);
            Assert.That(enterpriseUserCollection.TotalCount, Is.EqualTo(1));
            Assert.That(enterpriseUserCollection.Entries.Single().Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        //You'll need to change the filter term to something meaningful.
        public void FilterUsersByEmail()
        {
            var login = "jhoerr@gmail.com"; 
            EnterpriseUserCollection enterpriseUserCollection = Client.GetUsers(login);
            Assert.That(enterpriseUserCollection.TotalCount, Is.EqualTo(1));
            Assert.That(enterpriseUserCollection.Entries.Single().Login.Equals(login,StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        // This could be more than one, depending on how your account is set up.
        // If you're an enterprise admin, the user collection will contain all users in the enterprise.
        public void GetAllUsers()
        {
            EnterpriseUserCollection enterpriseUserCollection = Client.GetUsers();
            Assert.That(enterpriseUserCollection.TotalCount, Is.EqualTo(2));
        }

        [Test, Ignore("You'll need to change the current owner and new owner ID to something meaningful.")]
        public void MoveRootFolderToAnotherUser()
        {
            EnterpriseUser currentOwner = Client.GetUser("186819348");
            EnterpriseUser newOwner = Client.GetUser("182238740");
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

            var managedUser = new EnterpriseUser
                {
                    Name = expectedName,
                    Login = expectedLogin,
                    TrackingCodes = expectedTrackingCodes,
                };

            EnterpriseUser user = Client.CreateUser(managedUser);

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
            Thread.Sleep(2000); // give any user deletes time to happen
            var managedUser = new EnterpriseUser
                {
                    Name = "will change",
                    Login = "ajoioejwofiwej@gmail.com",
                    Status = UserStatus.Inactive,
                    Address = "will change street",
                    SpaceAmount = -1,
                    IsExemptFromDeviceLimits = false,
                    IsExemptFromLoginVerification = false,
                    IsSyncEnabled = false,
                    CanSeeManagedUsers = false,
                    Role = UserRole.User,
                };

            EnterpriseUser user = Client.CreateUser(managedUser);

            const string expectedName = "foo bar";
            const string expectedAddress = "some address";
            long expectedSpaceAmount = 2*(long) Math.Pow(2, 30); // 2 GB

            user.Name = expectedName;
            user.Address = expectedAddress;
            user.Status = UserStatus.Active;
            user.SpaceAmount = expectedSpaceAmount;
            user.IsExemptFromDeviceLimits = true;
            user.IsExemptFromLoginVerification = true;
            user.IsSyncEnabled = true;
            user.CanSeeManagedUsers = true;
            user.Role = UserRole.CoAdmin;

            try
            {
                EnterpriseUser updatedUser = Client.UpdateUser(user);
                Assert.That(updatedUser.Name, Is.EqualTo(expectedName));
                Assert.That(updatedUser.Address, Is.EqualTo(expectedAddress));
                Assert.That(updatedUser.IsExemptFromDeviceLimits, Is.True);
                Assert.That(updatedUser.IsExemptFromLoginVerification, Is.True);
                Assert.That(updatedUser.IsSyncEnabled, Is.True);
                Assert.That(updatedUser.CanSeeManagedUsers, Is.True);
                Assert.That(updatedUser.Role, Is.EqualTo(UserRole.CoAdmin));
                Assert.That(updatedUser.SpaceAmount, Is.EqualTo(expectedSpaceAmount));
//                Assert.That(updatedUser.Status, Is.EqualTo(UserStatus.Active));
            }
            finally
            {
                Client.Delete(user, false, true);
            }
        }

        [Test, ExpectedException(typeof (BoxException))]
        public void CannotCreateAdminUser()
        {
            var enterpriseUser = new EnterpriseUser
                {
                    Role = UserRole.Admin,
                    Login = "no.body@gmail.com",
                    Name = "No Body",
                };

            Client.CreateUser(enterpriseUser);
        }

        [Test, ExpectedException(typeof(BoxException)),]
        public void ConvertToStandaloneUser()
        {
            var enterpriseUser = new EnterpriseUser
            {
                Role = UserRole.User,
                Login = "box.csharp.sdk2@gmail.com",
                Name = "No Body",
            };

            EnterpriseUser entUser = null;
            try
            {
                entUser = Client.CreateUser(enterpriseUser);
                var standaloneUser = Client.ConvertToStandaloneUser(entUser);
                Assert.That(standaloneUser, Is.Not.Null);
                Assert.That(standaloneUser.Name, Is.EqualTo(entUser.Name));
                Assert.That(standaloneUser.Login, Is.EqualTo(entUser.Login));
            }
            finally
            {
                if (entUser != null)
                {
                    Client.Delete(entUser);
                }
            }
        }


        [Test]
        public void RequirePasswordReset()
        {
            var enterpriseUser = new EnterpriseUser
            {
                Role = UserRole.User,
                Login = "nobody_3ijfie2joefi2joefij2e@gmail.com",
                Name = "No Body",
            };

            EnterpriseUser entUser = null;
            try
            {
                entUser = Client.CreateUser(enterpriseUser);
                Assert.That(entUser.IsPasswordResetRequired, Is.False);

                entUser.IsPasswordResetRequired = true;
                var updated = Client.UpdateUser(entUser);
                Assert.That(updated.IsPasswordResetRequired, Is.True);

                // Once set, this can't be unset.
                updated.IsPasswordResetRequired = false;
                updated = Client.UpdateUser(updated);
                Assert.That(updated.IsPasswordResetRequired, Is.True);
            }
            finally
            {
                if (entUser != null)
                {
                    Client.Delete(entUser);
                }
            }
        }
    }
}