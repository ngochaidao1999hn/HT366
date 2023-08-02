using HT366.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Exam
{
    public class GetExamFilter
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? CreateDate { get; set; }
        public StatusEnum? Status { get; set; }
    }
}
