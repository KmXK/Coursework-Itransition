using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.Controllers
{
    public class AuthorizationController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public AuthorizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Username
                };

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, 
                        model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile avatarFile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Username
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string path;
                    if (avatarFile != null)
                    {
                        path = "/Files/" + user.Id + avatarFile.FileName;
                        await using (var fs = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await avatarFile.CopyToAsync(fs);
                        }
                    }
                    else
                        path = "/Files/no_avatar.jpg";

                    user.AvatarUrl = path;
                    await _userManager.UpdateAsync(user);

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginExternal(string provider, string returnUrl)
        {
            var redirectUrl = "Authorization/LoginExternalCallback";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> LoginExternalCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(RegisterExternal));
        }

        public IActionResult RegisterExternal()
        {
            return View();
        }

        [HttpPost]
        [ActionName(nameof(RegisterExternal))]
        public async Task<IActionResult> RegisterExternalConfirmed(RegisterExternalViewModel model)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = new ApplicationUser { UserName = model.Username, AvatarUrl = "/Files/no_avatar.jpg"};

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var identityResult = await _userManager.AddLoginAsync(user, info);
                if (identityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Username", "This account is already registered.");
            }
            else
                ModelState.AddModelError("Username", "This name is already in use.");


            return View(model);
        }
    }
}
