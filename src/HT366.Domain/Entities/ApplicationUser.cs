using Microsoft.AspNetCore.Identity;

namespace HT366.Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? SchoolName { get; set; }
    }
}
