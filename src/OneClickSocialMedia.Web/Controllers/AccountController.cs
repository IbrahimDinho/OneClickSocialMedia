using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClickSocialMedia.Business.Query;
using OneClickSocialMedia.Business.Query.Response;
using OneClickSocialMedia.Web.ViewModel;

namespace OneClickSocialMedia.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Request.Query["ReturnUrl"].ToString();
            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
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
                    foreach (string error in response.ErrorMessages)
                    {
                        ModelState.AddModelError("", error);
                    }

                }

            }

            return View(viewModel);

        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            return RedirectToAction(nameof(RecoverAccountConfirmation), new { email = model.Email });
        }

        [AllowAnonymous]
        public async Task<IActionResult> RecoverAccountConfirmation(string email)
        {
            ForgotPasswordQuery query = new ForgotPasswordQuery()
            {
                Email = email
            };

            ForgotPasswordResponse response = await mediator.Send(query);

            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            ResetPasswordQuery query = new ResetPasswordQuery()
            {
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                Token = model.Token
            };

            ResetPasswordResponse response = await mediator.Send(query);

            if (response.IsSuccess)
                return RedirectToAction("ResetPasswordConfirmation");

            if (response.ErrorMessages != null)
            {
                foreach (string error in response.ErrorMessages)
                    ModelState.AddModelError("", error);
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            ChangePasswordQuery query = new ChangePasswordQuery();

            ChangePasswordResponse response = await mediator.Send(query);

            return View(new ChangePasswordViewModel { Email = response.Email });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new ChangePasswordCommand
            {
                CurrentPassword = model.CurrentPassword,
                NewPassword = model.NewPassword,
                ConfirmNewPassword = model.ConfirmNewPassword
            };

            ChangePasswordCommandResponse response = await mediator.Send(command);

            if (response.IsSuccess)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            if (response.ErrorMessages != null)
            {
                foreach (var error in response.ErrorMessages)
                    ModelState.AddModelError("", error);
            }

            return View(model);

        }

    }
}
