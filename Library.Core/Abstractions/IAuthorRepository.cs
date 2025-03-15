using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IAuthorRepository
    {
        Task<AuthorEntity?> GetByIdAsyhnc(Guid id, CancellationToken cancellationToken);
        Task<PagedItems<AuthorEntity>> GetAllAsync(int page, int size, CancellationToken cancellationToken);
        Task<List<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<AuthorEntity?> GetByFullNAMe(string name, string surname, CancellationToken cancellationToken);
        Task<Guid> AddAsync(AuthorEntity entity, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(AuthorEntity entity);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);


    }
}
