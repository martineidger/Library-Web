using AutoMapper;
using Library.Api.Contracts;
using Library.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly GetUserInfoUseCase getUserInfoUseCase;
        private readonly GetBookOnHandsUseCase bookOnHandsUseCase;
        private readonly GetUsersBooksUseCase getUsersBooksUseCase;
        private readonly IMapper mapper;

        public UsersController(
            GetUserInfoUseCase getUserInfoUseCase, 
            GetBookOnHandsUseCase bookOnHandsUseCase,
            GetUsersBooksUseCase getUsersBooksUseCase,
            IMapper mapper)
        {
            this.getUserInfoUseCase = getUserInfoUseCase;
            this.bookOnHandsUseCase = bookOnHandsUseCase;
            this.getUsersBooksUseCase = getUsersBooksUseCase;
            this.mapper = mapper;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var userInfo = mapper.Map<UserInfoContract>(await getUserInfoUseCase.ExecuteAsync(id));

            return Ok(userInfo);
        }
        [HttpPost("takebook/{bookId:guid}")]
        public async Task<IActionResult> AddBookToUser(Guid bookId, [FromQuery] Guid userId)
        {
            var retDate = await bookOnHandsUseCase.ExecuteAsync(userId, bookId);
            return Ok(new {returnDate = retDate});
        }
        [HttpGet("{id:guid}/mybooks")]
        public async Task<IActionResult> GetUsersBooks(Guid id, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var books = await getUsersBooksUseCase.ExecuteAsync(id, page, size);

            return Ok(books);
        }

    }
}
