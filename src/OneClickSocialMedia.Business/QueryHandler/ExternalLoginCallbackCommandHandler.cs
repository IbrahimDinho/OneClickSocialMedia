using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;
using System.Security.Claims;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class ExternalLoginCallbackCommandHandler : IRequestHandler<ExternalLoginCallbackCommand, ExternalLoginCallbackCommandResponse>
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public ExternalLoginCallbackCommandHandler(
            SignInManager<Users> signInManager,
            UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<ExternalLoginCallbackCommandResponse> Handle(ExternalLoginCallbackCommand request, CancellationToken cancellationToken)
        {
            ExternalLoginCallbackCommandResponse response = new ExternalLoginCallbackCommandResponse
            {
                ReturnUrl = string.IsNullOrWhiteSpace(request.ReturnUrl) ? "/" : request.ReturnUrl
            };

            if (!string.IsNullOrWhiteSpace(request.RemoteError))
            {
                response.ErrorMessages.Add($"External provider error: {request.RemoteError}");
                return response;
            }

            ExternalLoginInfo? info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                response.ErrorMessages.Add("Could not load external login information.");
                return response;
            }

            // 1. If login is already linked, sign in directly
            SignInResult externalLoginResult = await signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: true,
                bypassTwoFactor: true);

            if (externalLoginResult.Succeeded)
            {
                response.IsSuccess = true;
                return response;
            }

            // 2. Get email and name
            string? email = info.Principal.FindFirstValue(ClaimTypes.Email)
                            ?? info.Principal.FindFirstValue("email");

            string displayName = GetDisplayName(info.Principal, email);

            if (string.IsNullOrWhiteSpace(email))
            {
                response.ErrorMessages.Add("Email claim was not supplied by Google.");
                return response;
            }

            // 3. Find existing local user by email
            Users? user = await userManager.FindByEmailAsync(email);

            // 4. Create user if missing
            if (user == null)
            {
                user = new Users
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Name = displayName,
                };

                var createResult = await userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    foreach (var error in createResult.Errors)
                    {
                        response.ErrorMessages!.Add(error.Description);
                    }

                    return response;
                }

            }

            // 5. Link login to local user
            var addLoginResult = await userManager.AddLoginAsync(user, info);

            if (!addLoginResult.Succeeded)
            {
                foreach (var error in addLoginResult.Errors)
                {
                    response.ErrorMessages.Add(error.Description);
                }

                return response;
            }

            // 6. Sign in the local Identity user
            await signInManager.SignInAsync(user, isPersistent: true);

            response.IsSuccess = true;
            return response;
        }



        private string GetDisplayName(ClaimsPrincipal principal, string email)
        {
            string? fullName = principal.FindFirstValue("name");

            string? firstName = principal.FindFirstValue(ClaimTypes.GivenName)
                            ?? principal.FindFirstValue("given_name");

            string? lastName = principal.FindFirstValue(ClaimTypes.Surname)
                           ?? principal.FindFirstValue("family_name");

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                return fullName;
            }

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"{firstName} {lastName}";
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                return firstName;
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                return lastName;
            }

            return email;
        }
    }
}