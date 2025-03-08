using Library.Core.Abstractions.ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authorisation
{
    public class RefreshTokenUseCase
    {
        private readonly ITokenService tokenService;

        public RefreshTokenUseCase(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public async Task<string> ExecuteAsync(string refreshToken)
        {
            var newToken = tokenService.RefreshToken(refreshToken);
            return newToken;
        }
    }
}
