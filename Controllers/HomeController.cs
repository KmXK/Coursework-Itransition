using System.Linq;
using Coursework.Domain;
using Coursework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lastReviews = _context.Reviews
                .Include(r => r.Ratings)
                .Include(r => r.Author)
                .Include(r=>r.Group)
                .ToList();

            var popularReviews = lastReviews
                .OrderByDescending(r => r.Ratings.Any() ? r.Ratings.Average(rating => rating.Rating) : 0 )
                .Take(5);

            return View(new MainPageViewModel()
            {
                LastReview = lastReviews,
                PopularReviews = popularReviews
            });
        }

        public IActionResult Search(string searchString)
        {
            var reviews = _context.Reviews
                .Include(r=>r.Comments)
                .Where(r=>
                    r.SearchVector.Matches(searchString) ||
                    r.Comments.Any(c=>c.SearchVector.Matches(searchString)))
                .Include(r => r.Author)
                .Include(r => r.Ratings)
                .Include(r=>r.Group)
                .ToList();

            return View(reviews);
        }
    }
}
