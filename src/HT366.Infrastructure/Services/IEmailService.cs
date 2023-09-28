using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Infrastructure.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, object data, string templateId);
    }
}