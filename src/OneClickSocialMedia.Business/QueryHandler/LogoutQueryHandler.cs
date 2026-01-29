using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class LogoutQueryHandler : IRequestHandler<LogoutQuery, LogoutResponse>
    {
        private readonly SignInManager<Users> signInManager;

        public LogoutQueryHandler(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<LogoutResponse> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
                await signInManager.SignOutAsync();

                return new LogoutResponse
                {
                    IsSuccess = true,
                    ErrorMessage = null
                };
        }
    }
}
