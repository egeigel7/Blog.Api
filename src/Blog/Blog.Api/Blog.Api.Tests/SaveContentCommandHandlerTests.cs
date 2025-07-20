using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.AdminEndpoints.SaveContent;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Blog.Api.Domain.ValueObjects;
using Blog.Api.Domain.Events;
using Marten.Exceptions;
using Moq;
using Xunit;

namespace Blog.Api.Tests
{
    public class SaveContentCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ValidContent_UpdatesAndSaves()
        {
            // Arrange
            var content = new Content(Guid.NewGuid(), "title", "old body", ContentStatus.Unpublished);
            var mockRepo = new Mock<IContentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(content.Id, It.IsAny<CancellationToken>())).ReturnsAsync(content);
            var handler = new SaveContentCommandHandler(mockRepo.Object);
            var request = new SaveContentRequest { Id = content.Id, Content = "new body" };
            var command = new SaveContentCommand(request);

            // Act
            await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.SaveAsync(content, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("new body", content.Body);
        }

        [Fact]
        public async Task HandleAsync_ContentNotFound_Throws()
        {
            // Arrange
            var mockRepo = new Mock<IContentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Content?)null);
            var handler = new SaveContentCommandHandler(mockRepo.Object);
            var request = new SaveContentRequest { Id = Guid.NewGuid(), Content = "body" };
            var command = new SaveContentCommand(request);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidStreamOperationException>(() => handler.HandleAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task HandleAsync_InvalidStatus_Throws()
        {
            // Arrange
            var content = new Content(Guid.NewGuid(), "title", "body", ContentStatus.Published);
            var mockRepo = new Mock<IContentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(content.Id, It.IsAny<CancellationToken>())).ReturnsAsync(content);
            var handler = new SaveContentCommandHandler(mockRepo.Object);
            var request = new SaveContentRequest { Id = content.Id, Content = "new body" };
            var command = new SaveContentCommand(request);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCastException>(() => handler.HandleAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task HandleAsync_DuplicateContent_Throws()
        {
            // Arrange
            var content = new Content(Guid.NewGuid(), "title", "body", ContentStatus.Unpublished);
            var mockRepo = new Mock<IContentRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(content.Id, It.IsAny<CancellationToken>())).ReturnsAsync(content);
            var handler = new SaveContentCommandHandler(mockRepo.Object);
            var request = new SaveContentRequest { Id = content.Id, Content = "body" };
            var command = new SaveContentCommand(request);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidStreamOperationException>(() => handler.HandleAsync(command, CancellationToken.None));
        }
    }
} 