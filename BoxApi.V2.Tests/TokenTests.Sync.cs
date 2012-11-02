using NUnit.Framework;

namespace BoxApi.V2.Tests
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