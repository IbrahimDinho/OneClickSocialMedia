using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Web.ViewModel
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Name of user
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Email Address of user
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40,MinimumLength = 6, ErrorMessage = "The {0} must be at {2} and at max {1} characters long")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; }

        /// <summary>
        /// Password of user confirm
        /// </summary>
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
