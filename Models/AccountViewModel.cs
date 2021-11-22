using System.Collections.Generic;
using Coursework.Domain.Entities;

namespace Coursework.Models
{
    public class AccountViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
