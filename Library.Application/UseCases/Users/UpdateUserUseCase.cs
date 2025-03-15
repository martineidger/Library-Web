using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    internal class UpdateUserUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public UpdateUserUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task ExecuteAsync(UserModel user, CancellationToken cancellationToken)
        {
            if (db.userRepository.GetByIdAsync(user.Id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on UpdateUserUseCase: no such user, id = {user.Id}");

            await db.userRepository.UpdateAsync(mapper.Map<UserEntity>(user));
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
