using HT366.Domain.Common.Enums;

namespace HT366.Application.Dtos.File
{
    public class FileReadDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = default!;
        public StatusEnum Status { get; set; }
    }
}