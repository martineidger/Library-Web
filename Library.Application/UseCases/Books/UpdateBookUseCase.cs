using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
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


        public UpdateBookUseCase(ILibraryUnitOfWork db, IMapper mapper, IImageService imageService)
        {
            this.db = db;
            this.mapper = mapper;
            this.imageService = imageService;

        }
        public async Task ExecuteAsync(BookModel book, CancellationToken cancellationToken)
        {
            var bookEnt = await db.bookRepository.GetByIdAsync(book.Id, cancellationToken)
                ?? throw new ObjectNotFoundException($"Error on UpdateBookUseCase: no such book, id = {book.Id}");
            Console.WriteLine("UPDATE");

            if (await db.bookRepository.GetByISBNAsync(book.ISBN, cancellationToken) != null)
                throw new ObjectAlreadyExistsException($"Book with ISBN {book.ISBN} already exists.");

            if (book.ImgFile != null)
            {
                Console.WriteLine("Added image");
                bookEnt.ImgPath = await imageService.SaveAsync(book.ImgFile);
            }

            bookEnt.ISBN = book.ISBN;
            bookEnt.Title = book.Title;
            bookEnt.Description = book.Description;
            bookEnt.Genre = book.Genre;
            bookEnt.PickDate = book.PickDate;
            bookEnt.ReturnDate = book.ReturnDate;
            
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
