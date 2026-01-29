using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Web.ViewModel;

namespace OneClickSocialMedia.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IMediator mediator;


        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Request.Query["ReturnUrl"].ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel viewModel, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                LoginQuery command = new LoginQuery();
                command.Email = viewModel.Email;
                command.Password = viewModel.Password;
                command.RememberMe = viewModel.RememberMe;

                LoginResponse response = mediator.Send(command).GetAwaiter().GetResult();

                if (response.IsSuccess)
                {
                    //Go to redirect if needs be and is safe.
                    //e.g deal with -> https://localhost:7275/Account/Login?ReturnUrl=https://evil.com
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.ErrorMessage);
                }

            }

            return View(viewModel);
        }

        public IActionResult Logout()
        {
            LogoutQuery command = new LogoutQuery();
            LogoutResponse response = mediator.Send(command).GetAwaiter().GetResult();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                RegisterQuery command = new RegisterQuery();
                command.Name = viewModel.Name;
                command.Email = viewModel.Email;
                command.Password = viewModel.Password;
                command.ConfirmPassword = viewModel.ConfirmPassword;

                RegisterResponse response = mediator.Send(command).GetAwaiter().GetResult();

                if (response.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in response.ErrorMessage)
                    {
                        ModelState.AddModelError("", error);
                    }
                    
                }

            }

            return View(viewModel);

        }
    }
}
