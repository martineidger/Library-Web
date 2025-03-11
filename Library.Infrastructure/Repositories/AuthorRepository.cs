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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext context;

        public AuthorRepository(LibraryDbContext  context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(AuthorEntity entity)
        {
                
            await context.Authors.AddAsync(entity);
            return entity.Id;
        }

        public bool Delete(Guid id)
        {
            var entity = context.Authors.Find(id);
            if (entity == null) return false;
            context.Authors.Remove(entity);
            return true;
        }

        public async Task<PagedItems<AuthorEntity>> GetAllAsync(int page, int size)
        {
            var query = context.Authors.Include(a => a.Books).AsNoTracking();

            var totalItems = await query.CountAsync(); 

            var items = await query
                .Skip((page - 1) * size) 
                .Take(size) 
                .ToListAsync(); 

            return new PagedItems<AuthorEntity>
            {
                Items = items,
                TotalCount = totalItems,
                PageSize = size,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        public async Task<List<AuthorEntity>> GetAllAsync()
        {
            return await context.Authors.Include(a => a.Books).AsNoTracking().ToListAsync();
        }

        public async Task<AuthorEntity?> GetByFullNAMe(string name, string surname)
        {
            return await context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.FirstName == name && a.Surname == surname);
        }

        public async Task<AuthorEntity?> GetByIdAsyhnc(Guid id)
        {
            return await context.Authors.FindAsync(id);
        }

        public async Task<Guid> UpdateAsync(AuthorEntity entity)
        {
            var localEntity = context.Authors.Local.FirstOrDefault(a => a.Id == entity.Id);
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
