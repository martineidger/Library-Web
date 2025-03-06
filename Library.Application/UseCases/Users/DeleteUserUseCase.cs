using Library.Core.Abstractions;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class DeleteUserUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public DeleteUserUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(string email)
        {
            if (db.userRepository.GetByEmailAsync(email) == null)
                throw new ObjectNotFoundException($"Error on DeleteUserUseCase: no such user, email = {email}");

            db.userRepository.Delete(email);
            await db.SaveChangesAsync();
        }
    }
}
