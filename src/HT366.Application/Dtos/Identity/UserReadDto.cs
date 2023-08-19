using HT366.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Identity
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public SexEnum Sex { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Description { get; set; }
        public string? Contact { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = default!;
        public string? SchoolName { get; set; }
    }
}
