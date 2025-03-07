using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Models
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string ExpiresIn { get; set; }
    }
}
