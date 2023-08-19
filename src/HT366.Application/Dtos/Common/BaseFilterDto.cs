using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Common
{
    public class BaseFilterDto
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
