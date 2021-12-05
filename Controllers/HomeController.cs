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
                .OrderByDescending(r => r.Ratings.Sum(rating => rating.Rating))
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
                .Where(r=>r.SearchVector.Matches(EF.Functions.ToTsQuery(searchString)))
                .Include(r => r.Author)
                .Include(r => r.Ratings)
                .Include(r=>r.Group)
                .ToList();

            return View(reviews);
        }
    }
}
