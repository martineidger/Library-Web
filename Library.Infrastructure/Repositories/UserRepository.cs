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

        public async Task AddAsync(UserEntity user)
        {
            await context.Users.AddAsync(user);
        }

        public bool Delete(Guid id)
        {
            var usr = context.Users.Find(id);
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
    }
}
