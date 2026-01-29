using MediatR;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClickSocialMedia.Business.Query
{
    public class RegisterQuery : IRequest<RegisterResponse>
    {

        /// <summary>
        /// Name of user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email Address of user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password of user confirm
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
