using OneClickSocialMedia.Contract.Dtos;

namespace OneClickSocialMedia.Contract
{
    public interface ICredentialsProvider
    {
        /// <summary>
        /// Get twitter creds including which includes all 4 of API key and secret and Token and token secret.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<TwitterCredentialsDto> GetTwitterCredsUserAsync(Guid userId, CancellationToken ct = default);


        /// <summary>
        /// Get twitter creds including which includes all 4 of API key and secret and Token and token secret.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<InstagramCredentialsDto> GetInstagramCredsUserAsync(Guid userId, CancellationToken ct = default);

    }
}
