using Microsoft.AspNetCore.DataProtection;
using OneClickSocialMedia.Contract;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;
using OneClickSocialMedia.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
namespace OneClickSocialMedia.Business.Service
{
    public class TwitterPostService : ITwitterPostService
    {
        private string XapiKey;
        private string XapiSecret;
        private string Xaccesstoken;
        private string Xtokendecret;
        private const string Xendpoint = "https://api.twitter.com/2/tweets";
        private const string Xmediaendpoint = "https://upload.twitter.com/1.1/media/upload.json";

        private readonly IHttpClientFactory httpClientFactory;

        public TwitterPostService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, TwitterCredentialsDto twitterCredentialsDto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task PostAsync(string commentToPost, Stream? fileImage, string? ImageUrl, TwitterCredentialsDto twitterCredentialsDto)
        {
            throw new NotImplementedException();
        }


       

    }
}
