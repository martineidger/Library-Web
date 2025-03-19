using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface IAuthorRepository : IBaseRepository<AuthorEntity>
    {
        Task<List<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<AuthorEntity?> GetByFullNAMe(string name, string surname, CancellationToken cancellationToken);
       
    }
}
