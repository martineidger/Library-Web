﻿using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Books
{
    public  class GetAllBooksUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetAllBooksUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }

        public async Task<List<BookEntity>> ExecuteAsync()
        {
            return await db.bookRepository.GetAllAsync() ??
                throw new ObjectNotFoundException($"Error on GetAllBooksUseCase: list was empty");
        }
    }
}
