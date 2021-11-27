using System.Linq;
using Coursework.Domain;
using Coursework.Models;
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
            var reviews = _context.Reviews
                .Include(r => r.Ratings)
                .Include(r => r.Author)
                .ToList();

            return View(reviews);
        }

        public IActionResult Search(string searchString)
        {
            var reviews = _context.Reviews
                .Where(r=>r.SearchVector.Matches(searchString))
                .Include(r => r.Author)
                .Include(r => r.Ratings)
                .ToList();

            return View(reviews);
        }
    }
}
