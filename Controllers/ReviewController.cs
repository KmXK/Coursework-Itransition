using System.Linq;
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
                var rewiew = new Review()
                {
                    Title = model.Title,
                    Text = model.Text,
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

            var review = await _context.Reviews.Include(r => r.Author)
                .FirstOrDefaultAsync(r => r.Id == id);

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
                review.Text = model.Text;

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

            var review = await _context.Reviews
                .Include(r=>r.Author)
                .Include(r=>r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                var rating = review.Ratings.FirstOrDefault(r => r.User == user);
                ViewBag.Rating = rating?.Rating ?? 0;
            }

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> SetRating(int reviewId, int rating)
        {
            if (User.Identity.IsAuthenticated)
            {
                var review = await _context.Reviews
                    .Include(r => r.Author)
                    .Include(r => r.Ratings)
                    .FirstOrDefaultAsync(r => r.Id == reviewId);
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                var reviewRating = review.Ratings.FirstOrDefault(r => r.User == user);
                if (reviewRating != null)
                    reviewRating.Rating = rating;
                else
                {
                    review.Ratings.Add(new ReviewRating()
                    {
                        User = user,
                        Rating = rating
                    });
                }
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
                return NotFound();
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
