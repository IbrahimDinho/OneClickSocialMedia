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
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        public HomeController(IMediator mediator) 
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Setting()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GetSettingsQuery request = new GetSettingsQuery();
            request.UserId = userId;

            GetSettingsResponse response = mediator.Send(request).GetAwaiter().GetResult();

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
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostSettings(SocialMediaSettingsViewModel viewModel)
        {
            PostToSettingsQuery request = new PostToSettingsQuery()
            {
                TwitterApiKey = viewModel.TwitterApiKey?.Trim(), //trim to remove accidental white space when paste in.
                TwitterApiSecret = viewModel.TwitterApiSecret?.Trim(),
                TwitterAccessToken = viewModel.TwitterAccessToken?.Trim(),
                TwitterAccessTokenSecret = viewModel.TwitterAccessTokenSecret?.Trim(),
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            PostToSettingsResponse response = mediator.Send(request).GetAwaiter().GetResult();

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
        public IActionResult PostToSocialMedia(SocialMediaViewModel viewModel)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PostToSocialMediaQuery request = new PostToSocialMediaQuery()
            {
                IsFaceBook = viewModel.IsFaceBook,
                IsInstagram = viewModel.IsInstagram,
                IsTwitter = viewModel.IsTwitter,
                Comment = viewModel.Comment,
                Image = viewModel.Image?.OpenReadStream(),
                URLforImage = viewModel.URLforImage,
                UserId = currentUserId,
            };
            
            PostToSocialMediaResponse response = mediator.Send(request).GetAwaiter().GetResult();

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
