using System;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Coursework.Controllers
{
    [Authorize]
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IOptions<RequestLocalizationOptions> _locOptions;

        public AccountController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context,
            IConfiguration configuration,
            IOptions<RequestLocalizationOptions> locOptions)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _locOptions = locOptions;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            
            return View(new AccountViewModel()
            {
                User = user,
                Reviews = _context.Reviews.Include(r=>r.Author).Where(r=>r.Author==user).ToList()
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Settings()
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var cultureItems = _locOptions.Value.SupportedCultures
                .Select(c => new SelectListItem {Value = c.Name, Text = c.NativeName})
                .ToList();

            var themeSection = _configuration.GetSection("ThemeFiles");
            var themes = themeSection.GetChildren()
                .Select(c => new SelectListItem() {Value = c.Key, Text = c.Key})
                .ToList();

            if (!Request.Cookies.TryGetValue("themeCookie", out string themeValue))
                themeValue = _configuration["DefaultTheme"];

            var model = new SettingsViewModel()
            {
                Themes = themes,
                TargetTheme = themeValue,
                TargetCulture = requestCulture.RequestCulture.Culture.Name,
                Cultures = cultureItems
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Settings(SettingsViewModel model)
        {
            if(!string.IsNullOrWhiteSpace(model.TargetCulture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(model.TargetCulture)),
                    new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)}
                );
            }

            var themeSection = _configuration.GetSection("ThemeFiles").GetSection(model.TargetTheme);
            if(themeSection.Exists())
                Response.Cookies.Append("themeCookie", model.TargetTheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
