using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size, CancellationToken cancellationToken);
    }
}
