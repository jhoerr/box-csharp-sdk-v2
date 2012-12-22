using System;
using System.IO;
using BoxApi.V2.Authentication.OAuth2;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace BoxApi.V2.Tests.Harness
{
    public class TestConfigInfo
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string CollaboratingUserEmail { get; set; }
        public string CollaboratingUserId { get; set; }

        public static TestConfigInfo Get()
        {
            TestConfigInfo testConfigInfo;
            var fi = TestConfigFileInfo();
            if (!fi.Exists)
            {
                throw new Exception("You need to provide the tests with your Access Token in a test_info.json file.  Please run through the authentication sample in BoxApi.V2.Samples.WebAuthentication.MVC in order to create this file.");
            }

            using (var stream = fi.OpenText())
            {
                string content = stream.ReadToEnd();
                testConfigInfo = new JsonDeserializer().Deserialize<TestConfigInfo>(new RestResponse { Content = content });
            }
            return testConfigInfo;
        }

        public static void Update(OAuthToken refreshAccessToken)
        {
            var testConfigInfo = Get();
            testConfigInfo.AccessToken = refreshAccessToken.AccessToken;
            testConfigInfo.RefreshToken = refreshAccessToken.RefreshToken;

            using (var stream = TestConfigFileInfo().Open(FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var updatedInfo = new JsonSerializer().Serialize(testConfigInfo);
                    writer.Write(updatedInfo);
                }
            }
        }

        private static FileInfo TestConfigFileInfo()
        {
            return new FileInfo(@"..\..\test_info.json");
        }
    }
}