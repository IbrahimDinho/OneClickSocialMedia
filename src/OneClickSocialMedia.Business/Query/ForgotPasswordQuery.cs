using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class ForgotPasswordQuery : IRequest<ForgotPasswordResponse>
    {
        /// <summary>
        /// The purpoted user email.
        /// </summary>
        public string Email { get; set; }
    }
}

