using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class LogoutCommand : IRequest<LogoutResponse>
    {
    }
}
