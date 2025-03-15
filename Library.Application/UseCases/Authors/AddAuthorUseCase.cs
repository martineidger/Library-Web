using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
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
        private readonly IMapper mapper;

        public AddAuthorUseCase( ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(AuthorModel author, CancellationToken cancellationToken)
        {
            var newAuthor = mapper.Map<AuthorEntity>(author);

            if (await db.authorRepository.GetByFullNAMe(author.FirstName, author.Surname, cancellationToken) != null)
                throw new ObjectAlreadyExistsException($"Author {author.FirstName} {author.Surname} already exists.");

            var id = await db.authorRepository.AddAsync(newAuthor, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}
