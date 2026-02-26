using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery, ChangePasswordResponse>
    {
        private readonly SignInManager<Users> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChangePasswordQueryHandler(
            SignInManager<Users> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                return new ChangePasswordResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Unable to access current user context."
                };
            }

            var user = await signInManager.UserManager.GetUserAsync(httpContext.User);

            if (user == null)
            {
                return new ChangePasswordResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "User not found."
                };
            }

            return new ChangePasswordResponse
            {
                IsSuccess = true,
                Email = user.Email,
            };
        }
    }
}