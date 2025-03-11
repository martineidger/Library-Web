using AutoMapper;
using FluentValidation;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authors;
using Library.Application.UseCases.Books;
using Library.Core.Exceptions;
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
        private readonly GetAuthorsWithoutPaginationUseCase getAuthorsWithoutPaginationUseCase;
        private readonly IValidator<AuthorContract> validator;

        public AuthorsController(
            IMapper mapper,
            GetAllAuthorsUseCase getAllAuthorsUseCase,
            AddAuthorUseCase addAuthorUseCase,
            GetAuthorByIdUseCase getAuthorByIdUseCase,
            DeleteAuthorUseCase deleteAuthorUseCase,
            UpdateAuthorUseCase updateAuthorUseCase, 
            GetAuthorsWithoutPaginationUseCase getAuthorsWithoutPaginationUseCase,
            
            IValidator<AuthorContract> validator)
        {
            this.mapper = mapper;
            this.getAllAuthorsUseCase = getAllAuthorsUseCase;
            this.addAuthorUseCase = addAuthorUseCase;
            this.getAuthorByIdUseCase = getAuthorByIdUseCase;
            this.deleteAuthorUseCase = deleteAuthorUseCase;
            this.updateAuthorUseCase = updateAuthorUseCase;
            this.getAuthorsWithoutPaginationUseCase = getAuthorsWithoutPaginationUseCase;
            this.validator = validator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            return Ok(await getAllAuthorsUseCase.ExecuteAsync(page, size));
        }
        [HttpGet("full")]
        public async Task<IActionResult> GetAllFull()
        {
            return Ok(await getAuthorsWithoutPaginationUseCase.ExecuteAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AuthorContract author) 
        {
            await validator.ValidateAndThrowAsync(author);

            var authorModel = mapper.Map<AuthorModel>(author);

            return Ok(await addAuthorUseCase.ExecuteAsync(authorModel));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            await deleteAuthorUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await getAuthorByIdUseCase.ExecuteAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] AuthorContract author, [FromRoute] Guid id)
        {
            await validator.ValidateAndThrowAsync(author);

            var authorModel = mapper.Map<AuthorModel>(author);

            authorModel.Id = id;
            await updateAuthorUseCase.ExecuteAsync(authorModel);

            return NoContent();

        }

    }
}
