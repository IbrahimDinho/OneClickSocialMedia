using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class GetSettingsQuery : IRequest<GetSettingsResponse>
    {
        /// <summary>
        /// The current user id
        /// </summary>
        public string UserId { get; set; }
    }
}

