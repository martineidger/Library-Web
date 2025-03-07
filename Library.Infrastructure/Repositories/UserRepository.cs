using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext context;

        public UserRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> AddAsync(UserEntity user)
        {
            await context.Users.AddAsync(user);
            return user.Id;
        }

        public bool Delete(string email)
        {
            var usr = context.Users.FirstOrDefault(u=> u.Email == email);
            if (usr != null) return false;
            context.Users.Remove(usr);
            return true;
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(us => us.Email == email);
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }

        public Task<Guid> UpdateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
