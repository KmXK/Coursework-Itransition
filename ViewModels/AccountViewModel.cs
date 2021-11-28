using System.Collections.Generic;
using Coursework.Domain.Entities;
using Coursework.Models;

namespace Coursework.ViewModels
{
    public class AccountViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
