namespace Coursework.Models
{
    public class ReviewRating
    {
        public int Id { get; set; }
        public Review Review { get; set; }
        public int Rating { get; set; }
    }
}
