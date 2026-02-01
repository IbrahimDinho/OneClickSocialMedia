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


    }
}
