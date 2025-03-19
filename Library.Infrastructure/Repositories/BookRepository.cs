using Library.Core.Abstractions;
using Library.Core.Entities;
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
    public class BookRepository : BaseRepository<BookEntity>, IBookRepository
    {
        private LibraryDbContext context;
        public BookRepository(LibraryDbContext context) : base(context) 
        {
            this.context = context;
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

       
    }
}
