using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper mapper;
        private readonly CreateUserUseCase createUserUseCase;
        private readonly LoginUseCase loginUseCase;
        private readonly RefreshTokenUseCase refreshTokenUseCase;

        public AuthController(
            IMapper mapper,
            CreateUserUseCase createUserUseCase,
            LoginUseCase loginUseCase,
            RefreshTokenUseCase refreshTokenUseCase
            )
        {
            this.mapper = mapper;
            this.createUserUseCase = createUserUseCase;
            this.loginUseCase = loginUseCase;
            this.refreshTokenUseCase = refreshTokenUseCase;
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody]RegistrationContract contract)
        {
            // validation

            var userModel = mapper.Map<UserModel>(contract);

            return Ok(new { userId = await createUserUseCase.ExecuteAsync(userModel)});  
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginContract contract)
        {
            // validation

           var tokens = await loginUseCase.ExecuteAsync(contract.Email, contract.Password);

            return Ok(tokens);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            // validation

            var token = await refreshTokenUseCase.ExecuteAsync(refreshToken);

            return Ok(new { accessToken = token });
        }
    }
}
