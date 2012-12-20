using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Authentication.OAuth2
{
    [TestFixture]
    public class AuthenticatorTests
    {
        [Test]
        public void GetAuthorizationUrl()
        {
            var authenticator = new TokenProvider("clientId", "clientSecret");
            var authorizationUrl = authenticator.GetAuthorizationUrl("http://foo.com");
            Assert.That(authorizationUrl, Is.EqualTo("https://api.box.com/oauth2/authorize?response_type=code&client_id=clientId&state=authenticated&redirect_uri=http%3A%2F%2Ffoo.com"));
        }

        [Test]
        public void RefreshAccessToken()
        {
            var testConfigInfo = TestConfigInfo.Get();
            var authenticator = new TokenProvider(testConfigInfo.ClientId, testConfigInfo.ClientSecret);
            var refreshAccessToken = authenticator.RefreshAccessToken(testConfigInfo.RefreshToken);
            Assert.That(refreshAccessToken.AccessToken, Is.Not.Null);
            Assert.That(refreshAccessToken.AccessToken, Is.Not.EqualTo(testConfigInfo.AccessToken));
            Assert.That(refreshAccessToken.RefreshToken, Is.Not.Null);
            Assert.That(refreshAccessToken.RefreshToken, Is.Not.EqualTo(testConfigInfo.RefreshToken));
            TestConfigInfo.Update(refreshAccessToken);
        }
    }
}
