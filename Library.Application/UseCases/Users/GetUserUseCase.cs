using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class GetUserUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetUserUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<UserEntity> ExecuteAsync(string email)
        {
            return await db.userRepository.GetByEmailAsync(email)??
                throw new ObjectNotFoundException($"Error on GetUserUseCase: no such user, email = {email}");
        }
    }
}
