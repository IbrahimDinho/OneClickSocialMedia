using MediatR;
using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Business.Service;
using OneClickSocialMedia.Constants;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class PostToSettingsCommandHandler : IRequestHandler<PostToSettingsCommand, PostToSettingsResponse>
    {
        private readonly IEncryptionService encryptionService;
        private readonly AppDbContext context;

        public PostToSettingsCommandHandler(IEncryptionService encryptionService, AppDbContext context)
        {
            this.encryptionService = encryptionService;
            this.context = context;
        }


        public async Task<PostToSettingsResponse> Handle(PostToSettingsCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateTwitterCredentials)
            {
                await SaveOrUpdateTwitterTokensAsync(request, cancellationToken);
            }
            if (request.UpdateInstagramCredentials)
            {
                await SaveOrUpdateInstagramTokensAsync(request, cancellationToken);
            }

            PostToSettingsResponse response = new PostToSettingsResponse
            {
                IsSuccess = true,
            };

            return response;

        }


        private async Task SaveOrUpdateTwitterTokensAsync(
        PostToSettingsCommand request,
        CancellationToken cancellationToken)
        {
            TwitterOAuthTokens existing = await context.TwitterOAuthTokens
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            string? encryptedTwitterAPISecret = null;
            string? encryptedTwitterAccessTokenSecret = null;

            if (!string.IsNullOrWhiteSpace(request.TwitterApiSecret))
            {
                encryptedTwitterAPISecret = encryptionService.Encrypt(
                    TwitterEndpoints.Provider,
                    request.TwitterApiSecret);
            }

            if (!string.IsNullOrWhiteSpace(request.TwitterAccessTokenSecret))
            {
                encryptedTwitterAccessTokenSecret = encryptionService.Encrypt(
                    TwitterEndpoints.Provider,
                    request.TwitterAccessTokenSecret);
            }

            if (existing == null)
            {
                // Create new record
                TwitterOAuthTokens twitterToken = new TwitterOAuthTokens
                {
                    TwitterApiKey = request.TwitterApiKey,
                    TwitterApiSecret = encryptedTwitterAPISecret,
                    TwitterAccessToken = request.TwitterAccessToken,
                    TwitterAccessTokenSecret = encryptedTwitterAccessTokenSecret,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId,
                };

                context.TwitterOAuthTokens.Add(twitterToken);
            }
            else
            {
                // Update existing record
                existing.TwitterApiKey = request.TwitterApiKey;
                existing.TwitterAccessToken = request.TwitterAccessToken;

                if (!string.IsNullOrWhiteSpace(encryptedTwitterAPISecret))
                    existing.TwitterApiSecret = encryptedTwitterAPISecret;

                if (!string.IsNullOrWhiteSpace(encryptedTwitterAccessTokenSecret))
                    existing.TwitterAccessTokenSecret = encryptedTwitterAccessTokenSecret;
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveOrUpdateInstagramTokensAsync(
        PostToSettingsCommand request,
        CancellationToken cancellationToken)
        {
            var existing = await context.InstagramOAuthTokens
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            string? encryptedAccessToken = null;

            if (!string.IsNullOrWhiteSpace(request.InstagramAccessToken))
            {
                encryptedAccessToken = encryptionService.Encrypt(
                    InstagramEndpoints.Provider,
                    request.InstagramAccessToken);
            }

            if (existing == null)
            {
                // Create new record
                InstagramOAuthTokens instagramToken = new InstagramOAuthTokens
                {
                    AccessToken = encryptedAccessToken,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId,
                };

                context.InstagramOAuthTokens.Add(instagramToken);
            }
            else
            {
                // Update existing record
                if (!string.IsNullOrWhiteSpace(encryptedAccessToken))
                    existing.AccessToken = encryptedAccessToken;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
