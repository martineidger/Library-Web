using AutoMapper;
using Library.Application.Models;
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
        private readonly IMapper mapper;

        public UpdateBookUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task ExecuteAsync(BookModel book)
        {
            if (await db.bookRepository.GetByIdAsync(book.Id) == null)
                throw new ObjectNotFoundException($"Error on UpdateBookUseCase: no such book, id = {book.Id}");

            await db.bookRepository.UpdateAsync(mapper.Map<BookEntity>(book));
            await db.SaveChangesAsync();
        }
    }
}
