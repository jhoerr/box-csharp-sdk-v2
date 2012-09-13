using NUnit.Framework;

namespace BoxApi.V2.SDK.Tests
{
    public class BoxApiTestHarness
    {
        protected int MaxWaitInSeconds { get; set; }

        [SetUp]
        public void Setup()
        {
            MaxWaitInSeconds = TestCredentials.MaxAsyncWaitInSeconds;
        }
    }
}