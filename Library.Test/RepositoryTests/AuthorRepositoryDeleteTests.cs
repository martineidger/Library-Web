﻿using Library.Core.Entities;
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
        public async Task Delete_ShouldRemoveAuthor_WhenAuthorExists()
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
            var cancellationToken = CancellationToken.None;

            // Act
             _repository.Delete(authorEntity);
            _context.SaveChanges();

            var result = await _repository.GetByFullNAMe(authorEntity.FirstName, authorEntity.Surname, cancellationToken) == null;

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(authorEntity, _context.Authors);
        }

    }
}
