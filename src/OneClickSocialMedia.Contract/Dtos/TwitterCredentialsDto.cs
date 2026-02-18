using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Contract.Dtos
{
    public sealed class TwitterCredentialsDto
    {
        public required string ApiKey { get; init; }
        public required string ApiSecret { get; init; }
        public required string AccessToken { get; init; }
        public required string AccessTokenSecret { get; init; }
    }
}
