using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task<string> SaveFile(IFormFile file)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
