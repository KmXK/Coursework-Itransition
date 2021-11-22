using System.Threading.Tasks;
using Coursework.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Controllers
{
    [Authorize]
    public class ReviewController: Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rewiew = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (rewiew == null)
                return NotFound();

            return View(rewiew);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rewiew = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            _context.Reviews.Remove(rewiew);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
