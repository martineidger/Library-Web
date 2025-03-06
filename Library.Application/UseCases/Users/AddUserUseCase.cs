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

        public AddUserUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<Guid> ExecuteAsync(UserEntity user)
        {
            //add validations
            var id = await db.userRepository.AddAsync(user);
            await db.SaveChangesAsync();

            return id;
        }
    }
}
