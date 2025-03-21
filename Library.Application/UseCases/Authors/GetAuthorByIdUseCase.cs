﻿using AutoMapper;
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
    public class GetAuthorByIdUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAuthorByIdUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<AuthorModel> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var authorEntity =  await db.authorRepository.GetByIdAsync(id, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetAuthorByIdUseCase: no such author, id = {id}");

            return mapper.Map<AuthorModel>(authorEntity);
        }
    }
}
