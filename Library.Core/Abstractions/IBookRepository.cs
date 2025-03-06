using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IBookRepository
    {
        Task<BookEntity> GetByIdAsync(Guid id);
        Task<BookEntity> GetByISBNAsync(string isbn);
        Task<List<BookEntity>> GetAllAsync();
        Task<Guid> AddAsync(BookEntity entity);
        bool Delete(Guid id);
        Task<Guid> UpdateAsync(BookEntity entity);
        Task<List<BookEntity>> GetBookByAuthor(Guid authorId);
        
    }
}
