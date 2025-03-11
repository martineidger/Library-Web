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
    public class UpdateAuthorUseCaseTests
    {
        private readonly Mock<ILibraryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateAuthorUseCase _useCase;

        public UpdateAuthorUseCaseTests()
        {
            _mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new UpdateAuthorUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            var authorModel = new AuthorModel { Id = Guid.NewGuid() };
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsyhnc(authorModel.Id)).ReturnsAsync((AuthorEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(() => _useCase.ExecuteAsync(authorModel));
            Assert.Equal($"Error on UpdateAuthorUseCase: no such author, id = {authorModel.Id}", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorModel = new AuthorModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                Surname = "Doe",
                BirthDate = new DateTime(1985, 5, 5),
                Country = "USA"
            };
            var authorEntity = new AuthorEntity { Id = authorModel.Id };
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsyhnc(authorModel.Id)).ReturnsAsync(authorEntity);
            _mockMapper.Setup(m => m.Map<AuthorEntity>(authorModel)).Returns(authorEntity);

            // Act
            await _useCase.ExecuteAsync(authorModel);

            // Assert
            _mockUnitOfWork.Verify(u => u.authorRepository.UpdateAsync(authorEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
