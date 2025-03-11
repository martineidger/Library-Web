using AutoMapper;
using FluentValidation;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IValidator<BookContract> validator;
        private readonly IMapper mapper;
        private readonly AddBookUseCase addBookUseCase;
        private readonly GetAllBooksUseCase getAllBooksUseCase;
        private readonly DeleteBookUseCase deleteBookUseCase;
        private readonly GetBookByIDUseCase getBookByIDUseCase;
        private readonly GetBookByISBNUseCase getBookByISBNUseCase;
        private readonly GetBooksByAuthorUseCase getBooksByAuthorUseCase;
        private readonly UpdateBookUseCase updateBookUseCase;
        private readonly GetBooksByTitleUseCase getBooksByTitleUseCase;

        public BooksController(
            IValidator<BookContract> validator,
            IMapper mapper,
            AddBookUseCase addBookUseCase,
            GetAllBooksUseCase getAllBooksUseCase,
            DeleteBookUseCase deleteBookUseCase,
            GetBookByIDUseCase getBookByIDUseCase,
            GetBookByISBNUseCase getBookByISBNUseCase,
            GetBooksByAuthorUseCase getBooksByAuthorUseCase,
            UpdateBookUseCase updateBookUseCase, 
            GetBooksByTitleUseCase getBooksByTitleUseCase)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.addBookUseCase = addBookUseCase;
            this.getAllBooksUseCase = getAllBooksUseCase;
            this.deleteBookUseCase = deleteBookUseCase;
            this.getBookByIDUseCase = getBookByIDUseCase;
            this.getBookByISBNUseCase = getBookByISBNUseCase;
            this.getBooksByAuthorUseCase = getBooksByAuthorUseCase;
            this.updateBookUseCase = updateBookUseCase;
            this.getBooksByTitleUseCase = getBooksByTitleUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var books = await getAllBooksUseCase.ExecuteAsync(page, size);
            
            return Ok(books);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BookContract book)
        {
            await validator.ValidateAndThrowAsync(book);

            var bookModel = mapper.Map<BookModel>(book);

            return Ok(await addBookUseCase.ExecuteAsync(bookModel));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            await deleteBookUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
           
            return Ok(await getBookByIDUseCase.ExecuteAsync(id));
        }

        [HttpGet("{isbn:length(10, 13)}")]
        public async Task<IActionResult> GetByISBN([FromRoute] string isbn)
        {
            
            return Ok(await getBookByISBNUseCase.ExecuteAsync(isbn));
        }
        [HttpGet("author/{authorId:guid}")]
        public async Task<IActionResult> GetByAuthor([FromRoute] Guid authorId, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await getBooksByAuthorUseCase.ExecuteAsync(authorId, page, size));
        }
        [HttpGet("title={title:length(1,50)}")]
        public async Task<IActionResult> GetByTitle(string title, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await getBooksByTitleUseCase.ExecuteAsync(title, page, size));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromForm]BookContract book, Guid id)
        {
            await validator.ValidateAndThrowAsync(book);

            var bookModel = mapper.Map<BookModel>(book);
            bookModel.Id = id;

            await updateBookUseCase.ExecuteAsync(bookModel);

            return NoContent();

        }

    }
}
