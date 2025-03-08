using Library.Api.Mappings;
using Library.Application.Mappings;

namespace Library.Api.Extensions
{
    public static class MappingExtension
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserEntProfile));
            services.AddAutoMapper(typeof(AuthorEntProfile));
            services.AddAutoMapper(typeof(BookEntProfile));

            services.AddAutoMapper(typeof(UserModelProfile));
            services.AddAutoMapper(typeof(AuthorModelProfile));
            services.AddAutoMapper(typeof(BookModelProfile));

        }
    }
}
