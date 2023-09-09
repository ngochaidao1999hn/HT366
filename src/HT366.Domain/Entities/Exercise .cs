using HT366.Domain.Common;
using HT366.Domain.Common.Enums;

namespace HT366.Domain.Entities
{
    public class Exercise : BaseEntity, ISoftDeleted
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Content { get; set; } = default!;
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public LevelEnum Level { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; }
        public List<File>? Files { get; set; }
    }
}