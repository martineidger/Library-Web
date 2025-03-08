using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authors;
using Library.Application.UseCases.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IMapper mapper;
        private readonly GetAllAuthorsUseCase getAllAuthorsUseCase;
        private readonly AddAuthorUseCase addAuthorUseCase;
        private readonly GetAuthorByIdUseCase getAuthorByIdUseCase;
        private readonly DeleteAuthorUseCase deleteAuthorUseCase;
        private readonly UpdateAuthorUseCase updateAuthorUseCase;

        public AuthorsController(
            IMapper mapper,
            GetAllAuthorsUseCase getAllAuthorsUseCase,
            AddAuthorUseCase addAuthorUseCase,
            GetAuthorByIdUseCase getAuthorByIdUseCase,
            DeleteAuthorUseCase deleteAuthorUseCase,
            UpdateAuthorUseCase updateAuthorUseCase)
        {
            this.mapper = mapper;
            this.getAllAuthorsUseCase = getAllAuthorsUseCase;
            this.addAuthorUseCase = addAuthorUseCase;
            this.getAuthorByIdUseCase = getAuthorByIdUseCase;
            this.deleteAuthorUseCase = deleteAuthorUseCase;
            this.updateAuthorUseCase = updateAuthorUseCase;
        }

        //[Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await getAllAuthorsUseCase.ExecuteAsync(page, size));
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AuthorContract author) 
        {
            //add validation

            var authorModel = mapper.Map<AuthorModel>(author);

            return Ok(await addAuthorUseCase.ExecuteAsync(authorModel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //validation

            await deleteAuthorUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //valid

            return Ok(await getAuthorByIdUseCase.ExecuteAsync(id));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] AuthorContract author, [FromRoute] Guid id)
        {
            //valid

            var authorModel = mapper.Map<AuthorModel>(author);

            authorModel.Id = id;
            await updateAuthorUseCase.ExecuteAsync(authorModel);

            return NoContent();

        }

    }
}
