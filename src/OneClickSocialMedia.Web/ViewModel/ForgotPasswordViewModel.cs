using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Web.ViewModel
{
    public class ForgotPasswordViewModel
    {

        /// <summary>
        /// Email Address
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
