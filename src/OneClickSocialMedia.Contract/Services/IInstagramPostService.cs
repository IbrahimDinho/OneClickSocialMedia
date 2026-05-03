using OneClickSocialMedia.Contract.Dtos;

namespace OneClickSocialMedia.Contract.Services
{
    public interface IInstagramPostService
    {

        /// <summary>
        /// Post the comment to instagram along with file or image
        /// </summary>
        /// <param name="commentToPost">comment to post</param>
        /// <param name="imageUrl">image url of the image to be posted</param>
        /// <param name="instagramCredentialsDto">Instagram api keys and secrets</param>
        /// <returns></returns>
        public Task<string> PostAsync(string commentToPost, string? imageUrl, InstagramCredentialsDto instagramCredentialsDto);

    }
}
