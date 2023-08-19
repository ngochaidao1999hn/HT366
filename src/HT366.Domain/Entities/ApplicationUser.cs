using HT366.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace HT366.Domain.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateOnly? DateOfBirth { get; set; }
        public SexEnum Sex { get; set; }
        public string? Contact { get; set; }
        public string? Description { get; set; }
        public string? SchoolName { get; set; }
        public bool IsVerified { get; set; } = false;
        public List<Exam>? Exams { get; set; }
    }
}
