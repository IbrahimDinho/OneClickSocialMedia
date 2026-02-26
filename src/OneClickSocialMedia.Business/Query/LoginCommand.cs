using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        /// <summary>
        /// Email Address of user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Remember me for the user.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}

