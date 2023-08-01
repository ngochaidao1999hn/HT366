using HT366.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Exam
{
    public class CreateExamDto
    {
        public string Name { get; set; } = default!; 
        public string Description { get; set; } = default!;
        public string Content { get; set; } = default!;
        public LevelEnum Level { get; set; }
        public Guid CateId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
