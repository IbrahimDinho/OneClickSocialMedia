namespace OneClickSocialMedia.Web.ViewModel
{
    public class SocialMediaViewModel
    {

        /// <summary>
        /// If facebook checkbox has been checked by user
        /// </summary>
        public bool IsFaceBook { get; set; }

        /// <summary>
        /// If twitter checkbox has been checked by user
        /// </summary>
        public bool IsTwitter { get; set; }

        /// <summary>
        /// If instagram checkbox has been checked by user
        /// </summary>
        public bool IsInstagram { get; set; }

        /// <summary>
        /// Url of image that the user wants to post
        /// </summary>
        public string URLforImage { get; set; }

        /// <summary>
        /// comment posted by the user.
        /// </summary>
        public string Comment { get; set; }

    }
}
