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
    public class GetAuthorsWithoutPaginationUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAuthorsWithoutPaginationUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<AuthorModel>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var authEntities = await db.authorRepository.GetAllAsync(cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetAuthorsWithoutPaginationUseCase: list was empty");

            return mapper.Map<List<AuthorModel>>(authEntities);
            
        }
    }
}
