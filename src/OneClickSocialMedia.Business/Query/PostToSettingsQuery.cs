using MediatR;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClickSocialMedia.Business.Query
{
    public class PostToSettingsQuery : IRequest<PostToSettingsResponse>
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Twitter Api Key
        /// </summary>
        public string TwitterApiKey { get; set; }

        /// <summary>
        /// Twitter Api Secret
        /// </summary>
        public string TwitterApiSecret { get; set; }

        /// <summary>
        /// Twitter Access Token
        /// </summary>
        public string TwitterAccessToken { get; set; }

        /// <summary>
        /// Twitter Token Secret
        /// </summary>
        public string TwitterAccessTokenSecret { get; set; }
    }
}
