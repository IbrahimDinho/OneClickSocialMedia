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
        private readonly IInstagramPostService instagramPostService;
        private readonly ICredentialsProvider credentialsProvider;

        public PostToSocialMediaCommandHandler(ITwitterPostService twitterPostService, ICredentialsProvider credentialsProvider, IInstagramPostService instagramPostService)
        {
            this.twitterPostService = twitterPostService;
            this.credentialsProvider = credentialsProvider;
            this.instagramPostService = instagramPostService;
        }
        public async Task<PostToSocialMediaResponse> Handle(PostToSocialMediaCommand command, CancellationToken cancellationToken)
        {
            PostToSocialMediaResponse validationResult = ValidateCommand(command);

            if (validationResult != null)
            {
                return validationResult;
            }

            //split into 3 services each service posts. can do validation and all in the services and 1 credential provider to get
            // things from the database


            List<Task> tasks = new List<Task>();

            if (command.IsTwitter)
            {
                tasks.Add(PostToTwitter(command));
            }
            if (command.IsInstagram)
            {
                tasks.Add(PostToInstagram(command));
            }
            if (command.IsFaceBook)
            {
                // do work
            }

            await Task.WhenAll(tasks);

            // TODO Get the error messages and join them together if failed and so users knows why... 
            return new PostToSocialMediaResponse
            {
                IsSuccess = true,
            };
        }

        private PostToSocialMediaResponse? ValidateCommand(PostToSocialMediaCommand command)
        {
            // Will need to refactor it throw exceptions/ better error handling.
            //TODO IE
            if (!command.IsTwitter && !command.IsInstagram && !command.IsFaceBook)
            {
                return new PostToSocialMediaResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Please select at least one social media platform."
                };
            }

            bool hasImageFile = command.Image != null && command.Image.Length > 0;
            bool hasImageUrl = !string.IsNullOrWhiteSpace(command.URLforImage);


            if (command.IsInstagram && !hasImageUrl)
            {
                return new PostToSocialMediaResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Instagram requires an image URL posted alongside it."
                };
            }

            return null; // valid
        }

        private async Task PostToTwitter(PostToSocialMediaCommand command)
        {
            TwitterCredentialsDto twitterCredentials = await credentialsProvider.GetTwitterCredsUserAsync(Guid.Parse(command.UserId));

            if (command.HasImage() == false)
            {
                await twitterPostService.PostAsync(command.Comment, twitterCredentials);
            }
            else
            {
                await twitterPostService.PostAsync(command.Comment, command.Image, command.URLforImage, twitterCredentials);
            }
        }

        private async Task PostToInstagram(PostToSocialMediaCommand command)
        {
            InstagramCredentialsDto instagramCredentials = await credentialsProvider.GetInstagramCredsUserAsync(Guid.Parse(command.UserId));


            await instagramPostService.PostAsync(command.Comment, command.URLforImage, instagramCredentials);
        }

        private async Task PostToFacebook(PostToSocialMediaCommand command)
        {
            FacebookCredentialsDto facebookCredentials = await credentialsProvider.GetFacebookCredsUserAsync(Guid.Parse(command.UserId));

            //TODO facebook service add and do validation too
            //get pageid and page acess token in postasync!! for fb service
        }
    }
}
