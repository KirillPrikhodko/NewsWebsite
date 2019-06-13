using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IAccountManageService accountManageService;
        private readonly ILoginService loginService;
        private readonly IUserService userService;

        public ManageController(IAccountManageService accountManageService, IUserService userService, ILoginService loginService)
        {
            this.accountManageService = accountManageService;
            this.loginService = loginService;
            this.userService = userService;
        }

        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.Blocked = HttpContext.User.IsInRole("Blocked");
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? Localization.PasswordChanged
                : message == ManageMessageId.SetPasswordSuccess ? Localization.PasswordSet
                : message == ManageMessageId.SetTwoFactorSuccess ? Localization.TwoFactorSet
                : message == ManageMessageId.Error ? Localization.Error
                : message == ManageMessageId.AddPhoneSuccess ? Localization.PhoneAdded
                : message == ManageMessageId.RemovePhoneSuccess ? Localization.PhoneRemoved
                : "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await loginService.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await loginService.Login(user, false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }
        
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await accountManageService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await loginService.Login(user, false, false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountManageService.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await loginService.Login(user, false, false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await accountManageService.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }
        
        public async Task<ActionResult> GetAccountSettingsPartial()
        {
            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = await HasPassword(),
                PhoneNumber = await accountManageService.GetPhoneNumberAsync(userId),
                TwoFactor = await accountManageService.GetTwoFactorEnabledAsync(userId),
                Logins = await accountManageService.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return PartialView("_AccountSettingsPartial", model);
        }

        public async Task<ActionResult> GetAccountGeneralInfoPartial()
        {
            var model = await userService.FindByIdAsync(HttpContext.User.Identity.GetUserId());
            return PartialView("_AccountGeneralInfoPartial", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && accountManageService != null || loginService != null)
            {
                //_userManager.Dispose(); //TODO: ImplementIDisposable on all controllers, services and repositories
                //_userManager = null;
            }

            base.Dispose(disposing);
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

    #region Helpers
    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task<bool> HasPassword()
        {
            var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private async Task<bool> HasPhoneNumber()
        {
            var user = await accountManageService.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }
        #endregion
    }
}