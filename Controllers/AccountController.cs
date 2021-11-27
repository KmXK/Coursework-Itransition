using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.Models;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var model = new SettingsViewModel()
            {
                Languages = new List<string>() {"Russian", "English"},
                Themes = new List<string>() {"White", "Black"},
                TargetLanguage = "Russian",
                TargetTheme = "White"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Settings(SettingsViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
