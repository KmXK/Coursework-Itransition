using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.Models;
using Ganss.XSS;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Controllers
{
    [Authorize]
    public class ReviewController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRewiewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                string textHtml = sanitizer.Sanitize(Markdown.ToHtml(model.Text));

                var rewiew = new Review()
                {
                    Title = model.Title,
                    Text = textHtml,
                    AuthorRating = model.Rating,
                    Author = await _userManager.FindByIdAsync(_userManager.GetUserId(User))
                };

                await _context.Reviews.AddAsync(rewiew);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            var review = await _context.Reviews.Include(r => r.Author).FirstOrDefaultAsync(r => r.Id == id);

            if (review.Author != user)
                return Unauthorized();

            return View(new EditReviewViewModel()
            {
                Id = review.Id,
                Text = review.Text,
                Title = review.Title,
                Rating = review.AuthorRating
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var review = await _context.Reviews.FindAsync(model.Id);
                review.AuthorRating = model.Rating;
                review.Title = model.Title;


                var sanitizer = new HtmlSanitizer();
                review.Text = sanitizer.Sanitize(Markdown.ToHtml(model.Text));

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var rewiew = await _context.Reviews
                .Include(r=>r.Author)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rewiew == null)
                return NotFound();

            return View(rewiew);
        }

        public async Task<IActionResult> Delete(int? id, string returnUrl = null)
        {
            if (id == null)
                return NotFound();

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                return NotFound();

            ViewBag.ReturnUrl = returnUrl;

            return View(review);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl = null)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if(review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
