using Library.Core.Abstractions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class LibraryUnitOfWork : ILibraryUnitOfWork
    {
        private readonly LibraryDbContext context;
        public IAuthorRepository authorRepository { get; }
        public IBookRepository bookRepository {  get; }
        public IUserRepository userRepository {  get; }

        public LibraryUnitOfWork(LibraryDbContext context)
        {
            this.context = context;

            authorRepository = new AuthorRepository(context);
            bookRepository = new BookRepository(context);
            userRepository = new UserRepository(context);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
