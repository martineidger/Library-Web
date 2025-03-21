﻿using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class GetBookOnHandsUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public GetBookOnHandsUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<DateTime> ExecuteAsync(Guid userId, Guid bookId, CancellationToken cancellationToken)
        {
            var usEntity = await db.userRepository.GetByIdAsync(userId, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetBookOnHandsUseCase: no such user, id = {userId}");

            var bookEntity = await db.bookRepository.GetByIdAsync(bookId, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on GetBookOnHandsUseCase: no such book, id = {bookId}");


            bookEntity.PickDate = DateTime.UtcNow;
            bookEntity.ReturnDate = DateTime.UtcNow.AddDays(10);
            usEntity.Books.Add(bookEntity);

            await db.SaveChangesAsync(cancellationToken);

            return (DateTime)bookEntity.ReturnDate;

        }
    }
}
