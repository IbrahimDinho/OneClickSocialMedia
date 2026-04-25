namespace OneClickSocialMedia.Constants
{
    public static class TwitterEndpoints
    {
        /// <summary>
        /// Endpoint used to create and manage tweets via the Twitter API v2.
        /// </summary>
        public const string Tweet = "https://api.twitter.com/2/tweets";

        /// <summary>
        /// Endpoint used to upload media (images) to Twitter before attaching to a tweet.
        /// </summary>
        public const string MediaUpload = "https://upload.twitter.com/1.1/media/upload.json";
    }
}
