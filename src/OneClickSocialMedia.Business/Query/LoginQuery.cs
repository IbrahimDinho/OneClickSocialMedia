using MediatR;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClickSocialMedia.Business.Query
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        /// <summary>
        /// Email Address of user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Remember me for the user.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
