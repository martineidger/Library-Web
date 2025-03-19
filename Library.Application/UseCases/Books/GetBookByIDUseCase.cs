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
    public class GetBookByIDUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetBookByIDUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<BookModel> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var bookEntity =  await db.bookRepository.GetByIdAsync(id, cancellationToken)??
                throw new ObjectNotFoundException($"Error on GetBookByIDUseCase: no such book, id = {id}");

            return mapper.Map<BookModel>(bookEntity);
        }
    }
}
