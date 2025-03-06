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
    public class GetAuthorByIdUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetAuthorByIdUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<AuthorEntity> ExecuteAsync(Guid id)
        {
            return await db.authorRepository.GetByIdAsyhnc(id) ??
                throw new ObjectNotFoundException($"Error on GetAuthorByIdUseCase: no such author, id = {id}");
        }
    }
}
