using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class ExternalLoginCallbackCommand : IRequest<ExternalLoginCallbackCommandResponse>
    {
        /// <summary>
        /// The return url to go to after the provider callback
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Any error message returned by the external authentication provider during the callback
        /// </summary>
        public string RemoteError { get; set; }
    }
}

