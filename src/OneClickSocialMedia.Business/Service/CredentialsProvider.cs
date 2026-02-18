using Microsoft.AspNetCore.DataProtection;
using OneClickSocialMedia.Contract;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
using OneClickSocialMedia.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OneClickSocialMedia.Business.Service
{
    public class CredentialsProvider : ICredentialsProvider
    {
        private string XapiKey;
        private string XapiSecret;
        private string Xaccesstoken;
        private string Xtokendecret;
        private const string Xendpoint = "https://api.twitter.com/2/tweets";
        private const string Xmediaendpoint = "https://upload.twitter.com/1.1/media/upload.json";

        private readonly AppDbContext context;

        public CredentialsProvider(AppDbContext context)
        {
            this.context = context;
        }


        /// <inheritdoc/>
        public Task<TwitterCredentialsDto> GetTwitterCredsUserAsync(Guid userId, CancellationToken ct = default)
        {
            return context.TwitterOAuthTokens
                 .Where(x => x.UserId == userId.ToString())
                 .Select(x => new TwitterCredentialsDto
                 {
                     ApiKey = x.TwitterApiKey,
                     ApiSecret = x.TwitterApiSecret,
                     AccessToken = x.TwitterAccessToken,
                     AccessTokenSecret = x.TwitterAccessTokenSecret,
                 })
                 .FirstOrDefaultAsync(ct);
        }
    }
}
