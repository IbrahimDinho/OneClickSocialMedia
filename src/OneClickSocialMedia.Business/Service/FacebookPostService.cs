using Newtonsoft.Json;
using OneClickSocialMedia.Constants;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
namespace OneClickSocialMedia.Business.Service
{
    public class FacebookPostService : IFacebookPostService
    {
        private readonly HttpClient client;

        public FacebookPostService(HttpClient client)
        {
            this.client = client;
        }

        /// <inheritdoc/>
        public async Task<string> PostAsync(string commentToPost, FacebookCredentialsDto facebookCredentialsDto)
        {
            string fbUrl = $"{FacebookEndpoints.FacebookBaseURL}{facebookCredentialsDto.PageId}/feed?access_token={facebookCredentialsDto.AccessToken}";

            var post = new
            {
                message = commentToPost
            };

            string jsonString = JsonConvert.SerializeObject(post);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(fbUrl, content);

            if (response.IsSuccessStatusCode)
                return "Fb Post sent successfully";

            string body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to send post. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        }

        /// <inheritdoc/>
        public async Task<string> PostAsync(string commentToPost, string? imageUrl, FacebookCredentialsDto facebookCredentialsDto)
        {
            ValidateImageFields(imageUrl);
            string fbUrl = $"{FacebookEndpoints.FacebookBaseURL}{facebookCredentialsDto.PageId}/photos?access_token={facebookCredentialsDto.AccessToken}";

            var post = new
            {
                message = commentToPost,
                url = imageUrl
            };

            string jsonString = JsonConvert.SerializeObject(post);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(fbUrl, content);

            if (response.IsSuccessStatusCode)
                return "Facebook image post sent successfully";

            string body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to send Facebook image post. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        }

        private void ValidateImageFields(string? imageUrl)
        {
            bool hasUrl = !string.IsNullOrWhiteSpace(imageUrl);

            if (hasUrl && !IsUrl(imageUrl))
            {
                throw new ArgumentException("Input is not a valid URL");
            }
        }

        private static bool IsUrl(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }


    }
}
