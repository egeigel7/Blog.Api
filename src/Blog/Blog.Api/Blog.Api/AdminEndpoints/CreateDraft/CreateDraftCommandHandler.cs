using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Blog.Api.Domain.Events;
using Marten;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public class CreateDraftCommandHandler: ICommandHandlerAsync<CreateDraftCommand, Guid>
    {
        IDocumentSession _session;
        public CreateDraftCommandHandler(IDocumentSession session) { _session = session; }
        public async Task<Guid> HandleAsync(CreateDraftCommand command, CancellationToken cancellationToken = default)
        {
            var newId = Guid.NewGuid();
            var contentCreated = new ContentCreated(newId, command.Request.Title);
            var contentSaved = new ContentSaved(newId, command.Request.Body);
            _session.Events.StartStream<Content>(newId, contentCreated, contentSaved);

            await _session.SaveChangesAsync();

            return newId;
        }
    }
}
