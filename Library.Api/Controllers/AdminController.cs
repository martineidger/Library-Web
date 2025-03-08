using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IMapper mapper;
        private readonly CreateUserUseCase createUserUseCase;
        private readonly DeleteUserUseCase deleteUserUseCase;

        public AdminController(
            IMapper mapper,
            CreateUserUseCase createUserUseCase, 
            DeleteUserUseCase deleteUserUseCase)
        {
            this.mapper = mapper;
            this.createUserUseCase = createUserUseCase;
            this.deleteUserUseCase = deleteUserUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] UserContract newAdmin)
        {
            // valid

            var usermodel = mapper.Map<UserModel>(newAdmin);

            return Ok(await createUserUseCase.ExecuteAsync(usermodel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // valid

            await deleteUserUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
