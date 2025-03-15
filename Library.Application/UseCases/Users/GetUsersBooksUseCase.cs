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

namespace Library.Application.UseCases.Users
{
    public class GetUsersBooksUseCase : BasePagedUseCase<BookModel, BookEntity>
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetUsersBooksUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<PagedItems<BookModel>> ExecuteAsync(Guid authorId, int page, int size, CancellationToken cancellationToken)
        {
            var authorEnt = await db.userRepository.GetByIdAsync(authorId, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetUsersBooksUseCase: no user (id: {authorId})");

            var booksList = await db.userRepository.GetUsersBooks(authorId, page, size, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetUsersBooksUseCase: empty list");

            return MapPagedItems(booksList, mapper);
        }
    }
}
