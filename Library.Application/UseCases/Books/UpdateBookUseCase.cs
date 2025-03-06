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
    public class UpdateBookUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public UpdateBookUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(BookEntity book)
        {
            if (db.bookRepository.GetByIdAsync(book.Id) == null)
                throw new ObjectNotFoundException($"Error on UpdateBookUseCase: no such book, id = {book.Id}");

            await db.bookRepository.UpdateAsync(book);
            await db.SaveChangesAsync();
        }
    }
}
