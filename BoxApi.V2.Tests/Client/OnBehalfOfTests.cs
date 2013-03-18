using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    class OnBehalfOfTests : BoxApiTestHarness
    {
        [Test]
        public void Me()
        {
            const string login = "box.csharp.sdk@gmail.com";
            var user = Client.OnBehalfOf(login).Me();
            Assert.That(user.Login, Is.EqualTo(login));
        }
    }
}
