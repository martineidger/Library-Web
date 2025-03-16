using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Books
{
    public class UpdateBookUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        private string defFileName;

        public UpdateBookUseCase(ILibraryUnitOfWork db, IMapper mapper, IImageService imageService)
        {
            this.db = db;
            this.mapper = mapper;
            this.imageService = imageService;

            defFileName = Path.Combine("covers", "def.jpg");
        }
        public async Task ExecuteAsync(BookModel book, CancellationToken cancellationToken)
        {
            if (await db.bookRepository.GetByIdAsync(book.Id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on UpdateBookUseCase: no such book, id = {book.Id}");

            var bookEnt = mapper.Map<BookEntity>(book);

            bookEnt.ISBN = book.ISBN;
            bookEnt.Title = book.Title;
            bookEnt.Description = book.Description;
            bookEnt.Genre = book.Genre;
            bookEnt.PickDate = book.PickDate;
            bookEnt.ReturnDate = book.ReturnDate;

            if (book.ImgFile != null)
            {
                Console.WriteLine("Added image");
                bookEnt.ImgPath = await imageService.SaveAsync(book.ImgFile);
            }
            else bookEnt.ImgPath = defFileName;
            
            bookEnt.Author = await db.authorRepository.GetByIdAsyhnc(book.AuthorID, cancellationToken) ??
                throw new ObjectNotFoundException($"Error on AddBookUseCase: no such author ({book.AuthorID})");

            //await db.bookRepository.UpdateAsync(bookEnt, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
