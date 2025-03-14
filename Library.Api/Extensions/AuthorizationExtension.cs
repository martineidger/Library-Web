﻿namespace Library.Api.Extensions
{
    public static class AuthorizationExtension
    {
        public static void AddAuthPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin"));

            });
        }
    }
}
