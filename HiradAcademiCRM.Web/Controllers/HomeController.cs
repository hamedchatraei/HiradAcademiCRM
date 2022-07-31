using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using KonkurCRM.Core.DTOs.User;
using KonkurCRM.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace HiradAcademiCRM.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Login

        [Route("Login")]
        public IActionResult Login(bool EditProfile = false, string ReturnUrl = "/AdviserPanel")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            string ReturnUrl = login.ReturnUrl;

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;

                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ReturnUrl = "/AdviserPanel";
                        return Redirect(ReturnUrl);
                    }

#pragma warning disable CS0162 // Unreachable code detected
                    return View();
#pragma warning restore CS0162 // Unreachable code detected
                }
                else
                {
                    ModelState.AddModelError("UserName", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("UserName", "کاربری با این مشخصات یافت نشد");
            return View(login);


        }

        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion
    }
}