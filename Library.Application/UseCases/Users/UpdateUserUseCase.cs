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
    internal class UpdateUserUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public UpdateUserUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(UserEntity user)
        {
            if (db.userRepository.GetByIdAsync(user.Id) == null)
                throw new ObjectNotFoundException($"Error on UpdateUserUseCase: no such user, id = {user.Id}");

            await db.userRepository.UpdateAsync(user);
            await db.SaveChangesAsync();
        }
    }
}
