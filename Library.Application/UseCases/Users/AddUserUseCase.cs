using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Users
{
    public class AddUserUseCase
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public AddUserUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<Guid> ExecuteAsync(UserModel user, CancellationToken cancellationToken)
        {
            var id = await db.userRepository.AddAsync(mapper.Map<UserEntity>(user), cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}
