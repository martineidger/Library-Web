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
        public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (await db.authorRepository.GetByIdAsyhnc(id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on DeleteAuthorUseCase: no such author, id = {id}");

            await db.authorRepository.DeleteAsync(id, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }

}
