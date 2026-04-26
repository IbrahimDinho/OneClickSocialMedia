using Microsoft.AspNetCore.DataProtection;

namespace OneClickSocialMedia.Business.Service
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtectionProvider provider;

        public EncryptionService(IDataProtectionProvider provider)
        {
            this.provider = provider;
        }

        public string Encrypt(string providerName, string value)
        {
            IDataProtector protector = provider.CreateProtector($"OAuthTokens.{providerName}");
            return protector.Protect(value);
        }

        public string Decrypt(string providerName, string value)
        {
            IDataProtector protector = provider.CreateProtector($"OAuthTokens.{providerName}");
            return protector.Unprotect(value);
        }
    }
}
