using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class ReviewRating
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int Rating { get; set; }
        public int ReviewId { get; set; }
    }
}
