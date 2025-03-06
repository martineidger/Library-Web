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
    public class UpdateAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public UpdateAuthorUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(AuthorEntity author)
        {
            if (db.authorRepository.GetByIdAsyhnc(author.Id) == null)
                throw new ObjectNotFoundException($"Error on UpdateAuthorUseCase: no such author, id = {author.Id}");

            await db.authorRepository.UpdateAsync(author);
            await db.SaveChangesAsync();
        }
    }
}
