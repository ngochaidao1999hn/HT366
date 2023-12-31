﻿using HT366.Domain.Common;
using HT366.Domain.Common.Enums;

namespace HT366.Domain.Entities
{
    public class Exam : BaseEntity, ISoftDeleted
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
        public string? RejectReason { get; set; }
        public List<File>? Files { get; set; }
        public Guid CreatedBy { get; set; }
        public ApplicationUser User { get; set; } = default!;
    }
}