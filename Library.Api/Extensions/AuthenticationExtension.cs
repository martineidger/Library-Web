using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.Api.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetValue<string>("ApiSettings:Secret");
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException(nameof(secret), "JWT secret key must be configured in ApiSettings section.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
