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
        public async Task ExecuteAsync(Guid id)
        {
            if (db.bookRepository.GetByIdAsync(id) == null)
                throw new ObjectNotFoundException($"Error on DeleteBookUseCase: no such book, id = {id}");

            db.bookRepository.Delete(id);
            await db.SaveChangesAsync();
        }
    }
}
