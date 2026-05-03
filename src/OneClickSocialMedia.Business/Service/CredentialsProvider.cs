using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Constants;
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
                     ApiSecret = encryptionService.Decrypt(TwitterEndpoints.Provider, x.TwitterApiSecret),
                     AccessToken = x.TwitterAccessToken,
                     AccessTokenSecret = encryptionService.Decrypt(TwitterEndpoints.Provider, x.TwitterAccessTokenSecret),
                 })
                 .FirstOrDefaultAsync(ct);
        }

        /// <inheritdoc/>
        public Task<InstagramCredentialsDto> GetInstagramCredsUserAsync(Guid userId, CancellationToken ct = default)
        {
            return context.InstagramOAuthTokens
                 .Where(x => x.UserId == userId.ToString())
                 .Select(x => new InstagramCredentialsDto
                 {
                     AccessToken = encryptionService.Decrypt(InstagramEndpoints.Provider, x.AccessToken),
                 })
                 .FirstOrDefaultAsync(ct);
        }

        /// <inheritdoc/>
        public Task<FacebookCredentialsDto> GetFacebookCredsUserAsync(Guid userId, CancellationToken ct = default)
        {
            return context.FacebookOAuthTokens
                 .Where(x => x.UserId == userId.ToString())
                 .Select(x => new FacebookCredentialsDto
                 {
                     AccessToken = encryptionService.Decrypt(FacebookEndpoints.Provider, x.UserAccessToken),
                 })
                 .FirstOrDefaultAsync(ct);
        }
    }
}
