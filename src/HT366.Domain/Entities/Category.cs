using HT366.Domain.Common;

namespace HT366.Domain.Entities
{
    public class Category : BaseEntity, ISoftDeleted
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<Lesson>? Lessons { get; set; }
        public List<Exercise>? Exercises { get; set; }
        public List<Exam>? Exams { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; }
    }
}