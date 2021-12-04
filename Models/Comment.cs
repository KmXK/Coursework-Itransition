using System;
using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public DateTime PostTime { get; set; }
        public string Text { get; set; }
    }
}
