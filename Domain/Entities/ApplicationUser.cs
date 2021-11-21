using System;
using Microsoft.AspNetCore.Identity;

namespace Coursework.Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string AvatarUrl { get; set; }
    }
}
