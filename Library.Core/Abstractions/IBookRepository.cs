using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IBookRepository : IBaseRepository<BookEntity>
    {
        Task<BookEntity?> GetByISBNAsync(string isbn, CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetByTitleAsync(string title, int page, int size,CancellationToken cancellationToken);
        Task<PagedItems<BookEntity>> GetBookByAuthor(Guid authorId, int page, int size, CancellationToken cancellationToken);
        
    }
}
