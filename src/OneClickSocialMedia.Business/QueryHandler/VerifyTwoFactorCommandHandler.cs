using MediatR;
using Microsoft.AspNetCore.Identity;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Data;

public class VerifyTwoFactorCommandHandler : IRequestHandler<VerifyTwoFactorCommand, VerifyTwoFactorCommandResponse>
{
    private readonly SignInManager<Users> signInManager;

    public VerifyTwoFactorCommandHandler(SignInManager<Users> signInManager)
    {
        this.signInManager = signInManager;
    }

    public async Task<VerifyTwoFactorCommandResponse> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
    {
        var result = await signInManager.TwoFactorSignInAsync(
            request.Provider,
            request.Code,
            isPersistent: request.RememberMe,
            rememberClient: request.RememberMe
        );

        if (result.Succeeded)
        {
            return new VerifyTwoFactorCommandResponse { IsSuccess = true };
        }

        if (result.IsLockedOut)
        {
            return new VerifyTwoFactorCommandResponse { IsSuccess = false, ErrorMessages = new List<string> { "The account is locked out." } };
        }

        return new VerifyTwoFactorCommandResponse { IsSuccess = false, ErrorMessages = new List<string> { "Invalid verification code." } };
    }
}