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
        Task<BookEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BookEntity?> GetByISBNAsync(string isbn, CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetByTitleAsync(string title, int page, int size,CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetAllAsync(int page, int size, CancellationToken cancellationToken);
        Task<Guid> AddAsync(BookEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(BookEntity entity, CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetBookByAuthor(Guid authorId, int page, int size, CancellationToken cancellationToken);
        
    }
}
