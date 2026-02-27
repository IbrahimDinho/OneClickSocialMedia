using Microsoft.AspNetCore.Identity;

namespace OneClickSocialMedia.Business.Identity
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The password is empty and must contain charachters",
                    Code = "PasswordIsEmpty"
                });
            }

            var username = await manager.GetUserNameAsync(user);
            if (string.Equals(username, password, StringComparison.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Username and password can't be the same.",
                    Code = "SameUserPass"
                });
            }

            if (password.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The word password is not allowed to be used for the password.",
                    Code = "PasswordContainsPassword"
                });
            }

            return IdentityResult.Success;

        }
    }
}
