using OneClickSocialMedia.Contract.Dtos;

namespace OneClickSocialMedia.Contract.Services
{
    public interface IFacebookPostService
    {
        /// <summary>
        /// Post a comment to a facebook page
        /// </summary>
        /// <param name="commentToPost">comment to post to page</param>
        /// <param name="facebookCredentialsDto">facebook api page token and page id</param>
        /// <returns></returns>
        public Task<string> PostAsync(string commentToPost, FacebookCredentialsDto facebookCredentialsDto);

        /// <summary>
        /// Post the comment to a facebook page alongside an image.
        /// </summary>
        /// <param name="commentToPost">comment to post to page</param>
        /// <param name="ImageUrl">image url of the image to be posted</param>
        /// <param name="facebookCredentialsDto">facebook api page token and page id</param>
        /// <returns></returns>
        public Task<string> PostAsync(string commentToPost, string? ImageUrl, FacebookCredentialsDto facebookCredentialsDto);

    }
}
