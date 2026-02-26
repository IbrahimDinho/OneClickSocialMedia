using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChangePasswordCommandHandler(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return new ChangePasswordCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Passwords do not match." }
                };
            }

            var http = httpContextAccessor.HttpContext;
            if (http == null)
            {
                return new ChangePasswordCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Something went wrong. Try again." }
                };
            }

            var user = await userManager.GetUserAsync(http.User);
            if (user == null)
            {
                return new ChangePasswordCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "User not found." }
                };
            }

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                return new ChangePasswordCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessages = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await signInManager.RefreshSignInAsync(user);

            return new ChangePasswordCommandResponse { IsSuccess = true };
        }
    }
}