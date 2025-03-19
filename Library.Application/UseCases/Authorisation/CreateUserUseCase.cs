using AutoMapper;
using Library.Application.Exceptions;
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
        public async Task<Guid> ExecuteAsync(UserModel user, CancellationToken cancellationToken)
        {
            var userEntity = mapper.Map<UserEntity>(user);

            if (await db.userRepository.GetByEmailAsync(user.Email, cancellationToken) != null)
                throw new ObjectAlreadyExistsException($"User with email {user.Email} already exists.");

            userEntity.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password); 

            var newEntity = await db.userRepository.AddAsync(userEntity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return newEntity.Id;
        }
    }
}
