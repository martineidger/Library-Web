using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Abstractions;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Application.UseCases.Books
{
    public class AddBookUseCase
    {
        private readonly IImageService imageService;
        private readonly ILibraryUnitOfWork db;

        public AddBookUseCase(IImageService imageService, ILibraryUnitOfWork db)
        {
            this.imageService = imageService;
            this.db = db;
        }
        public async Task<Guid> ExecuteAsync(BookEntity newBook, IFormFile bookImg)
        {
            //add validations
            newBook.ImgPath = await imageService.SaveAsync(bookImg);

            var id = await db.bookRepository.AddAsync(newBook);
            await db.SaveChangesAsync();

            return id;
        }
    }
}
