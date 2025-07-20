using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Moq;
using Xunit;

namespace Blog.Api.Tests
{
    public class CreateDraftCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_CreatesDraftAndSavesToRepository()
        {
            // Arrange
            var mockRepo = new Mock<IContentRepository>();
            var handler = new CreateDraftCommandHandler(mockRepo.Object);
            var request = new CreateDraftRequest { Title = "Test Title", Body = "Test Body" };
            var command = new CreateDraftCommand(request);

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.SaveAsync(It.IsAny<Content>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<Guid>(result);
        }
    }
} 