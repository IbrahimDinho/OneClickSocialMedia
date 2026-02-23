using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class ResetPasswordQuery : IRequest<ResetPasswordResponse>
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The new password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The confirmed new password
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// The token associated with the user to identify them
        /// </summary>
        public string Token { get; set; }

    }
}

