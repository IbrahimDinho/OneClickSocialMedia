using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly SignInManager<Users> signInManager;

        public LoginCommandHandler(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            const string InvalidCredentials = "Email or password is incorrect.";
            const string LockedOutMessage = "The account is locked out.";

            var user = await signInManager.UserManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new LoginResponse { IsSuccess = false, ErrorMessage = InvalidCredentials };
            }

            if (await signInManager.UserManager.IsLockedOutAsync(user))
            {
                return new LoginResponse { IsSuccess = false, ErrorMessage = LockedOutMessage };
            }

            SignInResult result = await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, true);

            if (result.Succeeded)
            {
                return new LoginResponse { IsSuccess = true };
            }
            else if (result.IsLockedOut)
            {
                return new LoginResponse { IsSuccess = false, ErrorMessage = LockedOutMessage };
            }
            else
            {
                return new LoginResponse { IsSuccess = false, ErrorMessage = InvalidCredentials };
            }

        }
    }
}
