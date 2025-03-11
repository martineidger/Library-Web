using Library.Application.Models;

namespace Library.Api.Contracts
{
    public class UserInfoContract
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Role { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
