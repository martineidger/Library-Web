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

namespace Library.Application.UseCases.Authorisation
{
    public class DeleteUserUseCase
    {
        private readonly ILibraryUnitOfWork db;

        public DeleteUserUseCase(ILibraryUnitOfWork db)
        {
            this.db = db;
        }
        public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (db.userRepository.GetByIdAsync(id, cancellationToken) == null)
                throw new ObjectNotFoundException($"Error on DeleteUserUseCase: no such user, id = {id}");

            await db.userRepository.DeleteAsync(id, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
