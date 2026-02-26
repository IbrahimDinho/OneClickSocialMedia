using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        private readonly SignInManager<Users> signInManager;

        public LogoutCommandHandler(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();

            return new LogoutResponse
            {
                IsSuccess = true,
            };
        }
    }
}
