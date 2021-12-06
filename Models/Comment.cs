using System;
using Coursework.Domain.Entities;
using NpgsqlTypes;

namespace Coursework.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public DateTime PostTime { get; set; }
        public string Text { get; set; }
        public int ReviewId { get; set; }

        public NpgsqlTsVector SearchVector { get; set; }
    }
}
