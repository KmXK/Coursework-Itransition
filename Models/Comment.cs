using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
