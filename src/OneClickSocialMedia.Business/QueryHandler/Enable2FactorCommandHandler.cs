using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class Enable2FactorCommandHandler : IRequestHandler<Enable2FactorCommand, Enable2FactorCommandResponse>
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Enable2FactorCommandHandler(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Enable2FactorCommandResponse> Handle(Enable2FactorCommand request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                return new Enable2FactorCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Unable to access user context." }
                };
            }

            var user = await userManager.GetUserAsync(httpContext.User);

            if (user == null)
            {
                return new Enable2FactorCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "User not found." }
                };
            }

            var result = await userManager.SetTwoFactorEnabledAsync(user, request.Enable);

            if (!result.Succeeded)
            {
                return new Enable2FactorCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await signInManager.RefreshSignInAsync(user);

            return new Enable2FactorCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}