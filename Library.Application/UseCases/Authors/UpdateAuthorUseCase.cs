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

namespace Library.Application.UseCases.Authors
{
    public class UpdateAuthorUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public UpdateAuthorUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task ExecuteAsync(AuthorModel author, CancellationToken cancellationToken)
        {
            if (await db.authorRepository.GetByIdAsync(author.Id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on UpdateAuthorUseCase: no such author, id = {author.Id}");

            await db.authorRepository.UpdateAsync(mapper.Map<AuthorEntity>(author), cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
