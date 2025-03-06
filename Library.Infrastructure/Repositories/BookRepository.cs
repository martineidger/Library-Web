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
    public class BookRepository : IBookRepository
    {
        private LibraryDbContext context;
        public BookRepository(LibraryDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(BookEntity entity)
        {
            await context.Books.AddAsync(entity);
            return entity.Id;
        }

        public bool Delete(Guid id)
        {
            var entity = context.Books.Find(id);
            if (entity == null) return false;
            context.Books.Remove(entity);
            return true;
        }

        public async Task<List<BookEntity>> GetAllAsync()
        {
            return await context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<List<BookEntity>> GetBookByAuthor(Guid authorId)
        {
            return await context.Books.AsNoTracking().Where(b => b.Author.Id == authorId).ToListAsync();
        }


        public async Task<BookEntity> GetByIdAsync(Guid id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<BookEntity> GetByISBNAsync(string isbn)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<Guid> UpdateAsync(BookEntity entity)
        {
            //context.Update(entity);
            var entToUpdate = await context.Books.FirstOrDefaultAsync(b => b.Id == entity.Id);

            entToUpdate.Title = entity.Title;
            entToUpdate.Author = entity.Author;
            entToUpdate.ISBN = entity.ISBN;
            entToUpdate.ImgPath = entity.ImgPath;
            entToUpdate.ReturnDate = entity.ReturnDate;
            entToUpdate.PickDate = entity.PickDate;
            entToUpdate.Description = entity.Description;

            return entToUpdate.Id;
        }
    }
}
