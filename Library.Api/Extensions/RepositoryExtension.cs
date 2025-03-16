using Library.Core.Abstractions;
using Library.Infrastructure;
using Library.Infrastructure.Context;
using Library.Infrastructure.Initializing;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresDocker"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<ILibraryUnitOfWork, LibraryUnitOfWork>();

            services.AddTransient<IDbInitializer, DbInitializer>();
        }
    }
}
