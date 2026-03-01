using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Request.Query["ReturnUrl"].ToString();
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                LoginCommand command = new LoginCommand();
                command.Email = viewModel.Email;
                command.Password = viewModel.Password;
                command.RememberMe = viewModel.RememberMe;

                LoginResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    if (response.RequiresTwoFactor)
                    {
                        viewModel.RequiresTwoFactor = true;
                        viewModel.TwoFactorProvider = response.TwoFactorProvider;

                        return View(viewModel);
                    }

                    if (response.ShouldPromptEnableTwoFactor)
                    {
                        return RedirectToAction(nameof(EnableTwoFactor));
                    }

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


        public async Task<IActionResult> Logout()
        {
            LogoutCommand command = new LogoutCommand();
            LogoutResponse response = await mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyTwoFactor(LoginViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.TwoFactorCode))
            {
                ModelState.AddModelError("", "Please enter the verification code.");
                viewModel.RequiresTwoFactor = true;
                return View("Login", viewModel);
            }

            VerifyTwoFactorCommandResponse response = await mediator.Send(new VerifyTwoFactorCommand
            {
                Code = viewModel.TwoFactorCode,
                Provider = viewModel.TwoFactorProvider ?? TokenOptions.DefaultEmailProvider,
                RememberMe = viewModel.RememberMe
            });

            if (!response.IsSuccess)
            {
                foreach (string error in response.ErrorMessages)
                    ModelState.AddModelError("", error);

                viewModel.RequiresTwoFactor = true;
                return View("Login", viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                RegisterCommand command = new RegisterCommand();
                command.Name = viewModel.Name;
                command.Email = viewModel.Email;
                command.Password = viewModel.Password;
                command.ConfirmPassword = viewModel.ConfirmPassword;

                RegisterResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(EnableTwoFactor));
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
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
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
                return RedirectToAction(nameof(Login));

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
            ResetPasswordCommand query = new ResetPasswordCommand()
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
        public async Task<IActionResult> ResetPasswordConfirmation()
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

        [Authorize]
        public async Task<IActionResult> EnableTwoFactor()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactor(bool enable)
        {
            if (enable == false)
            {
                RedirectToAction("Index", "Home");
            }

            Enable2FactorCommand command = new Enable2FactorCommand()
            {
                Enable = true,
            };

            Enable2FactorCommandResponse response = await mediator.Send(command);

            if (response.IsSuccess == false)
            {
                foreach (string error in response.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
