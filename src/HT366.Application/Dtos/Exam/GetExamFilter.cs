using HT366.Application.Dtos.Common;
using HT366.Domain.Common.Enums;

namespace HT366.Application.Dtos.Exam
{
    public class GetExamFilter : BaseFilterDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? CreateDate { get; set; }
        public StatusEnum? Status { get; set; }
        public Guid? CateId { get; set; }
        public LevelEnum? Level { get; set; }
        public string? CreatedBy { get; set; }
    }
}