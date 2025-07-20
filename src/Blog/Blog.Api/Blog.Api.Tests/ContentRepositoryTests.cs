using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Marten;
using Moq;
using Xunit;

namespace Blog.Api.Tests
{
    public class ContentRepositoryTests
    {
        [Fact]
        public async Task SaveAsync_CallsSaveChangesAsyncOnSession()
        {
            // Arrange
            var mockSession = new Mock<IDocumentSession>();
            var repo = new ContentRepository(mockSession.Object);
            var content = new Content(Guid.NewGuid(), "title", "body", Blog.Api.Domain.ValueObjects.ContentStatus.Unpublished);

            // Act
            await repo.SaveAsync(content);

            // Assert
            mockSession.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

    public class ContentAggregateTests
    {
        [Fact]
        public void Apply_ContentCreated_SetsTitleAndStatus()
        {
            // Arrange
            var content = new Content();
            var evt = new Blog.Api.Domain.Events.ContentCreated(Guid.NewGuid(), "Test Title");

            // Act
            content.Apply(evt);

            // Assert
            Assert.Equal("Test Title", content.Title);
            Assert.Equal(Blog.Api.Domain.ValueObjects.ContentStatus.Unpublished, content.Status);
        }

        [Fact]
        public void Apply_ContentSaved_UpdatesBody()
        {
            // Arrange
            var content = new Content(Guid.NewGuid(), "title", "old body", Blog.Api.Domain.ValueObjects.ContentStatus.Unpublished);
            var evt = new Blog.Api.Domain.Events.ContentSaved(content.Id, "new body");

            // Act
            content.Apply(evt);

            // Assert
            Assert.Equal("new body", content.Body);
        }
    }
} 