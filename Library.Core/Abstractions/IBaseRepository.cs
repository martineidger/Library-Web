using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedItems<T>> GetAllAsync(int page, int size, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        void Delete(T entity);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
