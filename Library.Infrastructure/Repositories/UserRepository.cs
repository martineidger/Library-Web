﻿using Library.Core.Abstractions;
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

        public async Task<Guid> AddAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            return user.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await context.Books.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return false;

            context.Books.Remove(entity);

            return true;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Users.Include(u => u.Books).FirstOrDefaultAsync(us => us.Email == email, cancellationToken);
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Users.FindAsync(id, cancellationToken);
        }

        public async Task<PagedItems<BookEntity>> GetUsersBooks(Guid userId, int page, int size, CancellationToken cancellationToken)
        {
            var query = await context.Users.Include(u => u.Books).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            var books = (IQueryable<BookEntity>)query.Books;
            var totalItems = await books.CountAsync(cancellationToken); 

            var items = await books
                .Skip((page - 1) * size) 
                .Take(size) 
                .ToListAsync(cancellationToken); 

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

            return await Task.FromResult(entity.Id);
        }
    }
}
