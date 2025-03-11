using Library.Core.Abstractions;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public DeleteAuthorUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(Guid id)
        {
            if (await db.authorRepository.GetByIdAsyhnc(id) == null)
                throw new ObjectNotFoundException($"Error on DeleteAuthorUseCase: no such author, id = {id}");

            db.authorRepository.Delete(id);
            await db.SaveChangesAsync();
        }
    }

}
