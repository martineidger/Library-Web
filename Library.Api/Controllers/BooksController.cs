using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly AddBookUseCase addBookUseCase;
        private readonly GetAllBooksUseCase getAllBooksUseCase;
        private readonly DeleteBookUseCase deleteBookUseCase;
        private readonly GetBookByIDUseCase getBookByIDUseCase;
        private readonly GetBookByISBNUseCase getBookByISBNUseCase;
        private readonly GetBooksByAuthorUseCase getBooksByAuthorUseCase;
        private readonly UpdateBookUseCase updateBookUseCase;

        public BooksController(
            IMapper mapper,
            AddBookUseCase addBookUseCase,
            GetAllBooksUseCase getAllBooksUseCase,
            DeleteBookUseCase deleteBookUseCase,
            GetBookByIDUseCase getBookByIDUseCase,
            GetBookByISBNUseCase getBookByISBNUseCase,
            GetBooksByAuthorUseCase getBooksByAuthorUseCase,
            UpdateBookUseCase updateBookUseCase)
        {
            this.mapper = mapper;
            this.addBookUseCase = addBookUseCase;
            this.getAllBooksUseCase = getAllBooksUseCase;
            this.deleteBookUseCase = deleteBookUseCase;
            this.getBookByIDUseCase = getBookByIDUseCase;
            this.getBookByISBNUseCase = getBookByISBNUseCase;
            this.getBooksByAuthorUseCase = getBooksByAuthorUseCase;
            this.updateBookUseCase = updateBookUseCase;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await getAllBooksUseCase.ExecuteAsync(page, size));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookContract book) // добавлять сразу с автором
        {
            //add validation

            var bookModel = mapper.Map<BookModel>(book);

            return Ok(await addBookUseCase.ExecuteAsync(bookModel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //validation

            await deleteBookUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //valid

            return Ok(await getBookByIDUseCase.ExecuteAsync(id));
        }

        [HttpGet("{isbn:length(10, 13)}")]
        public async Task<IActionResult> GetByISBN([FromRoute] string isbn)
        {
            //valid

            return Ok(await getBookByISBNUseCase.ExecuteAsync(isbn));
        }
        [HttpGet("author/{authorId:guid}")]
        public async Task<IActionResult> GetByAuthor([FromRoute] Guid authorId, [FromRoute] int page = 1, [FromRoute] int size = 10)
        {
            return Ok(await getBooksByAuthorUseCase.ExecuteAsync(authorId, page, size));
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody]BookContract book, Guid id)
        {
            //valid

            var bookModel = mapper.Map<BookModel>(book);
            bookModel.Id = id;

            await updateBookUseCase.ExecuteAsync(bookModel);

            return NoContent();

        }

    }
}
