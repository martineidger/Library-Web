using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Books
{
    public class DeleteBookUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public DeleteBookUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (await db.bookRepository.GetByIdAsync(id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on DeleteBookUseCase: no such book, id = {id}");

            await db.bookRepository.DeleteAsync(id, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
