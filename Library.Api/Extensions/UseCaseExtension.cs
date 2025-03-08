using Library.Application.UseCases.Authors;

namespace Library.Api.Extensions
{
    public static class UseCaseExtension
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<AddAuthorUseCase>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithScopedLifetime());
        }
    }
}
