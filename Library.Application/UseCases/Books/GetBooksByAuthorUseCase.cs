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
    public class GetBooksByAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetBooksByAuthorUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<List<BookEntity>> ExecuteAsync(Guid authorId)
        {
            var booksList = await db.bookRepository.GetAllAsync()??
                throw new ObjectNotFoundException($"Error on GetBooksByAuthorUseCase: list was empty");

            return booksList.Where(b => b.Author.Id == authorId).ToList() ?? new List<BookEntity>();
        }
    }
}
