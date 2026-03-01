using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class VerifyTwoFactorCommand : IRequest<VerifyTwoFactorCommandResponse>
    {
        /// <summary>
        /// The two-factor authentication provider used to validate the verification code
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// The one-time verification code sent to the user for two-factor authentication
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Indicates whether the authentication session should be persisted across browser sessions
        /// </summary>
        public bool RememberMe { get; set; }

    }
}

