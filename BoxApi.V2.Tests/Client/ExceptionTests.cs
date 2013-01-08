using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BoxApi.V2.Model;
using NUnit.Framework;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class ExceptionTests
    {
        private static readonly Error TestError = new Error() {Message = "the requested thing was not found", Code = "not found", Status = 404}; 
        [Test]
        public void Ctor()
        {
            var boxException = new BoxException(TestError);
            Assert.That(boxException.Error, Is.SameAs(TestError));
            Assert.That(boxException.Message, Is.EqualTo(TestError.Message));
            Assert.That(boxException.ShortDescription, Is.EqualTo(TestError.Code));
            Assert.That(boxException.HttpStatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
