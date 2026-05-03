namespace OneClickSocialMedia.Constants
{
    public static class FacebookEndpoints
    {
        /// <summary>
        /// Endpoint used to post on facebook base URL 
        /// </summary>
        public const string FacebookBaseURL = "https://graph.facebook.com/";

        /// <summary>
        /// Provider constant for Facebook.
        /// </summary>
        public const string Provider = "Facebook";


        /// <summary>
        /// part of the Endpoint to get the pageid and page access token
        /// </summary>
        public const string FacebookMe = "https://graph.facebook.com/me/accounts?access_token=";

    }
}
