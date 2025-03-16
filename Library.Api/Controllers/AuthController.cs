using AutoMapper;
using FluentValidation;
using Library.Api.Contracts;
using Library.Application.Models;
using Library.Application.UseCases.Authorisation;
using Library.Core.Exceptions;
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
        private readonly IValidator<RegistrationContract> regValidator;
        private readonly IValidator<LoginContract> logValidator;

        public AuthController(
            IMapper mapper,
            CreateUserUseCase createUserUseCase,
            LoginUseCase loginUseCase,
            RefreshTokenUseCase refreshTokenUseCase,

            IValidator<RegistrationContract> regValidator,
            IValidator<LoginContract> logValidator
            )
        {
            this.mapper = mapper;
            this.createUserUseCase = createUserUseCase;
            this.loginUseCase = loginUseCase;
            this.refreshTokenUseCase = refreshTokenUseCase;
            this.regValidator = regValidator;
            this.logValidator = logValidator;
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody]RegistrationContract contract, CancellationToken cancellationToken)
        {
            await regValidator.ValidateAndThrowAsync( contract );

            var userModel = mapper.Map<UserModel>(contract);

            return Ok(new { userId = await createUserUseCase.ExecuteAsync(userModel, cancellationToken)});  
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginContract contract, CancellationToken cancellationToken)
        {
            await logValidator.ValidateAndThrowAsync( contract );

            var tokens = await loginUseCase.ExecuteAsync(contract.Email, contract.Password, cancellationToken);

            return Ok(tokens);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
        {
            Console.WriteLine(refreshToken);
            var token = await refreshTokenUseCase.ExecuteAsync(refreshToken, cancellationToken);

            return Ok(new { accessToken = token });
        }
    }
}
