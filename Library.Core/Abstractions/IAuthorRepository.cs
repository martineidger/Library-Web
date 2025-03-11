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
        Task<AuthorEntity?> GetByIdAsyhnc(Guid id);
        Task<PagedItems<AuthorEntity>> GetAllAsync(int page, int size);
        Task<List<AuthorEntity>> GetAllAsync();
        Task<AuthorEntity?> GetByFullNAMe(string name, string surname);
        Task<Guid> AddAsync(AuthorEntity entity);
        Task<Guid> UpdateAsync(AuthorEntity entity);
        bool Delete(Guid id);


    }
}
