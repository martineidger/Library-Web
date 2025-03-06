using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions.ServicesAbstractions
{
    public interface IImageService
    {
        Task<string> SaveAsync(IFormFile image);
        Task<bool> DeleteAsync(string path);
    }
}
