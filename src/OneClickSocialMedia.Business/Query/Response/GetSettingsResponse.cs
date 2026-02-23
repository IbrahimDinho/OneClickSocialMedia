namespace OneClickSocialMedia.Business.Query.Response
{
    public class GetSettingsResponse : Response
    {
        public string TwitterApiKey { get; set; }

        public string TwitterApiSecret { get; set; }

        public string TwitterAccessToken { get; set; }

        public string TwitterAccessTokenSecret { get; set; }

        public bool HasTwitterAccessTokenSecret { get; set; }

        public bool HasTwitterApiSecret { get; set; }
    }
}

