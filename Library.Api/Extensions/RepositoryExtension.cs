using Library.Core.Abstractions;
using Library.Infrastructure;
using Library.Infrastructure.Repositories;

namespace Library.Api.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<ILibraryUnitOfWork, LibraryUnitOfWork>();
        }
    }
}
