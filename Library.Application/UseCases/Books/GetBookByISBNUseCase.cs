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
    public class GetBookByISBNUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetBookByISBNUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<BookEntity> ExecuteAsync(string isbn)
        {
            return await db.bookRepository.GetByISBNAsync(isbn) ??
                throw new ObjectNotFoundException($"Error on GetByISBNAsync: no such book, ISBN = {isbn}");
        }
    }
}
