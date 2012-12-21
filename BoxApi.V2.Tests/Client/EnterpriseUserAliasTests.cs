using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class EnterpriseUserAliasTests : BoxApiTestHarness
    {
        [Test]
        public void GetAliasesWhenNoneExist()
        {
            var user = Client.Me(new[]{Field.Login, });
            var aliasCollection = Client.GetEmailAliases(user);
            Assert.That(aliasCollection.TotalCount, Is.EqualTo(0));
            Assert.That(aliasCollection.Entries.Any(), Is.False);
        }

        [TestCase("indiana.edu"), Ignore("Change the domain to one used in your organization's enterprise account,")]
        public void AddAlias(string domain)
        {
            var expectedAlias = string.Format("box.api.test.secondary@{0}", domain);
            User user = Client.CreateUser(new ManagedUser() { Name = "test user", Status = UserStatus.Inactive, Login = string.Format("box.api.test.primary@{0}", domain) });

            try
            {
                EmailAlias alias = Client.AddEmailAlias(user, expectedAlias);
                Assert.That(alias.Type, Is.EqualTo(ResourceType.EmailAlias));
                Assert.That(alias.Email, Is.EqualTo(expectedAlias));
            }
            finally
            {
                Client.Delete(user);
            }
        }
    }
}
