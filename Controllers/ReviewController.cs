using System;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.Models;
using Coursework.ViewModels;
using Ganss.XSS;
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
            return View(new CreateRewiewViewModel()
            {
                Groups = _context.ReviewGroups.Select(g=>g.Name).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRewiewViewModel model)
        {
            model.Groups = _context.ReviewGroups.Select(g => g.Name).ToList();
            if (ModelState.IsValid)
            {
                var group = await _context.ReviewGroups.FirstOrDefaultAsync(g => g.Name == model.SelectedGroup);
                if (group == null)
                {
                    ModelState.AddModelError("SelectedGroup", "Указанная группа не существует");
                    return View(model);
                }

                var rewiew = new Review()
                {
                    Title = model.Title,
                    Text = model.Text,
                    AuthorRating = model.Rating,
                    Author = await _userManager.FindByIdAsync(_userManager.GetUserId(User)),
                    Group = group
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

            if (review.Author != user && !User.IsInRole("Admin"))
                return NotFound();
            return View(new EditReviewViewModel()
            {
                Id = review.Id,
                Text = review.Text,
                Title = review.Title,
                Rating = review.AuthorRating,
                Groups = _context.ReviewGroups.Select(g=>g.Name).ToList()
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
                review.Group = await _context.ReviewGroups.FirstOrDefaultAsync(g => g.Name == model.SelectedGroup);

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
                .Include(r=>r.Group)
                .Include(r=>r.Ratings)
                .Include(r=>r.Likes)
                .Include(r=>r.Comments)
                .ThenInclude(c=>c.Author)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                var rating = review.Ratings.FirstOrDefault(r => r.User == user);
                ViewBag.Rating = rating?.Rating ?? 0;

                ViewBag.IsLiked = review.Likes.FirstOrDefault(l => l.User == user)?.Rating == 1;
            }

            ViewBag.Likes = review.Likes.Sum(l => l.Rating);

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
            var review = await _context.Reviews
                .Include(r=>r.Comments)
                .Include(r=>r.Ratings)
                .Include(r=>r.Likes)
                .FirstOrDefaultAsync(r => r.Id == id);
            if(review != null)
            {
                review.Comments.Clear();
                review.Ratings.Clear();
                review.Likes.Clear();
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLike(int reviewId)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var review = await _context.Reviews
                .Include(r => r.Likes)
                .FirstOrDefaultAsync(r => r.Id == reviewId);
            var rating = review.Likes.FirstOrDefault(l => l.User == user);
            if (rating == null)
            {
                review.Likes.Add(new UserRating()
                {
                    Rating = 1,
                    User = user
                });
            }
            else
            {
                rating.Rating = rating.Rating == 1 ? 0 : 1;
            }

            await _context.SaveChangesAsync();

            return Content(review.Likes.Sum(l => l.Rating).ToString());
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(int reviewId, string text)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var review = await _context.Reviews
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null || user == null)
                return NotFound();

            if (text == null)
                return NoContent();

            Comment comment = new Comment()
            {
                Author = user,
                PostTime = DateTime.UtcNow,
                Text = text
            };
            review.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return PartialView("CommentPartial", comment);
        }
    }
}
