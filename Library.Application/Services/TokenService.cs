using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Library.Application.Models;
using Library.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace Library.Application.Services
{
    public enum TokenConstants
    {
        ACS_TOKEN_LIFE = 30,
        RFSH_TOKEN_LIFE = 30
    }
    public class TokenService
    {
        private readonly string secretKey;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly ILibraryUnitOfWork db;

        public TokenService(IConfiguration configuration, ILibraryUnitOfWork db)
        {
            this.secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.db = db;
        }
        public string GetAccesToken(TokenRequestModel request)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(secretKey);

            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                    new Claim(ClaimTypes.Role, request.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes((int)TokenConstants.ACS_TOKEN_LIFE),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }

        public string GetRefreshToken(TokenRequestModel request)
        {
            Guid newGuid = Guid.NewGuid();
            string refreshTokenStr = newGuid.ToString("D");

            var refreshToken = new RefreshTokenModel()
            {
                Token = refreshTokenStr,
                UserId = request.UserId,
                ExpiresIn = DateTime.UtcNow.AddDays((int)TokenConstants.RFSH_TOKEN_LIFE).ToString("o") 
            };

            string tokenString = JsonConvert.SerializeObject(refreshToken);
            string codedRefreshToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tokenString));

            Console.WriteLine("User refresh-token: " + codedRefreshToken);

            return codedRefreshToken;
        }

        public string RefreshToken(string refreshToken)
        {
            var userId = ValidateRefreshToken(refreshToken);
            var user = db.userRepository.GetByIdAsync(userId).Result;

            /* var key = System.Text.Encoding.ASCII.GetBytes(secretKey);
             var accessTokenDescriptor = new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(new Claim[]
                 {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                     new Claim(ClaimTypes.Role, user.Role)
                 }),
                 Expires = DateTime.UtcNow.AddMinutes((int)TokenConstants.ACS_TOKEN_LIFE),
                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
             };

             var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
             var accessTokenStr = tokenHandler.WriteToken(accessToken);*/

            var newAccessToken = GetAccesToken(new TokenRequestModel()
            {
                UserId = userId,
                Role = user.Role
            });

            return newAccessToken;
        }

        private Guid ValidateRefreshToken(string refreshToken)
        {
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
