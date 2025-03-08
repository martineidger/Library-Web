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
using Library.Core.Exceptions;

namespace Library.Application.UseCases.Books
{
    public class AddBookUseCase
    {
        private readonly IImageService imageService;
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        //private string defFileName = "/App/BookCovers/Def.jpg";
        private string defFileName = "D:\\Code\\Library\\BookCovers\\def.jpg";

        public AddBookUseCase(IImageService imageService, ILibraryUnitOfWork db, IMapper mapper)
        {
            this.imageService = imageService;
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(BookModel newBook)
        {
            var bookEnt = mapper.Map<BookEntity>(newBook);

            if (newBook.ImgFile != null)
                bookEnt.ImgPath = await imageService.SaveAsync(newBook.ImgFile);
            else bookEnt.ImgPath = defFileName;
            
            bookEnt.Author = await db.authorRepository.GetByIdAsyhnc(newBook.AuthorID)??
                throw new ObjectNotFoundException($"Error on AddBookUseCase: no such author ({newBook.AuthorID})");

            var id = await db.bookRepository.AddAsync(bookEnt);
            await db.SaveChangesAsync();

            return id;
        }
    }
}
