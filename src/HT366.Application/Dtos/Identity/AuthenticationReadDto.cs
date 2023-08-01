using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Dtos.Identity
{
    public class AuthenticationReadDto
    {
        public string? AccessToken { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
