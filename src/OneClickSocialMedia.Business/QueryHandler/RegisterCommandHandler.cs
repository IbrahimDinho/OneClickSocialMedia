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

        public RegisterCommandHandler(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Users users = new Users
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email,
            };


            IdentityResult result = await userManager.CreateAsync(users, request.Password);

            if (result.Succeeded)
            {
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

