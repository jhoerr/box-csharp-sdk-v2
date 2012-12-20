namespace BoxApi.V2.Authentication.OAuth2
{
    public class OAuthToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
    }
}