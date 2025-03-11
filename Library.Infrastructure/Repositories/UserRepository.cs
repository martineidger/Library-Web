using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
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

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await context.Users.Include(u => u.Books).FirstOrDefaultAsync(us => us.Email == email);
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size)
        {
            var query = await context.Users.Include(u => u.Books).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            var books = query.Books as List<BookEntity>;
            var totalItems = books.Count(); 

            var items = books
                .Skip((page - 1) * size) 
                .Take(size) 
                .ToList(); 

            return new PagedItems<BookEntity>
            {
                Items = items,
                TotalCount = totalItems,
                PageSize = size,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
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
