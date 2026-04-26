using MediatR;
using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Business.Service;
using OneClickSocialMedia.Data;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, GetSettingsResponse>
    {
        private readonly IEncryptionService encryptionService;
        private readonly AppDbContext context;

        public GetSettingsQueryHandler(IEncryptionService encryptionService, AppDbContext context)
        {
            this.encryptionService = encryptionService;
            this.context = context;
        }


        public async Task<GetSettingsResponse> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {

            TwitterOAuthTokens twitterToken = await context.TwitterOAuthTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (twitterToken == null)
            {
                return new GetSettingsResponse
                {
                    IsSuccess = true
                };
            }

            string decryptedApiSecret = string.IsNullOrWhiteSpace(twitterToken.TwitterApiSecret)
           ? string.Empty
           : encryptionService.Decrypt("Twitter", twitterToken.TwitterApiSecret);

            string decryptedAccessTokenSecret = string.IsNullOrWhiteSpace(twitterToken.TwitterAccessTokenSecret)
                ? string.Empty
                : encryptionService.Decrypt("Twitter", twitterToken.TwitterAccessTokenSecret);


            return new GetSettingsResponse
            {
                IsSuccess = true,
                TwitterApiKey = MaskValue(twitterToken.TwitterApiKey),
                TwitterAccessToken = MaskValue(twitterToken.TwitterAccessToken),

                HasTwitterApiSecret = !string.IsNullOrWhiteSpace(decryptedApiSecret),
                HasTwitterAccessTokenSecret = !string.IsNullOrWhiteSpace(decryptedAccessTokenSecret),

            };

        }

        private static string MaskValue(string value, int visibleChars = 6)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Length <= visibleChars)
                return value;

            var lastChars = value.Substring(value.Length - visibleChars);
            return new string('*', value.Length - visibleChars) + lastChars;
        }
    }


}

