using OneClickSocialMedia.Constants;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
using System.Text;
using System.Text.Json;
namespace OneClickSocialMedia.Business.Service
{
    public class TwitterPostService : ITwitterPostService
    {

        private readonly IHttpClientFactory httpClientFactory;

        public TwitterPostService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, TwitterCredentialsDto twitterCredentialsDto)
        {
            return PostToXAsync(commentToPost, twitterCredentialsDto.ApiKey, twitterCredentialsDto.ApiSecret, twitterCredentialsDto.AccessToken, twitterCredentialsDto.AccessTokenSecret, TwitterEndpoints.Tweet, string.Empty);
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, Stream? fileImage, string? ImageUrl, TwitterCredentialsDto twitterCredentialsDto)
        {
            //validate image and is it url or file image? what takes precedent if both provided?? 
            //use this TwitterEndpoints.Tweet AND TwitterEndpoints.MediaUpload here
            throw new NotImplementedException();
        }

        private async Task<string> PostToXAsync(string tweet, string apiKey, string apiSecret, string token, string tokenSecret, string endpoint, string? mediaId)
        {
            OAuthMessageHandler oauth = new OAuthMessageHandler(apiKey, apiSecret, token, tokenSecret);


            // Build payload
            object tweetData = string.IsNullOrWhiteSpace(mediaId)
                ? new { text = tweet }
                : new
                {
                    text = tweet,
                    media = new
                    {
                        media_ids = new[] { mediaId }
                    }
                };

            string jsonData = JsonSerializer.Serialize(tweetData);

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            using HttpClient httpClient = new HttpClient(oauth, disposeHandler: true);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "Tweet sent successfully";

            // NO exception for now later can do my own way implementing exceptions!!
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to send tweet. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        }



    }
}
