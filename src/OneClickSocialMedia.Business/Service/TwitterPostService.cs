using Newtonsoft.Json;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Constants;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
using System.Text;
namespace OneClickSocialMedia.Business.Service
{
    public class TwitterPostService : ITwitterPostService
    {


        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, TwitterCredentialsDto twitterCredentialsDto)
        {
            return PostToXAsync(commentToPost, twitterCredentialsDto.ApiKey, twitterCredentialsDto.ApiSecret, twitterCredentialsDto.AccessToken, twitterCredentialsDto.AccessTokenSecret, TwitterEndpoints.Tweet, string.Empty);
        }

        /// <inheritdoc/>
        public async Task PostAsync(string commentToPost, Stream? fileImage, string? ImageUrl, TwitterCredentialsDto twitterCredentialsDto)
        {
            ValidateImageFields(fileImage, ImageUrl);
            using var imageStream = await GetImageStreamAsync(ImageUrl);
            string mediaId = await UploadFileToXAsync(twitterCredentialsDto.ApiKey, twitterCredentialsDto.ApiSecret, twitterCredentialsDto.AccessToken, twitterCredentialsDto.AccessTokenSecret, TwitterEndpoints.MediaUpload, imageStream);

            PostToXAsync(commentToPost, twitterCredentialsDto.ApiKey, twitterCredentialsDto.ApiSecret, twitterCredentialsDto.AccessToken, twitterCredentialsDto.AccessTokenSecret, TwitterEndpoints.Tweet, mediaId);
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

            string jsonData = JsonConvert.SerializeObject(tweetData);

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            using HttpClient httpClient = new HttpClient(oauth, disposeHandler: true);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return "Tweet sent successfully";

            string body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Failed to send tweet. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        }


        private static async Task<string> UploadFileToXAsync(string APIKey, string APISecret, string AccessToken, string AccessTokenSecret, string uploadMediaEndpoint, Stream imageStream)
        {
            var oauth = new OAuthMessageHandler(APIKey, APISecret, AccessToken, AccessTokenSecret);
            if (imageStream.CanSeek)
            {
                imageStream.Position = 0;
            }

            string mediaType = "image/jpeg"; // or 'multipart/form-data'? 

            using var fileContent = new StreamContent(imageStream);
            fileContent.Headers.Add("Content-Type", mediaType);
            var multipartContent = new MultipartFormDataContent
            {
                    { fileContent, "media" }
             };

            var createUploadRequest = new HttpRequestMessage(HttpMethod.Post, uploadMediaEndpoint)
            {
                Content = multipartContent
            };

            using (var httpClient = new HttpClient(oauth))
            {
                var uploadResponse = await httpClient.SendAsync(createUploadRequest);
                if (uploadResponse.IsSuccessStatusCode)
                {
                    var responseContent = await uploadResponse.Content.ReadAsStringAsync();

                    TwitterMediaUploadResponse deserializedResponse = JsonConvert.DeserializeObject<TwitterMediaUploadResponse>(responseContent);
                    return deserializedResponse?.MediaIdString
                        ?? throw new Exception("Failed to get media ID from Twitter response");
                }
                else
                {
                    return null;
                }
            }
        }

        private void ValidateImageFields(Stream? fileImage, string? imageUrl)
        {
            bool hasFile = fileImage != null && fileImage.CanRead && fileImage.Length > 0;
            bool hasUrl = !string.IsNullOrWhiteSpace(imageUrl);

            if (hasFile && hasUrl)
            {
                throw new ArgumentException("Provide either an image file or an image URL, not both.");
            }

            if (hasUrl && !IsUrl(imageUrl))
            {
                throw new ArgumentException("Input is not a valid URL");
            }
        }

        private static async Task<Stream> GetImageStreamAsync(string input)
        {

            var client = new HttpClient();
            return await client.GetStreamAsync(input);

        }

        private static bool IsUrl(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

    }
}
