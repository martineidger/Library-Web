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
    public class GetBookByIDUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetBookByIDUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<BookEntity> ExecuteAsync(Guid id)
        {
            return await db.bookRepository.GetByIdAsync(id)??
                throw new ObjectNotFoundException($"Error on GetBookByIDUseCase: no such book, id = {id}");
        }
    }
}
