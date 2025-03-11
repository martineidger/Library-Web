using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class GetUserInfoUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetUserInfoUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<UserModel> ExecuteAsync(Guid id)
        {
            var userEntity = await db.userRepository.GetByIdAsync(id) ??
                throw new ObjectNotFoundException($"Error on GetUserUseCase: no such user, id = {id}");

            return mapper.Map<UserModel>(userEntity);
        }
    }
}
