using HT366.Application.Dtos.File;
using HT366.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Exam
{
    public class ExamReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Content { get; set; } = default!;
        public StatusEnum Status { get; set; }
        public Guid CreatedBy { get; set; }
        public IEnumerable<FileReadDto>? Files { get; set; }
    }
}
