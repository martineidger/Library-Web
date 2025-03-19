using AutoMapper;
using FluentValidation;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IMapper mapper;
        private readonly CreateUserUseCase createUserUseCase;
        private readonly DeleteUserUseCase deleteUserUseCase;
        private readonly IValidator<RegistrationContract> validator;

        public AdminController(
            IMapper mapper,
            CreateUserUseCase createUserUseCase, 
            DeleteUserUseCase deleteUserUseCase, 
            
            IValidator<RegistrationContract> validator)
        {
            this.mapper = mapper;
            this.createUserUseCase = createUserUseCase;
            this.deleteUserUseCase = deleteUserUseCase;
            this.validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] RegistrationContract newAdmin, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(newAdmin);

            var adminModel = mapper.Map<UserModel>(newAdmin);
            adminModel.Role = "Admin";

            return Ok(await createUserUseCase.ExecuteAsync(adminModel, cancellationToken));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await deleteUserUseCase.ExecuteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
