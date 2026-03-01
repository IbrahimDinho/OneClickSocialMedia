using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Web.ViewModel
{
    public class LoginViewModel
    {

        /// <summary>
        /// Email Address
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// Remember me checkbox 
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// If 2 factor authentication has been enabled
        /// </summary>
        public bool RequiresTwoFactor { get; set; }

        /// <summary>
        /// 2 factor provider, so far only implemented email
        /// </summary>
        public string TwoFactorProvider { get; set; }

        /// <summary>
        /// The 2 factor code from the user to authenticate them.
        /// </summary>
        public string TwoFactorCode { get; set; }

    }
}
