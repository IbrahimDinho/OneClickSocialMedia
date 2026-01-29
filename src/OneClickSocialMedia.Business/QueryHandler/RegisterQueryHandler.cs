using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class RegisterQueryHandler : IRequestHandler<RegisterQuery, RegisterResponse>
    {
        private readonly UserManager<Users> userManager;

        public RegisterQueryHandler(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<RegisterResponse> Handle(RegisterQuery request, CancellationToken cancellationToken)
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
                response.ErrorMessage = result.Errors.Select(x => x.Description).ToList();
                return response;
            }
        }
    }
}
