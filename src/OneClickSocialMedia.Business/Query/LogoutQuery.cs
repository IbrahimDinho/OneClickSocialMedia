using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class LogoutQuery : IRequest<LogoutResponse>
    {
    }
}
