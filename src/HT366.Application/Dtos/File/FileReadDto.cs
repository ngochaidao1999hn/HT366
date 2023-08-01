using HT366.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.File
{
    public class FileReadDto
    {
        public string Url { get; set; } = default!;
        public StatusEnum Status { get; set; }
    }
}
