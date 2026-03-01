using EmailService;
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
        private readonly IEmailSender emailSender;

        public LoginCommandHandler(SignInManager<Users> signInManager, IEmailSender emailSender)
        {
            this.signInManager = signInManager;
            this.emailSender = emailSender;
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
                return new LoginResponse { IsSuccess = true, ShouldPromptEnableTwoFactor = !user.TwoFactorEnabled };
            }
            if (result.IsLockedOut)
            {
                return new LoginResponse { IsSuccess = false, ErrorMessage = LockedOutMessage };
            }
            if (result.RequiresTwoFactor)
            {
                return await GenerateOTPFor2Factor(user, request.RememberMe);
            }


            return new LoginResponse { IsSuccess = false, ErrorMessage = InvalidCredentials };

        }

        private async Task<LoginResponse> GenerateOTPFor2Factor(Users user, bool rememberMe)
        {
            var providers = await signInManager.UserManager.GetValidTwoFactorProvidersAsync(user);
            var emailProvider = TokenOptions.DefaultEmailProvider;

            if (!providers.Contains(emailProvider))
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Email 2FA is not available for this account."
                };
            }

            // Generate OTP
            string code = await signInManager.UserManager.GenerateTwoFactorTokenAsync(user, emailProvider);

            var message = new Message(
            new[] { user.Email! },
            "[OneClickSocialMedia] Your Login Verification Code",
            $"Your two-factor authentication code is: {code}"
            );

            await emailSender.SendEmailAsyncOTPCode(message);

            return new LoginResponse
            {
                IsSuccess = true,
                RequiresTwoFactor = true,
                TwoFactorProvider = emailProvider,
                RememberMe = rememberMe
            };
        }
    }
}
