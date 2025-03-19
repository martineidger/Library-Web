using Library.Core.Abstractions;
using Library.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public DeleteAuthorUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var authorToDelete = await db.authorRepository.GetByIdAsync(id, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on DeleteAuthorUseCase: no such author, id = {id}");

            db.authorRepository.Delete(authorToDelete);
            await db.SaveChangesAsync(cancellationToken);
        }
    }

}
