using EmailService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;
using System.Text;
using IEmailSender = EmailService.IEmailSender;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordQuery, ForgotPasswordResponse>
    {
        private readonly SignInManager<Users> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailSender emailSender;


        public ForgotPasswordHandler(SignInManager<Users> signInManager, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.emailSender = emailSender;
        }

        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(request.Email);

            // Do NOT reveal whether the account exists or is confirmed so its still a success
            if (user == null || !await signInManager.UserManager.IsEmailConfirmedAsync(user))
            {
                return new ForgotPasswordResponse
                {
                    IsSuccess = true
                };
            }

            // Generate token
            string token = await signInManager.UserManager.GeneratePasswordResetTokenAsync(user);

            // Encode token for URL
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // Build reset link
            var http = httpContextAccessor.HttpContext;
            string resetLink = $"{http!.Request.Scheme}://{http.Request.Host}/Account/ResetPassword?email={Uri.EscapeDataString(user.Email!)}&token={encodedToken}";

            // Send email (message.Content will be the reset link)
            var message = new Message(
                new[] { user.Email! },
                "[OneClickSocialMedia] Password Reset",
                resetLink
            );

            await emailSender.SendEmailAsync(message);

            return new ForgotPasswordResponse
            {
                IsSuccess = true
            };
        }
    }
}

