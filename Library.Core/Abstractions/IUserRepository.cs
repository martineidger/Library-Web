using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(UserEntity user, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(UserEntity user);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size, CancellationToken cancellationToken);
    }
}
