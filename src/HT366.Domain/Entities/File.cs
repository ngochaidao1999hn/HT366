using HT366.Domain.Common;
using HT366.Domain.Common.Enums;

namespace HT366.Domain.Entities
{
    public class File : BaseEntity, ISoftDeleted
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; }
        public string Url { get; set; } = default!;
        public Guid? ExamId { get; set; }
        public Exam? Exam { get; set; }
        public Guid? ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        public Guid? LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
    }
}