using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authors
{
    public class AddAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public AddAuthorUseCase( ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<Guid> ExecuteAsync(AuthorEntity newAuthor)
        {
            var id = await db.authorRepository.AddAsync(newAuthor);
            await db.SaveChangesAsync();

            return id;
        }
    }
}
