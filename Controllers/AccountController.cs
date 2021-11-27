using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.Models;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Coursework.Controllers
{
    [Authorize]
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
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

            var model = new SettingsViewModel()
            {
                Themes = new List<string>() {"White", "Black"},
                TargetCulture = requestCulture.RequestCulture.Culture.Name,
                TargetTheme = "White"
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

            return RedirectToAction("Index", "Home");
        }
    }
}
