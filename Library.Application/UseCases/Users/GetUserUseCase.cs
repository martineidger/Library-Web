using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class GetUserUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetUserUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<UserModel> ExecuteAsync(string email, CancellationToken cancellationToken)
        {
            var userEntity = await db.userRepository.GetByEmailAsync(email, cancellationToken)??
                throw new ObjectNotFoundException($"Error on GetUserUseCase: no such user, email = {email}");

            return mapper.Map<UserModel>(userEntity);
        }
    }
}
