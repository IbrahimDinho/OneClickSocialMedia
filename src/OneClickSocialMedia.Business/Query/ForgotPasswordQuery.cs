using MediatR;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClickSocialMedia.Business.Query
{
    public class ForgotPasswordQuery : IRequest<ForgotPasswordResponse>
    {
        /// <summary>
        /// The purpoted user email.
        /// </summary>
        public  string Email { get; set; }
    }
}
