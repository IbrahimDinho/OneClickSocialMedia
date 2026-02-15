using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Business.Service;
using OneClickSocialMedia.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, GetSettingsResponse>
    {
        private readonly EncryptionService encryptionService;
        private readonly AppDbContext context;

        public GetSettingsQueryHandler(EncryptionService encryptionService, AppDbContext context) { 
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
                TwitterApiKey = twitterToken.TwitterApiKey,
                TwitterAccessToken = twitterToken.TwitterAccessToken,

                HasTwitterApiSecret = !string.IsNullOrWhiteSpace(decryptedApiSecret),
                HasTwitterAccessTokenSecret = !string.IsNullOrWhiteSpace(decryptedAccessTokenSecret),

            };

        }
    }
}
