namespace OneClickSocialMedia.Business.Query.Response
{
    public class LoginResponse : Response
    {
        /// <summary>
        /// wether to show 2 factor authentication screen
        /// </summary>
        public bool ShouldPromptEnableTwoFactor { get; set; }

        /// <summary>
        /// If 2 factor authentication has been enabled
        /// </summary>
        public bool RequiresTwoFactor { get; set; }
        /// <summary>
        /// 2 factor provider, so far only implemented email
        /// </summary>
        public string TwoFactorProvider { get; set; }

        /// <summary>
        /// If user wants to remember me set when logs in.
        /// </summary>
        public bool RememberMe { get; set; }


    }
}
