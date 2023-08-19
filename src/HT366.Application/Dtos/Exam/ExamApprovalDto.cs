using HT366.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Exam
{
    public class ExamApprovalDto
    {
        public bool IsApproved { get; set; }
        public string? RejectReason { get; set; }
    }
}
