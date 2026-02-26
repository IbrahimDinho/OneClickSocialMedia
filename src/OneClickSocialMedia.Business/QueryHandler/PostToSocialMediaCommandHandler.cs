using MediatR;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Contract;
using OneClickSocialMedia.Contract.Dtos;
using OneClickSocialMedia.Contract.Services;

namespace OneClickSocialMedia.Business.QueryHandler
{
    public class PostToSocialMediaCommandHandler : IRequestHandler<PostToSocialMediaCommand, PostToSocialMediaResponse>
    {
        private readonly ITwitterPostService twitterPostService;
        private readonly ICredentialsProvider credentialsProvider;

        public PostToSocialMediaCommandHandler(ITwitterPostService twitterPostService, ICredentialsProvider credentialsProvider)
        {
            this.twitterPostService = twitterPostService;
            this.credentialsProvider = credentialsProvider;
        }
        public async Task<PostToSocialMediaResponse> Handle(PostToSocialMediaCommand request, CancellationToken cancellationToken)
        {
            //split into 3 services each service posts. can do validation and all in the services and 1 credential provider to get
            // things from the database
            TwitterCredentialsDto twitterCredentials = await credentialsProvider.GetTwitterCredsUserAsync(Guid.Parse(request.UserId));

            // If no image just normal post otherwise post with image url or file
            twitterPostService.PostAsync(request.Comment, twitterCredentials);


            // Get the error messages and join them together if failed and so users knows why... 
            return new PostToSocialMediaResponse
            {
                IsSuccess = true,
            };
        }
    }
}
