using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Web.ViewModel
{
    public class SocialMediaSettingsViewModel
    {

        /// <summary>
        /// Twitter Api Key
        /// </summary>
        [Display(Name = "API Key")]
        public string TwitterApiKey { get; set; }

        /// <summary>
        /// Twitter Api Secret
        /// </summary>
        [Display(Name = "API Secret")]
        [DataType(DataType.Password)]
        public string TwitterApiSecret { get; set; }

        /// <summary>
        /// Twitter Access Token
        /// </summary>
        [Display(Name = "Access Token")]
        public string TwitterAccessToken { get; set; }

        /// <summary>
        /// Twitter Token Secret
        /// </summary>
        [Display(Name = "Access Token Secret")]
        [DataType(DataType.Password)]
        public string TwitterAccessTokenSecret { get; set; }

        public bool HasTwitterApiSecret { get; set; }
        public bool HasTwitterAccessTokenSecret { get; set; }

        public bool UpdateTwitterCredentials { get; set; }


        /// <summary>
        /// Instagram Access Token
        /// </summary>
        [Display(Name = "Access Token")]
        [DataType(DataType.Password)]
        public string InstagramAccessToken { get; set; }

        public bool HasInstagramAccessToken { get; set; }


        public bool UpdateInstagramCredentials { get; set; }

        /// <summary>
        /// Facebook Access Token
        /// </summary>
        [Display(Name = "Access Token")]
        [DataType(DataType.Password)]
        public string FacebookAccessToken { get; set; }

        /// <summary>
        /// Twitter Access Token
        /// </summary>
        [Display(Name = "Page ID")]
        public string FacebookPageId { get; set; }

        public bool HasFacebookAccessToken { get; set; }

        public bool UpdateFacebookCredentials { get; set; }

    }
}
