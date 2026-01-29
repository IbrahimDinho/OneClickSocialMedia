using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Web.ViewModel;
using System.Diagnostics;

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

        public IActionResult Setting()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostToSocialMedia(SocialMediaViewModel viewModel)
        {
            PostToSocialMediaQuery request = new PostToSocialMediaQuery();
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
