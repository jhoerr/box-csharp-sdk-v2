using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture, Ignore("Requires access to private beta.")]
    public class TokenTests : BoxApiTestHarness
    {
        [Test]
        public void GetToken()
        {
            var boxToken = Client.CreateToken("box.tokens.test@gmail.com");
            Assert.That(boxToken.Token, Is.Not.Null);
        }

    }
}