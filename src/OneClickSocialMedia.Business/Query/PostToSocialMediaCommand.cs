using MediatR;
using Microsoft.AspNetCore.Http;
using OneClickSocialMedia.Business.Query.Response;
using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Business.Query
{
    public class PostToSocialMediaCommand : IRequest<PostToSocialMediaResponse>
    {
        /// <summary>
        /// If facebook checkbox has been checked by user
        /// </summary>
        public bool IsFaceBook { get; set; }

        /// <summary>
        /// If twitter checkbox has been checked by user
        /// </summary>
        public bool IsTwitter { get; set; }

        /// <summary>
        /// If instagram checkbox has been checked by user
        /// </summary>
        public bool IsInstagram { get; set; }

        /// <summary>
        /// The image file given by the user
        /// </summary>
        public Stream? Image { get; set; }

        /// <summary>
        /// The image file given by the user
        /// </summary>
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// Url of image that the user wants to post
        /// </summary>
        [Url]
        public string URLforImage { get; set; }

        /// <summary>
        /// comment posted by the user.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The current user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// If image has been passed in
        /// </summary>
        public bool HasImage()
        {
            return (Image != null && Image.Length > 0) ||
                   !string.IsNullOrWhiteSpace(URLforImage);
        }
    }
}

