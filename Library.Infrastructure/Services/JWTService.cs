using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Library.Core.Abstractions;
using Library.Core.Abstractions.ServicesAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Library.Infrastructure.Services
{
    public enum TokenConstants
    {
        ACS_TOKEN_LIFE = 30,
        RFSH_TOKEN_LIFE = 30
    }

    record RefreshTokenModel (string Token, Guid UserId, string ExpiresIn);
   
    public class JWTService : ITokenService
    {
        private readonly string secretKey;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly ILibraryUnitOfWork db;

        public JWTService(IConfiguration configuration, ILibraryUnitOfWork db)
        {
            this.secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.db = db;
        }
        public string GetAccesToken(Guid id, string role)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(secretKey);

            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes((int)TokenConstants.ACS_TOKEN_LIFE),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }

        public string GetRefreshToken(Guid id)
        {
            Guid newGuid = Guid.NewGuid();
            string refreshTokenStr = newGuid.ToString("D");

            var refreshToken = new RefreshTokenModel
            (
                refreshTokenStr,
                id,
                DateTime.UtcNow.AddDays((int)TokenConstants.RFSH_TOKEN_LIFE).ToString("o")
            );

            string tokenString = JsonConvert.SerializeObject(refreshToken);
            string codedRefreshToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tokenString));

            Console.WriteLine("User refresh-token: " + codedRefreshToken);

            return codedRefreshToken;
        }

        public async Task<string> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var userId = ValidateRefreshToken(refreshToken);
            var user = await db.userRepository.GetByIdAsync(userId, cancellationToken);

            var newAccessToken = GetAccesToken(userId, user.Role);

            return newAccessToken;
        }

        private Guid ValidateRefreshToken(string refreshToken)
        {
            Console.WriteLine($"{refreshToken}");
            byte[] bytes = Convert.FromBase64String(refreshToken);
            string jsonString = Encoding.UTF8.GetString(bytes);

            RefreshTokenModel refreshTokenModel = JsonConvert.DeserializeObject<RefreshTokenModel>(jsonString);
            if (refreshTokenModel == null)
                throw new SecurityTokenException("Error when converting refresh token: token cannot be null");

            DateTime expirationDate = DateTime.Parse(refreshTokenModel.ExpiresIn);
            if (DateTime.UtcNow > expirationDate)
                throw new SecurityTokenException($"Refresh token has been expired {refreshTokenModel.ExpiresIn}");

            return refreshTokenModel.UserId;

        }
    }
}
