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
        bool Delete(string email);
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByEmailAsync(string email);
    }
}
