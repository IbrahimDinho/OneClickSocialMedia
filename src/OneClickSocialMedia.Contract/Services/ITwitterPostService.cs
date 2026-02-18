using OneClickSocialMedia.Contract.Dtos;

namespace OneClickSocialMedia.Contract.Services
{
    public interface ITwitterPostService
    {
        /// <summary>
        /// Post a comment to twitter
        /// </summary>
        /// <param name="commentToPost">comment to post</param>
        /// <param name="twitterCredentialsDto">twitter api keys and secrets</param>
        /// <returns></returns>
        public Task PostAsync(string commentToPost, TwitterCredentialsDto twitterCredentialsDto);

        /// <summary>
        /// Post the comment to twitter along with file or image
        /// </summary>
        /// <param name="commentToPost">comment to post</param>
        /// <param name="fileImage">image file to be posted</param>
        /// <param name="ImageUrl">image url of the image to be posted</param>
        /// <param name="twitterCredentialsDto">twitter api keys and secrets</param>
        /// <returns></returns>
        public Task PostAsync(string commentToPost, Stream? fileImage, string? ImageUrl, TwitterCredentialsDto twitterCredentialsDto);

    }
}
