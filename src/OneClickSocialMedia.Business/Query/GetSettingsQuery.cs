using MediatR;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneClickSocialMedia.Business.Query
{
    public class GetSettingsQuery : IRequest<GetSettingsResponse>
    {
        public string UserId { get; set; }
    }
}
