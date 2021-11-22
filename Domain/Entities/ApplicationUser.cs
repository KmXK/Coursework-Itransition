using System;
using System.Collections.Generic;
using Coursework.Models;
using Microsoft.AspNetCore.Identity;

namespace Coursework.Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string AvatarUrl { get; set; }
        public ICollection<UserRating> Ratings { get; set; }
    }
}
