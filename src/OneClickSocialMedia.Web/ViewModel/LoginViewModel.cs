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

    }
}
