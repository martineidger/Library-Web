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

        public void Delete(Guid id)
        {
            var usr = context.Users.FirstOrDefault(u=> u.Id == id);
            context.Users.Remove(usr);
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(us => us.Email == email);
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<Guid> UpdateAsync(UserEntity entity)
        {
            var localEntity = context.Books.Local.FirstOrDefault(a => a.Id == entity.Id);
            if (localEntity != null)
            {
                context.Entry(localEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                context.Update(entity);
            }

            return entity.Id;
        }
    }
}
