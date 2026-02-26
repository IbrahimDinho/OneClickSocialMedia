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
            SignInResult result = await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);


            if (result.Succeeded)
            {
                LoginResponse response = new LoginResponse();
                response.IsSuccess = true;
                return response;
            }
            else
            {
                LoginResponse response = new LoginResponse();
                response.IsSuccess = false;
                response.ErrorMessage = "Email or password is incorrect.";
                return response;
            }
        }
    }
}
