using AutoMapper;
using Library.Application.Models;
using Library.Application.UseCases.Authors;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
using Library.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Test.UseCasesTests
{
    public class AddAuthorUseCaseTests
    {
        private readonly Mock<ILibraryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddAuthorUseCase _useCase;

        public AddAuthorUseCaseTests()
        {
            _mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new AddAuthorUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenAuthorExists()
        {
            // Arrange
            var authorModel = new AuthorModel
            {
                FirstName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Country = "USA"
            };
            var cancellationToken = CancellationToken.None;

            _mockUnitOfWork.Setup(u => u.authorRepository.GetByFullNAMe(authorModel.FirstName, authorModel.Surname, cancellationToken))
                           .ReturnsAsync(new AuthorEntity());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ObjectAlreadyExistsException>(() => _useCase.ExecuteAsync(authorModel, cancellationToken));
            Assert.Equal($"Author John Doe already exists.", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnId_WhenAuthorIsAdded()
        {
            // Arrange
            var authorModel = new AuthorModel
            {
                FirstName = "Jane",
                Surname = "Doe",
                BirthDate = new DateTime(1985, 5, 5),
                Country = "USA"
            };
            var cancellationToken = CancellationToken.None;

            var authorEntity = new AuthorEntity { Id = Guid.NewGuid() };
            _mockMapper.Setup(m => m.Map<AuthorEntity>(authorModel)).Returns(authorEntity);
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByFullNAMe(authorModel.FirstName, authorModel.Surname, cancellationToken))
                           .ReturnsAsync((AuthorEntity)null);
            _mockUnitOfWork.Setup(u => u.authorRepository.AddAsync(authorEntity, cancellationToken)).ReturnsAsync(authorEntity);

            // Act
            var result = await _useCase.ExecuteAsync(authorModel, cancellationToken);

            // Assert
            Assert.Equal(authorEntity.Id, result);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(cancellationToken), Times.Once);
        }
    }
}
