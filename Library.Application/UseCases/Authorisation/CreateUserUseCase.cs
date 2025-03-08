using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authorisation
{
    public class CreateUserUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public CreateUserUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(UserModel user)
        {
            var userEntity = mapper.Map<UserEntity>(user);
            userEntity.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password); // мейби вынеси в маппинг 

            var newId = await db.userRepository.AddAsync(userEntity);
            await db.SaveChangesAsync();
            return newId;
        }
    }
}
