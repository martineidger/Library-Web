using Library.Application.UseCases.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly AddBookUseCase addBookUseCase;
        
        public BooksController(AddBookUseCase addBookUseCase)
        {
            this.addBookUseCase = addBookUseCase;
        }
        public async Task<IActionResult> Index()
        {
            return Ok(await addBookUseCase.ExecuteAsync());
        }
    }
}
