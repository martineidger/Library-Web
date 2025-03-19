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
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly LibraryDbContext context;

        public UserRepository(LibraryDbContext context) : base(context) 
        {
            this.context = context;
        }


        public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Users.Include(u => u.Books).FirstOrDefaultAsync(us => us.Email == email, cancellationToken);
        }
        public async Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size, CancellationToken cancellationToken)
        {
            var user = await context.Users
                                    .Include(u => u.Books)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
            {
                return new PagedItems<BookEntity>
                {
                    Items = new List<BookEntity>(),
                    TotalCount = 0,
                    PageSize = size,
                    CurrentPage = page,
                    TotalPages = 0
                };
            }

            var books = user.Books; 
            var totalItems = books.Count; 

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

    }
}
