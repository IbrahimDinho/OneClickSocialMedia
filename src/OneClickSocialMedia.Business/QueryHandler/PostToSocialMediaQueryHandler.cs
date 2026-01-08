using MediatR;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class PostToSocialMediaQueryHandler : IRequestHandler<PostToSocialMediaQuery, PostToSocialMediaResponse>
    {
        public Task<PostToSocialMediaResponse> Handle(PostToSocialMediaQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
