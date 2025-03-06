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
    public class ReturnBookUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public ReturnBookUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(UserEntity user, BookEntity book)
        {
            var usEntity = await db.userRepository.GetByIdAsync(user.Id) ??
                throw new ObjectNotFoundException($"Error on ReturnBookUseCase: no such user, id = {user.Id}");

            var bookEntity = await db.bookRepository.GetByIdAsync(book.Id) ??
                throw new ObjectNotFoundException($"Error on ReturnBookUseCase: no such book, id = {book.Id}");

            usEntity.Books.Remove(bookEntity);
            bookEntity.PickDate = null;
            bookEntity.ReturnDate = null;

            await db.SaveChangesAsync();
        }
    }
}
