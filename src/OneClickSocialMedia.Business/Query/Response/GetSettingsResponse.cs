namespace OneClickSocialMedia.Business.Query.Response
{
    public class GetSettingsResponse : Response
    {
        #region Twitter
        public string TwitterApiKey { get; set; }

        public string TwitterApiSecret { get; set; }

        public string TwitterAccessToken { get; set; }

        public string TwitterAccessTokenSecret { get; set; }

        public bool HasTwitterAccessTokenSecret { get; set; }

        public bool HasTwitterApiSecret { get; set; }
        #endregion Twitter

        #region Instagram
        public string InstagramAccessToken { get; set; }

        public bool HasInstagramAccessToken { get; set; }
        #endregion Instagram

        #region Facebook
        public string FacebookAccessToken { get; set; }

        public bool HasFacebookAccessToken { get; set; }
        #endregion Facebook
    }
}

