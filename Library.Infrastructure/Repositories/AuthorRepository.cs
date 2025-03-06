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

        public async Task<List<AuthorEntity>> GetAllAsync()
        {
            return await context.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<AuthorEntity> GetByIdAsyhnc(Guid id)
        {
            return await context.Authors.FindAsync(id);
        }

        public async Task<Guid> UpdateAsync(AuthorEntity entity)
        {
            context.Update(entity);
            return entity.Id;
        }
    }
}
