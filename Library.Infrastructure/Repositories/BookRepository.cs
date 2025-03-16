using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private LibraryDbContext context;
        public BookRepository(LibraryDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(BookEntity entity, CancellationToken cancellationToken)
        {

            await context.Books.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await context.Books.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return false;

            context.Books.Remove(entity);

            return true;
        }

        public async Task<PagedItems<BookEntity>> GetAllAsync(int page, int size, CancellationToken cancellationToken)
        {
            var query = context.Books.AsNoTracking();

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
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

        public async Task<PagedItems<BookEntity>> GetBookByAuthor(Guid authorId, int page, int size, CancellationToken cancellationToken)
        {
            var query = context.Books.AsNoTracking().Where(b => b.Author.Id == authorId);
            var totalItems = await query.CountAsync(cancellationToken); 

            var items = await query
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


        public async Task<BookEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<BookEntity?> GetByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn,cancellationToken );
        }
        public async Task<PagedItems<BookEntity>> GetByTitleAsync(string title, int page, int size, CancellationToken cancellationToken)
        {
            var query = context.Books.AsNoTracking().Where(b => b.Title.Contains(title));
            var totalItems = await query.CountAsync(cancellationToken); 

            var items = await query
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

        public async Task<Guid> UpdateAsync(BookEntity entity, CancellationToken cancellationToken)
        {

            //var localEntity = context.Books.Local.FirstOrDefault(a => a.Id == entity.Id);

            //if (localEntity != null)
            //{
            //    context.Entry(localEntity).CurrentValues.SetValues(entity);
            //}
            //else
            //{
            //    context.Books.Update(entity);
            //}

            var existingEntity = await context.Books.FindAsync(entity.Id, cancellationToken);

            if (existingEntity != null)
            {
                existingEntity.ISBN = entity.ISBN;
                existingEntity.Title = entity.Title;
                existingEntity.Genre = entity.Genre;
                existingEntity.Description = entity.Description;
                existingEntity.AuthorID = entity.AuthorID;
                existingEntity.ImgPath = entity.ImgPath;
                existingEntity.PickDate = entity.PickDate;
                existingEntity.ReturnDate = entity.ReturnDate;

                context.Books.Update(existingEntity);
            }

            return entity.Id;

        }
    }
}
