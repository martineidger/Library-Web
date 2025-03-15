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
    public class GetBookByISBNUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetBookByISBNUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<BookModel> ExecuteAsync(string isbn, CancellationToken cancellationToken)
        {
            var bookEntity = await db.bookRepository.GetByISBNAsync(isbn, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetByISBNAsync: no such book, ISBN = {isbn}");

            return mapper.Map<BookModel>(bookEntity);
        }
    }
}
