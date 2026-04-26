using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Contract;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.Service
{
    public class CredentialsProvider : ICredentialsProvider
    {

        private readonly AppDbContext context;
        private readonly IEncryptionService encryptionService;


        public CredentialsProvider(AppDbContext context, IEncryptionService encryptionService)
        {
            this.context = context;
            this.encryptionService = encryptionService;
        }


        /// <inheritdoc/>
        public Task<TwitterCredentialsDto> GetTwitterCredsUserAsync(Guid userId, CancellationToken ct = default)
        {
            return context.TwitterOAuthTokens
                 .Where(x => x.UserId == userId.ToString())
                 .Select(x => new TwitterCredentialsDto
                 {
                     ApiKey = x.TwitterApiKey,
                     ApiSecret = encryptionService.Decrypt("Twitter", x.TwitterApiSecret),
                     AccessToken = x.TwitterAccessToken,
                     AccessTokenSecret = encryptionService.Decrypt("Twitter", x.TwitterAccessTokenSecret),
                 })
                 .FirstOrDefaultAsync(ct);
        }
    }
}
