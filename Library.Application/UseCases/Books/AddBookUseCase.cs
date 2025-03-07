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

namespace Library.Application.UseCases.Books
{
    public class AddBookUseCase
    {
        private readonly IImageService imageService;
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public AddBookUseCase(IImageService imageService, ILibraryUnitOfWork db, IMapper mapper)
        {
            this.imageService = imageService;
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(BookModel newBook, IFormFile bookImg)
        {
            newBook.ImgPath = await imageService.SaveAsync(bookImg);

            var id = await db.bookRepository.AddAsync(mapper.Map<BookEntity>(newBook));
            await db.SaveChangesAsync();

            return id;
        }
    }
}
