using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.Domain.Entities.Aggregates;
using Marten;

namespace Blog.Api.Domain
{
    public class ContentRepository : IContentRepository
    {
        private readonly IDocumentSession _session;
        public ContentRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Content?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Use Marten's event sourcing to aggregate the Content
            var content = await _session.Events.AggregateStreamAsync<Content>(id, token: cancellationToken);
            return content;
        }

        public async Task SaveAsync(Content aggregate, CancellationToken cancellationToken = default)
        {
            // Marten saves changes via events, so SaveAsync is a no-op here
            await _session.SaveChangesAsync(cancellationToken);
        }
    }
} 