namespace OneClickSocialMedia.Contract.Dtos
{
    public sealed class FacebookCredentialsDto
    {
        public required string AccessToken { get; init; }

        public required string PageId { get; init; }

    }
}
