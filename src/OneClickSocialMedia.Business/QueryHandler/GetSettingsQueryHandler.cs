using MediatR;
using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Business.Service;
using OneClickSocialMedia.Constants;
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

            InstagramOAuthTokens instagramToken = await context.InstagramOAuthTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            FacebookOAuthTokens facebookToken = await context.FacebookOAuthTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (twitterToken == null && instagramToken == null && facebookToken == null)
            {
                return new GetSettingsResponse
                {
                    IsSuccess = true
                };
            }

            GetSettingsResponse response = new GetSettingsResponse();

            if (twitterToken != null)
            {
                string decryptedApiSecret = string.IsNullOrWhiteSpace(twitterToken.TwitterApiSecret)
               ? string.Empty
               : encryptionService.Decrypt(TwitterEndpoints.Provider, twitterToken.TwitterApiSecret);

                string decryptedAccessTokenSecret = string.IsNullOrWhiteSpace(twitterToken.TwitterAccessTokenSecret)
                    ? string.Empty
                    : encryptionService.Decrypt(TwitterEndpoints.Provider, twitterToken.TwitterAccessTokenSecret);

                PopulateTwitterSettings(response, twitterToken, decryptedApiSecret, decryptedAccessTokenSecret);

            }

            if (instagramToken != null)
            {
                string decryptedAccessToken = string.IsNullOrWhiteSpace(instagramToken.AccessToken)
               ? string.Empty
               : encryptionService.Decrypt(InstagramEndpoints.Provider, instagramToken.AccessToken);

                PopulateInstagramSettings(response, instagramToken, decryptedAccessToken);

            }

            if (facebookToken != null)
            {
                string decryptedAccessToken = string.IsNullOrWhiteSpace(facebookToken.UserAccessToken)
               ? string.Empty
               : encryptionService.Decrypt(FacebookEndpoints.Provider, facebookToken.UserAccessToken);

                PopulateFacebookSettings(response, facebookToken, decryptedAccessToken);

            }


            return response;


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

        private void PopulateTwitterSettings(
        GetSettingsResponse response,
        TwitterOAuthTokens twitterToken,
        string decryptedApiSecret,
        string decryptedAccessTokenSecret)
        {
            response.IsSuccess = true;

            response.TwitterApiKey = MaskValue(twitterToken.TwitterApiKey);
            response.TwitterAccessToken = MaskValue(twitterToken.TwitterAccessToken);

            response.HasTwitterApiSecret = !string.IsNullOrWhiteSpace(decryptedApiSecret);
            response.HasTwitterAccessTokenSecret = !string.IsNullOrWhiteSpace(decryptedAccessTokenSecret);
        }

        private void PopulateInstagramSettings(
        GetSettingsResponse response,
        InstagramOAuthTokens instagramToken,
        string decryptedAccessToken)
        {
            response.IsSuccess = true;

            response.HasInstagramAccessToken = !string.IsNullOrWhiteSpace(decryptedAccessToken);
        }

        private void PopulateFacebookSettings(
        GetSettingsResponse response,
        FacebookOAuthTokens facebookToken,
        string decryptedAccessToken)
        {
            response.IsSuccess = true;

            response.FacebookPageId = MaskValue(facebookToken.PageId);

            response.HasFacebookAccessToken = !string.IsNullOrWhiteSpace(decryptedAccessToken);
        }

    }


}

