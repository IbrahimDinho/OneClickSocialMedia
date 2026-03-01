using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;

        public RegisterCommandHandler(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Users user = new Users
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true,
            };


            IdentityResult result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                RegisterResponse response = new RegisterResponse();
                response.IsSuccess = true;
                return response;
            }
            else
            {
                RegisterResponse response = new RegisterResponse();
                response.IsSuccess = false;
                response.ErrorMessages = result.Errors.Select(x => x.Description).ToList();
                return response;
            }
        }
    }
}

