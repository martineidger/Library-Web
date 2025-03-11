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
    public class GetBooksByTitleUseCase : BasePagedUseCase<BookModel, BookEntity>
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetBooksByTitleUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<PagedItems<BookModel>> ExecuteAsync(string title, int page, int size)
        {
            var booksList = await db.bookRepository.GetByTitleAsync(title, page, size) ??
                throw new ObjectNotFoundException($"Error on GetBooksByTitleUseCase: list was empty");


            return MapPagedItems(booksList, mapper);
        }
    }
}
