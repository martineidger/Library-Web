using Library.Core.Abstractions.ServicesAbstractions;
using Library.Core.Abstractions;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Library.Application.Models;
using Library.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace Library.Application.UseCases.Books
{
    public class AddBookUseCase
    {
        private readonly IWebHostEnvironment env;
        private readonly IImageService imageService;
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        private string defFileName; 

        public AddBookUseCase(IWebHostEnvironment env, IImageService imageService, ILibraryUnitOfWork db, IMapper mapper)
        {
            this.env = env;
            this.imageService = imageService;
            this.db = db;
            this.mapper = mapper;

            defFileName = Path.Combine("covers", "def.jpg");
        }
        public async Task<Guid> ExecuteAsync(BookModel newBook, CancellationToken cancellationToken)
        {
            var bookEnt = mapper.Map<BookEntity>(newBook);

            if (await db.bookRepository.GetByISBNAsync(newBook.ISBN, cancellationToken) != null)
                throw new ObjectAlreadyExistsException($"Book with ISBN {newBook.ISBN} already exists.");

            if (newBook.ImgFile != null)
            {
                Console.WriteLine("Added image");
                bookEnt.ImgPath = await imageService.SaveAsync(newBook.ImgFile);
            }
            else bookEnt.ImgPath = defFileName;
            
            bookEnt.Author = await db.authorRepository.GetByIdAsync(newBook.AuthorID, cancellationToken)??
                throw new ObjectNotFoundException($"Error on AddBookUseCase: no such author ({newBook.AuthorID})");

            var addedBook = await db.bookRepository.AddAsync(bookEnt, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return addedBook.Id;
        }
    }
}
