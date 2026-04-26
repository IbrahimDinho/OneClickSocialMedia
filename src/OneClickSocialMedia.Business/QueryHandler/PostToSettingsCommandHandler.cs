using MediatR;
using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Business.Service;
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
            TwitterOAuthTokens existing = await context.TwitterOAuthTokens
        .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            string encryptedTwitterAPISecret = string.Empty;
            string encryptedTwitterAccessTokenSecret = string.Empty;

            if (!string.IsNullOrWhiteSpace(request.TwitterApiSecret))
            {
                encryptedTwitterAPISecret = encryptionService.Encrypt("Twitter", request.TwitterApiSecret);
            }
            if (!string.IsNullOrWhiteSpace(request.TwitterAccessTokenSecret))
            {
                encryptedTwitterAccessTokenSecret = encryptionService.Encrypt("Twitter", request.TwitterAccessTokenSecret);
            }

            if (existing == null)
            {
                //Create a new record

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
                //Update record 

                existing.TwitterApiKey = request.TwitterApiKey;
                existing.TwitterAccessToken = request.TwitterAccessToken;

                if (encryptedTwitterAPISecret != null)
                    existing.TwitterApiSecret = encryptedTwitterAPISecret;

                if (encryptedTwitterAccessTokenSecret != null)
                    existing.TwitterAccessTokenSecret = encryptedTwitterAccessTokenSecret;
            }

            await context.SaveChangesAsync();

            PostToSettingsResponse response = new PostToSettingsResponse
            {
                IsSuccess = true,
            };

            return response;

        }
    }
}
