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
    public class GetAllAuthorsUseCaseTests
    {
        private readonly Mock<ILibraryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllAuthorsUseCase _useCase;

        public GetAllAuthorsUseCaseTests()
        {
            _mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAllAuthorsUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenNoAuthorsFound()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUnitOfWork.Setup(u => u.authorRepository.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), cancellationToken))
                           .ReturnsAsync((PagedItems<AuthorEntity>)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(() => _useCase.ExecuteAsync(1, 10, cancellationToken));
            Assert.Equal("Error on GetAllAuthorsUseCase: list was empty", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnPagedAuthors_WhenAuthorsExist()
        {
            // Arrange
            var authorEntities = new PagedItems<AuthorEntity>
            {
                Items = new List<AuthorEntity>
            {
                new AuthorEntity { Id = Guid.NewGuid(), FirstName = "John", Surname = "Doe" },
                new AuthorEntity { Id = Guid.NewGuid(), FirstName = "Jane", Surname = "Doe" }
            },
                TotalCount = 2,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };
            var cancellationToken = CancellationToken.None;
            _mockUnitOfWork.Setup(u => u.authorRepository.GetAllAsync(1, 10, cancellationToken)).ReturnsAsync(authorEntities);
            _mockMapper.Setup(m => m.Map<List<AuthorModel>>(It.IsAny<List<AuthorEntity>>()))
                       .Returns(new List<AuthorModel>
                       {
                       new AuthorModel { Id = authorEntities.Items[0].Id, FirstName = authorEntities.Items[0].FirstName, Surname = authorEntities.Items[0].Surname },
                       new AuthorModel { Id = authorEntities.Items[1].Id, FirstName = authorEntities.Items[1].FirstName, Surname = authorEntities.Items[1].Surname }
                       });

            // Act
            var result = await _useCase.ExecuteAsync(1, 10, cancellationToken);

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.Items.Count);
        }
    }
}
