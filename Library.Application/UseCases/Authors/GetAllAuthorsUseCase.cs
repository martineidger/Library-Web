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
    public class GetAllAuthorsUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetAllAuthorsUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<List<AuthorEntity>> ExecuteAsync()
        {
            return await db.authorRepository.GetAllAsync() ??
                throw new ObjectNotFoundException($"Error on GetAllAuthorsUseCase: list was empty");
        }
    }
}
