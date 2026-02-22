using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, ResetPasswordResponse>
    {
        private readonly SignInManager<Users> signInManager;


        public ResetPasswordQueryHandler(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new ResetPasswordResponse
                {
                    IsSuccess = true
                };
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new ResetPasswordResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Passwords do not match." }
                };
            }

            var result = await signInManager.UserManager.ResetPasswordAsync(
                user,
                request.Token,
                request.Password
                );

            if (result.Succeeded)
            {
                return new ResetPasswordResponse
                {
                    IsSuccess = true
                };
            }

            return new ResetPasswordResponse
            {
                IsSuccess = false,
                ErrorMessages = result.Errors.Select(e => e.Description).ToList()
            };

        }
    }
}
