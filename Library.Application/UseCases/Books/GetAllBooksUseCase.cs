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
    public  class GetAllBooksUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAllBooksUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<BookModel>> ExecuteAsync()
        {
            var bookEntityes = await db.bookRepository.GetAllAsync() ??
                throw new ObjectNotFoundException($"Error on GetAllBooksUseCase: list was empty");

            return mapper.Map<List<BookModel>>(bookEntityes);
        }
    }
}
