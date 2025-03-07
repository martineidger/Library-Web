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
    public class GetBooksByAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetBooksByAuthorUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<BookModel>> ExecuteAsync(Guid authorId)
        {
            var booksList = await db.bookRepository.GetAllAsync()??
                throw new ObjectNotFoundException($"Error on GetBooksByAuthorUseCase: list was empty");

           return mapper.Map<List<BookModel>>(booksList.Where(b => b.Author.Id == authorId).ToList() ?? new());
        }
    }
}
