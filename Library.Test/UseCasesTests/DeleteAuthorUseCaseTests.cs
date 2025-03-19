using Library.Application.UseCases.Authors;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Application.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Test.UseCasesTests
{
    public class DeleteAuthorUseCaseTests
    {
        private readonly Mock<ILibraryUnitOfWork> _mockUnitOfWork;
        private readonly DeleteAuthorUseCase _useCase;

        public DeleteAuthorUseCaseTests()
        {
            _mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            _useCase = new DeleteAuthorUseCase(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenAuthorNotFound()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            
            var authorId = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsync(authorId, cancellationToken)).ReturnsAsync((AuthorEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(() => _useCase.ExecuteAsync(authorId, cancellationToken));
            Assert.Equal($"Error on DeleteAuthorUseCase: no such author, id = {authorId}", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteAuthor_WhenAuthorExists()
        {

            // Arrange
            var cancellationToken = CancellationToken.None;
            var authorId = Guid.NewGuid();

            // Создайте экземпляр AuthorEntity, который будет использоваться для удаления
            var authorEntity = new AuthorEntity { Id = authorId };

            // Настройте мок, чтобы вернуть созданный объект
            _mockUnitOfWork.Setup(u => u.authorRepository.GetByIdAsync(authorId, cancellationToken))
                .ReturnsAsync(authorEntity);

            // Act
            await _useCase.ExecuteAsync(authorId, cancellationToken);

            // Assert
            _mockUnitOfWork.Verify(u => u.authorRepository.Delete(authorEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(cancellationToken), Times.Once);
        }
    }
}
