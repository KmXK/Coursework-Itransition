using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Coursework.Models;
using Microsoft.AspNetCore.Identity;

namespace Coursework.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string AvatarUrl { get; set; }
        [InverseProperty("User")]
        public ICollection<UserRating> Ratings { get; set; }
    }
}