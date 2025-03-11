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
        Task<Guid> AddAsync(UserEntity user);
        Task<Guid> UpdateAsync(UserEntity user);
        void Delete(Guid id);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size);
    }
}
