using Newtonsoft.Json;
using OneClickSocialMedia.Constants;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
using System.Text;


namespace OneClickSocialMedia.Business.Service
{
    public class InstagramPostService : IInstagramPostService
    {
        private readonly HttpClient client;

        public InstagramPostService(HttpClient client)
        {
            this.client = client;
        }


        /// <inheritdoc/>
        public async Task<string> PostAsync(string commentToPost, string? imageUrl, InstagramCredentialsDto instagramCredentialsDto)
        {

            string instagramUserId = await GetUserIdInstagram(instagramCredentialsDto.AccessToken);
            HttpResponseMessage mediaContainer = await CreateMediaContainerAsync(commentToPost, imageUrl, instagramUserId, instagramCredentialsDto.AccessToken);

            if (!mediaContainer.IsSuccessStatusCode)
            {
                string body = await mediaContainer.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to create Instagram container. Status={(int)mediaContainer.StatusCode} {mediaContainer.ReasonPhrase}. Body: {body}");
            }

            string containerContent = await mediaContainer.Content.ReadAsStringAsync();
            string containerId = JsonConvert.DeserializeObject<dynamic>(containerContent).id.ToString();

            HttpResponseMessage response = PostToInstagram(containerId, instagramUserId, instagramCredentialsDto.AccessToken);
            if (response.IsSuccessStatusCode)
            {

                return "Instagram post sent successfully";
            }
            else
            {
                string body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to send Instagram post. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
            }
        }



        private HttpResponseMessage PostToInstagram(string containerId, string instaUserId, string accessToken)
        {
            string apiEndPoint = $"{instaUserId}/media_publish?access_token={accessToken}";

            var postData = new
            {
                creation_id = containerId
            };

            string jsonString = JsonConvert.SerializeObject(postData);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{InstagramEndpoints.Insta}{apiEndPoint}", content).Result;
            return response;
        }


        private async Task<string> GetUserIdInstagram(string instagramAccessToken)
        {
            string url = $"{InstagramEndpoints.InstagramMe}?fields=id,username&access_token={instagramAccessToken}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Failed to get Instagram user id. Status={(int)response.StatusCode} {response.ReasonPhrase}. Body: {responseContent}");
                }
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return jsonResponse.id.ToString();

            }
            catch (Exception ex)
            {
                throw new HttpRequestException(
                    $"Failed to call Instagram user id endpoint. {ex.Message}", ex);
            }
        }


        private async Task<HttpResponseMessage> CreateMediaContainerAsync(string captionInsta, string imageUrl, string instaUserId, string accessToken)
        {
            string uri = InstagramEndpoints.Insta + instaUserId + "/media?access_token=" + accessToken;
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            List<KeyValuePair<string, string>> collection = [new KeyValuePair<string, string>("image_url", imageUrl)];
            if (!string.IsNullOrEmpty(captionInsta))
            {
                collection.Add(new KeyValuePair<string, string>("caption", captionInsta));
            }
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);

            return response;


        }


    }
}
