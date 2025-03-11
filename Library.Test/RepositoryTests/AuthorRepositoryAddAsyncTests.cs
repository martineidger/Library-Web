using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Test.RepositoryTests
{
    public class AuthorRepositoryAddAsyncTests
    {
        private readonly AuthorRepository _repository;
        private readonly LibraryDbContext _context;

        public AuthorRepositoryAddAsyncTests()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryDbContext(options);
            _repository = new AuthorRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAuthor_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorEntity = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Country = "USA",
            };

            // Act
            var result = await _repository.AddAsync(authorEntity);
            await _context.SaveChangesAsync(); // Сохраняем изменения

            // Assert
            Assert.Equal(authorEntity.Id, result);
            Assert.Contains(authorEntity, _context.Authors);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException_WhenAuthorAlreadyExists()
        {
            // Arrange
            var authorEntity = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Country = "USA",
            };
            await _repository.AddAsync(authorEntity);
            await _context.SaveChangesAsync();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () => {
                    await _repository.AddAsync(authorEntity);
                    await _context.SaveChangesAsync(); }); // Повторное добавление
            Assert.Contains("same key", exception.Message);
        }
    }
}
