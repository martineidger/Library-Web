using FluentValidation;
using Library.Api.Contracts;
using Library.Api.Validation;

namespace Library.Api.Extensions
{
    public static class ValidationExtension
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegistrationContract>, RegistrationValidator>();
            services.AddScoped<IValidator<LoginContract>, LoginValidator>();
            services.AddScoped<IValidator<BookContract>, BookValidator>();
            services.AddScoped<IValidator<AuthorContract>, AuthorValidator>();
        }
    }
}