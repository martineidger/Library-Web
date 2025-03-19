using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authorisation
{
    public class LoginUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly ITokenService tokenService;

        public LoginUseCase(ILibraryUnitOfWork db, ITokenService tokenService)
        {
            this.db = db;
            this.tokenService = tokenService;
        }
        public async Task<TokenModel> ExecuteAsync(string login, string password, CancellationToken cancellationToken)
        {
            var user = await db.userRepository.GetByEmailAsync(login, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on LoginUseCase: no such user (email {login})");

            if (!BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
                throw new UnauthorizedAccessException("Wrong password");

            var accessToken = tokenService.GetAccesToken(user.Id, user.Role);
            var refreshToken = tokenService.GetRefreshToken(user.Id);

            return new()
            { 
                AccessToken = accessToken,
                RefreshToken = refreshToken,    
                UserId = user.Id
            };

        }
    }
}
