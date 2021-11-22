using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int Rating { get; set; }
    }
}
