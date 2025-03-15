using AutoMapper;
using Library.Application.Models;
using Library.Application.UseCases.Authors;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Test.UseCasesTests
{
    public class GetAuthorByIdUseCaseTests
    {
        private readonly Mock<ILibraryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAuthorByIdUseCase _useCase;

        public GetAuthorByIdUseCaseTests()
        {
            _mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAuthorByIdUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var authorId = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsyhnc(authorId, cancellationToken)).ReturnsAsync((AuthorEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(() => _useCase.ExecuteAsync(authorId, cancellationToken));
            Assert.Equal($"Error on GetAuthorByIdUseCase: no such author, id = {authorId}", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAuthorModel_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var authorEntity = new AuthorEntity
            {
                Id = authorId,
                FirstName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Country = "USA"
            };
            var authorModel = new AuthorModel
            {
                Id = authorId,
                FirstName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Country = "USA"
            };
            var cancellationToken = CancellationToken.None;
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsyhnc(authorId, cancellationToken)).ReturnsAsync(authorEntity);
            _mockMapper.Setup(m => m.Map<AuthorModel>(authorEntity)).Returns(authorModel);

            // Act
            var result = await _useCase.ExecuteAsync(authorId, cancellationToken);

            // Assert
            Assert.Equal(authorId, result.Id);
            Assert.Equal(authorModel.FirstName, result.FirstName);
            Assert.Equal(authorModel.Surname, result.Surname);
            Assert.Equal(authorModel.BirthDate, result.BirthDate);
            Assert.Equal(authorModel.Country, result.Country);
        }
    }
}
