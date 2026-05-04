using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class PostToSettingsCommand : IRequest<PostToSettingsResponse>
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Twitter Api Key
        /// </summary>
        public string TwitterApiKey { get; set; }

        /// <summary>
        /// Twitter Api Secret
        /// </summary>
        public string TwitterApiSecret { get; set; }

        /// <summary>
        /// Twitter Access Token
        /// </summary>
        public string TwitterAccessToken { get; set; }

        /// <summary>
        /// Twitter Token Secret
        /// </summary>
        public string TwitterAccessTokenSecret { get; set; }

        /// <summary>
        /// Instagram Token Secret
        /// </summary>
        public string InstagramAccessToken { get; set; }

        public bool UpdateTwitterCredentials { get; set; }

        public bool UpdateInstagramCredentials { get; set; }

        /// <summary>
        /// Facebook Token Secret
        /// </summary>
        public string FacebookAccessToken { get; set; }
        /// <summary>
        /// Facebook Page Id
        /// </summary>
        public string FacebookPageId { get; set; }
        public bool HasFacebookAccessToken { get; set; }

        public bool UpdateFacebookCredentials { get; set; }
    }
}
