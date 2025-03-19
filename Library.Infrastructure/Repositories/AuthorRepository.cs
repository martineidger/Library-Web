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
    public class AuthorRepository : BaseRepository<AuthorEntity>, IAuthorRepository
    {
        private readonly LibraryDbContext context;

        public AuthorRepository(LibraryDbContext  context) : base(context)
        {
            this.context = context;
        }
        //public async Task<Guid> AddAsync(AuthorEntity entity, CancellationToken cancellationToken)
        //{
                
        //    await context.Authors.AddAsync(entity, cancellationToken);
        //    return entity.Id;
        //}

        //public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        //{
        //    var entity = await context.Authors.FindAsync(new object[] { id }, cancellationToken);
        //    if (entity == null) return false;

        //    context.Authors.Remove(entity);

        //    return true;
        //}

        //public async Task<PagedItems<AuthorEntity>> GetAllAsync(int page, int size, CancellationToken cancellationToken)
        //{
        //    var query = context.Authors.Include(a => a.Books).AsNoTracking();

        //    var totalItems = await query.CountAsync(cancellationToken); 

        //    var items = await query
        //        .Skip((page - 1) * size) 
        //        .Take(size) 
        //        .ToListAsync(cancellationToken); 

        //    return new PagedItems<AuthorEntity>
        //    {
        //        Items = items,
        //        TotalCount = totalItems,
        //        PageSize = size,
        //        CurrentPage = page,
        //        TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        //    };
        //}

        public async Task<List<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Authors.Include(a => a.Books).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<AuthorEntity?> GetByFullNAMe(string name, string surname, CancellationToken cancellationToken)
        {
            return await context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.FirstName == name && a.Surname == surname, cancellationToken);
        }

        //public async Task<AuthorEntity?> GetByIdAsyhnc(Guid id, CancellationToken cancellationToken)
        //{
        //    return await context.Authors.FindAsync(id, cancellationToken);
        //}

        //public async Task<Guid> UpdateAsync(AuthorEntity entity)
        //{
        //    var localEntity = context.Authors.Local.FirstOrDefault(a => a.Id == entity.Id);
        //    if (localEntity != null)
        //    {
        //        context.Entry(localEntity).CurrentValues.SetValues(entity);
        //    }
        //    else
        //    {
        //        context.Update(entity);
        //    }

        //    return await Task.FromResult(entity.Id);
        //}
    }
}
