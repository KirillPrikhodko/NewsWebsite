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

        public ManageController(IAccountManageService accountManageService, ILoginService loginService)
        {
            this.accountManageService = accountManageService;
            this.loginService = loginService;
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";
            return View();
        }

        //TODO: implement the rest of the class

        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await loginService.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = accountManageService.FindById(User.Identity.GetUserId()); //original code used await UserManager.FindByIdAsync
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

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await accountManageService.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = accountManageService.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                await loginService.Login(user, false, false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await accountManageService.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = accountManageService.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                await loginService.Login(user, false, false);
            }
            return RedirectToAction("Index", "Manage");
        }

        ////
        //// GET: /Manage/VerifyPhoneNumber
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var code = await accountManageService.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
        //    // Send an SMS through the SMS provider to verify the phone number
        //    return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        //}

        ////
        //// POST: /Manage/VerifyPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
        //    }
        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "Failed to verify phone");
        //    return View(model);
        //}

        ////
        //// POST: /Manage/RemovePhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RemovePhoneNumber()
        //{
        //    var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
        //    if (!result.Succeeded)
        //    {
        //        return RedirectToAction("Index", new { Message = ManageMessageId.Error });
        //    }
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        //}

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
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
                var user = accountManageService.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    await loginService.Login(user, false, false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountManageService.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = accountManageService.FindById(User.Identity.GetUserId());
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

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = accountManageService.FindById(User.Identity.GetUserId());
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

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await accountManageService.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        public async Task<ActionResult> GetAccountSettingsPartial()
        {
            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await accountManageService.GetPhoneNumberAsync(userId),
                TwoFactor = await accountManageService.GetTwoFactorEnabledAsync(userId),
                Logins = await accountManageService.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return PartialView("_AccountSettingsPartial", model);
        }

        public ActionResult GetAccountGeneralInfoPartial()
        {
            return View("_AccountGeneralInfoPartial");
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

        private bool HasPassword()
        {
            var user = accountManageService.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = accountManageService.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }
        #endregion
    }
}