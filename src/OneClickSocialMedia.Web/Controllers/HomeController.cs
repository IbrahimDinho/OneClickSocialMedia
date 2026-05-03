using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Web.ViewModel;
using System.Diagnostics;
using System.Security.Claims;

namespace OneClickSocialMedia.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Setting()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GetSettingsQuery request = new GetSettingsQuery();
            request.UserId = userId;

            GetSettingsResponse response = await mediator.Send(request);

            if (!response.IsSuccess)
            {
                return View();
            }

            SocialMediaSettingsViewModel viewModel = new SocialMediaSettingsViewModel
            {
                TwitterApiKey = response.TwitterApiKey,
                TwitterApiSecret = response.TwitterApiSecret,
                TwitterAccessToken = response.TwitterAccessToken,
                TwitterAccessTokenSecret = response.TwitterAccessTokenSecret,
                HasTwitterApiSecret = response.HasTwitterApiSecret,
                HasTwitterAccessTokenSecret = response.HasTwitterAccessTokenSecret,
                InstagramAccessToken = response.InstagramAccessToken,
                HasInstagramAccessToken = response.HasInstagramAccessToken,
                HasFacebookAccessToken = response.HasFacebookAccessToken,
                FacebookAccessToken = response.FacebookAccessToken
            };


            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSettings(SocialMediaSettingsViewModel viewModel)
        {
            PostToSettingsCommand command = new PostToSettingsCommand()
            {
                TwitterApiKey = viewModel.TwitterApiKey?.Trim(), //trim to remove accidental white space when paste in tokens.
                TwitterApiSecret = viewModel.TwitterApiSecret?.Trim(),
                TwitterAccessToken = viewModel.TwitterAccessToken?.Trim(),
                TwitterAccessTokenSecret = viewModel.TwitterAccessTokenSecret?.Trim(),
                UpdateTwitterCredentials = viewModel.UpdateTwitterCredentials,
                UpdateInstagramCredentials = viewModel.UpdateInstagramCredentials,
                InstagramAccessToken = viewModel.InstagramAccessToken?.Trim(),
                FacebookAccessToken = viewModel.FacebookAccessToken?.Trim(),
                UpdateFacebookCredentials = viewModel.UpdateFacebookCredentials,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            PostToSettingsResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                TempData["Success"] = true;
                TempData["Message"] = "Posted successfully!";
            }
            else
            {
                TempData["Message"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Setting));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostToSocialMedia(SocialMediaViewModel viewModel)
        {

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PostToSocialMediaCommand command = new PostToSocialMediaCommand()
            {
                IsFaceBook = viewModel.IsFaceBook,
                IsInstagram = viewModel.IsInstagram,
                IsTwitter = viewModel.IsTwitter,
                Comment = viewModel.Comment,
                Image = viewModel.Image?.OpenReadStream(),
                ImageFile = viewModel.Image,
                URLforImage = viewModel.URLforImage,
                UserId = currentUserId,
            };

            PostToSocialMediaResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                TempData["Success"] = true;
                TempData["Message"] = "Posted successfully!";
            }
            else
            {
                TempData["Message"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Index));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
