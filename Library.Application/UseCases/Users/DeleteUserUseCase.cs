using Library.Core.Abstractions;
using Library.Application.Exceptions;
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
        public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var userToDelete = await db.userRepository.GetByIdAsync(id, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on DeleteUserUseCase: no such user, id = {id}");

            db.userRepository.Delete(userToDelete);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
