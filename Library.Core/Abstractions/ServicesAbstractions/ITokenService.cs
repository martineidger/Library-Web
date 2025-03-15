using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions.ServicesAbstractions
{
    public interface ITokenService
    {
        public string GetAccesToken(Guid id, string role);
        public string GetRefreshToken(Guid id);
        public string RefreshToken(string refreshToken, CancellationToken cancellationToken);
    }
}
