using Library.Core.Entities;
using Library.Infrastructure.Context;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Library.Test.RepositoryTests
{
    public class AuthorRepositoryDeleteTests
    {
        private readonly AuthorRepository _repository;
        private readonly LibraryDbContext _context;

        public AuthorRepositoryDeleteTests()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryDbContext(options);
            _repository = new AuthorRepository(_context);
        }

        [Fact]
        public void Delete_ShouldRemoveAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorEntity = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                Surname = "Doe",
                BirthDate = new DateTime(1985, 5, 15),
                Country = "Canada",
            };
            _context.Authors.Add(authorEntity);
            _context.SaveChanges();

            // Act
            var result = _repository.Delete(authorEntity.Id);
            _context.SaveChanges();

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(authorEntity, _context.Authors);
        }

        [Fact]
        public void Delete_ShouldReturnFalse_WhenAuthorDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = _repository.Delete(nonExistentId);

            // Assert
            Assert.False(result);
        }
    }
}
