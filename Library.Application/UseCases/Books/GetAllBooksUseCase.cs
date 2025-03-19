using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Books
{
    public  class GetAllBooksUseCase : BasePagedUseCase<BookModel, BookEntity>
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAllBooksUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<PagedItems<BookModel>> ExecuteAsync(int page, int size, CancellationToken cancellationToken)
        {
            var bookEntityes = await db.bookRepository.GetAllAsync(page, size, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetAllBooksUseCase: list was empty");

            return MapPagedItems(bookEntityes, mapper);
        }
    }
}
