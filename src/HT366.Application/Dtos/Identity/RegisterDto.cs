namespace HT366.Application.Dtos.Identity
{
    public class RegisterDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? SchoolName { get; set; }
    }
}