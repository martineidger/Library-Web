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

namespace Library.Application.UseCases.Authors
{
    public class GetAllAuthorsUseCase: BasePagedUseCase<AuthorModel, AuthorEntity>
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAllAuthorsUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<PagedItems<AuthorModel>> ExecuteAsync(int page, int size, CancellationToken cancellationToken)
        {
            var authEntities =  await db.authorRepository.GetAllAsync(page, size, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetAllAuthorsUseCase: list was empty");

            return MapPagedItems(authEntities, mapper);
        }


    }
}
