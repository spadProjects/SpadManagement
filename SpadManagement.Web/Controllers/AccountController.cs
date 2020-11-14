using SpadManagement.Web.Models;
using SpadManagement.Web.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;

namespace SpadManagement.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public ActionResult Index()
        {
            return View("Login");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Session["UserName"])))
                return RedirectToAction("index", "Home");

            return View("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CustomerSlide()
        {
            return View("CustomerSlide");
        }


        public async Task<ActionResult> CheckUserActivation()
        {
            var userId = User.Identity.GetUserId();
            string cache_key = userId + "-CheckUserActivation";
            object result = HttpContext.Cache.Get(cache_key);
            if (result == null)
            {
                var user = await UserManager.FindByIdAsync(userId);
                result = user.IsLocked ?? true;
                HttpContext.Cache.Add(cache_key, result, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.High, null);
            }
            return Content(((bool)result).ToString().ToLower());
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var appSettings = ConfigurationManager.AppSettings;
                var captcha = appSettings["enableCaptcha"];

                bool needCaptcha;
                var IsBool = bool.TryParse(captcha, out needCaptcha);

                if (!IsBool)
                    throw new Exception("تنظیمات Captcha در webConfig صحیح نیست");

                if (!this.IsCaptchaValid("captcha is not valid") && needCaptcha)
                {
                    ViewBag.ErrorMessage = "عبارت امنیتی را درست وارد کنید";
                    return View("Login");
                }

                var userExistence = await UserManager.FindByNameAsync(model.UserName);
                if (userExistence != null)
                {
                    var user = await UserManager.FindAsync(model.UserName, model.Password);
                    if (user != null)
                    {
                        if (user.IsLocked ?? false)
                        {
                            ModelState.AddModelError("", "این حساب کاربری مسدود است");
                            return View(model);
                        }

                        if (user.LockoutEnabled)
                        {
                            var endDate = user.LockoutEndDateUtc != null ? user.LockoutEndDateUtc.Value : DateTime.MinValue;
                            if (endDate >= DateTime.Now)
                            {
                                ModelState.AddModelError("", string.Format("حساب کاربری شما به مدت {0} دقیقه به صورت موقت مسدود شد",
                                       ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));

                                return View("Login");
                            }
                        }

                        //var userCenterId = ServiceFactory.CreateByModel<AspNetUser>()
                        //	.GetDefaultQuery()
                        //	.Where(p => p.UserName.ToLower() == user.UserName)
                        //	.Select(p => p.CenterId)
                        //	.FirstOrDefault() ?? 0;

                        //if (userCenterId != 0)
                        //{
                        //	var owners = ServiceFactory.Create<Services.Interfaces.IPersonService>("PersonService")
                        //		.GetAvaiableCentersForSelectingOwner(userCenterId)
                        //		.ToList();

                        //}
                        user.LastLoginDatePreview = user.LastLoginDate ?? DateTime.Now;
                        user.LastLoginDate = DateTime.Now;
                        await UserManager.UpdateAsync(user);

                        await SignInAsync(user, model.RememberMe);
                        await UserManager.ResetAccessFailedCountAsync(user.Id);
                        await UserManager.SetLockoutEnabledAsync(user.Id, false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        if (userExistence.LockoutEnabled)
                        {
                            var endDate = userExistence.LockoutEndDateUtc.Value;
                            if (endDate >= DateTime.Now)
                            {
                                ModelState.AddModelError("", string.Format("حساب کاربری شما به مدت {0} دقیقه به صورت موقت مسدود شد",
                                       ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));

                                user.LastLoginDatePreview = user.LastLoginDate ?? DateTime.Now;
                                user.LastLoginDate = DateTime.Now;
                                await UserManager.UpdateAsync(user);
                                return View("Login");
                            }
                            else
                            {
                                await UserManager.ResetAccessFailedCountAsync(userExistence.Id);
                                await UserManager.SetLockoutEnabledAsync(userExistence.Id, false);

                                var message = await FailedAccess(userExistence);
                                ModelState.AddModelError("", message);
                            }
                        }
                        else
                        {
                            var message = await FailedAccess(userExistence);
                            ModelState.AddModelError("", message);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "نام کاربری یا رمز عبور نادرست است");
                }
            }
            // If we got this far, something failed, redisplay form
            return View("Login");
        }

        private async Task<string> FailedAccess(ApplicationUser userExistence)
        {
            var message = "";
            userExistence.AccessFailedCount++;
            await UserManager.UpdateAsync(userExistence);

            int accessFailedCount = await UserManager.GetAccessFailedCountAsync(userExistence.Id);

            int attemptsLeft =
                Convert.ToInt32(
                    ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"].ToString()) -
                accessFailedCount;

            if (attemptsLeft > 0)
            {
                message = string.Format("نام کاربری شما پس از {0} تلاش ناموفق دیگر به صورت موقت قفل خواهد شد", attemptsLeft);
            }
            else
            {
                int lockTime = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString());

                message = string.Format("به دلیل چندین تلاش ناموفق برای ورود، حساب کاربری شما به مدت  {0} دقیقه از دسترس خارج شده است.", lockTime);

                userExistence.LockoutEnabled = true;
                userExistence.LockoutEndDateUtc = DateTime.Now.AddMinutes(lockTime);
                await UserManager.UpdateAsync(userExistence);
            }
            return message;
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "گذرواژه شما با موفقیت تغییر یافت."
                : message == ManageMessageId.SetPasswordSuccess ? "گذرواژه شما تنظیم شد."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "خطا اتفاق افتاد."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            var dbUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (dbUser != null)
            {
                //var lastChangeDate = dbUser.LastPasswordChangedDate ?? DateTime.MinValue;
                //if (lastChangeDate.AddHours(24) <= DateTime.Now)
                //{
                if (hasPassword)
                {
                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            dbUser.LastPasswordChangedDate = DateTime.Now;
                            await UserManager.UpdateAsync(dbUser);

                            //var userEmail = ServiceFactory.Create<IAspNetUserService>("AspNetUserService").GetCurrentUserEmailAddress();
                            //if (!string.IsNullOrEmpty(userEmail))
                            //{
                            //    var receiverList = new List<string>();
                            //    receiverList.Add(userEmail);

                            //    var changeDate = dbUser.LastPasswordChangedDate.Value;
                            //    var changeDateString = changeDate.ToPersianString() + " " + $"{changeDate.Hour}:{changeDate.Minute}:{changeDate.Second}";

                            //    var emailProvider = ObjectRegistry.GetObject<IEmailProvider>("EmailProvider");
                            //    emailProvider.Notify($"کاربر گرامی رمز عبور شما در تاریخ {changeDateString} با موفقیت تغییر یافت",
                            //        "تغییر رمز عبور", receiverList, null);
                            //}

                            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }
                else
                {
                    // User does not have a password so remove any validation errors caused by a missing OldPassword field
                    ModelState state = ModelState["OldPassword"];
                    if (state != null)
                    {
                        state.Errors.Clear();
                    }

                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }

                //}
                //else
                //{
                //    var error = new List<string>();
                //    error.Add("هنوز 24 ساعت از زمان آخرین تغییر رمز عبور شما نگذشته است. لطفا بعدا سعی کنید");

                //    AddErrors(new IdentityResult(error));
                //}
            }
            else
            {
                var error = new List<string>();
                error.Add("کاربر جاری پیدا نشد");

                AddErrors(new IdentityResult(error));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Session["UserName"] = string.Empty;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
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

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            HttpContext.Session["UserName"] = user.UserName;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
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
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}