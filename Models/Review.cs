using System.Collections.Generic;
using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class Review
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public string Title{ get; set; }
        public string Text { get; set; }
        public int AuthorRating { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
