using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Domain.Common
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }

    }
}
