using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
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
        public async Task ExecuteAsync(UserEntity user, BookEntity book)
        {
            var usEntity = await db.userRepository.GetByIdAsync(user.Id) ??
                throw new ObjectNotFoundException($"Error on GetBookOnHandsUseCase: no such user, id = {user.Id}");

            var bookEntity = await db.bookRepository.GetByIdAsync(book.Id) ??
                throw new ObjectNotFoundException($"Error on GetBookOnHandsUseCase: no such book, id = {book.Id}");


            bookEntity.PickDate = DateTime.UtcNow;
            bookEntity.ReturnDate = DateTime.UtcNow.AddDays(10);
            usEntity.Books.Add(bookEntity);

            await db.SaveChangesAsync();


        }
    }
}
