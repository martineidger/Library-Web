using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Abstractions
{
    public interface ILibraryUnitOfWork : IDisposable
    {
        IAuthorRepository authorRepository { get; }
        IBookRepository bookRepository { get; }
        IUserRepository userRepository { get; }
        Task SaveChangesAsync();
    }
}
