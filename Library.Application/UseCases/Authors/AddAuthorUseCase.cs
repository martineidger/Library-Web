using AutoMapper;
using Library.Application.Models;
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
        private readonly IMapper mapper;

        public AddAuthorUseCase( ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(AuthorModel author)
        {
            var newAuthor = mapper.Map<AuthorEntity>(author);

            var id = await db.authorRepository.AddAsync(newAuthor);
            await db.SaveChangesAsync();

            return id;
        }
    }
}
