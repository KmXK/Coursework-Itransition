using System.Linq;
using System.Threading.Tasks;
using Coursework.Domain;
using Coursework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Services
{
    public class LikesService
    {
        private readonly ApplicationDbContext _context;

        public LikesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetUserLikes(ApplicationUser user)
        {
            if (user == null)
                return 0;

            return _context.Reviews
                .Include(r => r.Author)
                .Include(r => r.Likes)
                .Where(r => r.Author == user)
                .Sum(r => r.Likes.Sum(l => l.Rating));
        }
    }
}
