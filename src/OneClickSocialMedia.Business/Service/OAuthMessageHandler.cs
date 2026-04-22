using System.Net.Http.Headers;

namespace OneClickSocialMedia.Business.Service
{
    /// <summary>
    /// Basic DelegatingHandler that creates an OAuth authorization header based on the OAuthBase
    /// class downloaded from http://oauth.net
    /// </summary>
    internal class OAuthMessageHandler : DelegatingHandler
    {
        // Obtain these values by creating a Twitter app at http://dev.twitter.com/
        private static string _consumerKey;
        private static string _consumerSecret;
        private static string _token;
        private static string _tokenSecret;


        private OAuthBase _oAuthBase = new OAuthBase();

        public OAuthMessageHandler(
        string consumerKey,
        string consumerSecret,
        string token,
        string tokenSecret)
        : base(new HttpClientHandler())
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _token = token;
            _tokenSecret = tokenSecret;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Compute OAuth header 
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
                request.RequestUri,
                _consumerKey,
                _consumerSecret,
                _token,
                _tokenSecret,
                request.Method.Method,
                _oAuthBase.GenerateTimeStamp(),
                _oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            return base.SendAsync(request, cancellationToken);
        }
    }
}