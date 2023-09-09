namespace HT366.Application.Dtos.Identity
{
    public class AuthenticationReadDto
    {
        public string? AccessToken { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}