using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Coursework.Models;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
            if (ModelState.IsValid)
            {
                var group = await _context.ReviewGroups.FirstOrDefaultAsync(g => g.Name == model.SelectedGroup);
                if (group == null)
                {
                    ModelState.AddModelError("SelectedGroup", "Указанная группа не существует");
                    return View(model);
                }

                var review = new Review()
                {
                    Title = model.Title,
                    Text = model.Text,
                    AuthorRating = model.Rating,
                    Author = await _userManager.FindByIdAsync(_userManager.GetUserId(User)),
                    Group = group,
                    Images = new List<ImageUrl>()
                };

                string path = "/Files/";
                foreach (var modelUploadFile in model.UploadFiles)
                {
                    var imageUrl = new ImageUrl()
                    {
                        Url = Path.Combine(path, Path.ChangeExtension(Path.GetRandomFileName(),
                            Path.GetExtension(modelUploadFile.FileName)))
                    };
                    await using (var fs = new FileStream(_webHostEnvironment.WebRootPath +
                                                         imageUrl.Url, FileMode.Create))
                    {
                        await modelUploadFile.CopyToAsync(fs);
                    }
                    review.Images.Add(imageUrl);
                }

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            model.Groups = _context.ReviewGroups.Select(g => g.Name).ToList();
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
                .Include(r=>r.Images)
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
