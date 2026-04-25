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
        public async Task<PostToSocialMediaResponse> Handle(PostToSocialMediaCommand command, CancellationToken cancellationToken)
        {
            //split into 3 services each service posts. can do validation and all in the services and 1 credential provider to get
            // things from the database

            #region Twitter
            TwitterCredentialsDto twitterCredentials = await credentialsProvider.GetTwitterCredsUserAsync(Guid.Parse(command.UserId));

            if (command.HasImage() == false)
            {
                await twitterPostService.PostAsync(command.Comment, twitterCredentials);
            }
            else
            {
                await twitterPostService.PostAsync(command.Comment, command.Image, command.URLforImage, twitterCredentials);
            }
            #endregion

            // Get the error messages and join them together if failed and so users knows why... 
            return new PostToSocialMediaResponse
            {
                IsSuccess = true,
            };
        }
    }
}
