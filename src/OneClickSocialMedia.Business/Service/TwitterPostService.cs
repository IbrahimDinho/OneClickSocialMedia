using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
namespace OneClickSocialMedia.Business.Service
{
    public class TwitterPostService : ITwitterPostService
    {
        private string XapiKey;
        private string XapiSecret;
        private string Xaccesstoken;
        private string Xtokendecret;
        private const string Xendpoint = "https://api.twitter.com/2/tweets";
        private const string Xmediaendpoint = "https://upload.twitter.com/1.1/media/upload.json";

        private readonly IHttpClientFactory httpClientFactory;

        public TwitterPostService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, TwitterCredentialsDto twitterCredentialsDto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, Stream? fileImage, string? ImageUrl, TwitterCredentialsDto twitterCredentialsDto)
        {
            throw new NotImplementedException();
        }

        //private async Task<string> PostToXAsync(string tweet, string apiKey, string apiSecret, string token, string tokenSecret, string endpoint, string? mediaId)
        //{
        //    var oauth = new OAuthMessageHandler(apiKey, apiSecret, token, tokenSecret);


        //    // Build payload
        //    object tweetData = string.IsNullOrWhiteSpace(mediaId)
        //        ? new { text = tweet }
        //        : new
        //        {
        //            text = tweet,
        //            media = new
        //            {
        //                media_ids = new[] { mediaId }
        //            }
        //        };

        //    string jsonData = JsonSerializer.Serialize(tweetData);

        //    using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        //    {
        //        Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        //    };

        //     //Prefer HttpClientFactory for handler-less clients.
        //     //Since OAuth is a handler instance, we attach it here.
        //    using var httpClient = new HttpClient(oauth, disposeHandler: true);

        //    var response = await httpClient.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //        return "Tweet sent successfully";

        //    // NO exception for now later can do my own way implementing exceptions!!
        //    var body = await response.Content.ReadAsStringAsync();
        //    throw new HttpRequestException(
        //        $"Failed to send tweet. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        //}



    }
}
