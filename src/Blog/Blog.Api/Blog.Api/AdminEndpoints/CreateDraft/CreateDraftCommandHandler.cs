using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.Domain;
using Blog.Api.Domain.Aggregates;
using Blog.Api.Domain.Events;
using Marten;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public class CreateDraftCommandHandler: ICommandHandlerAsync<CreateDraftCommand, Guid>
    {
        public CreateDraftCommandHandler() { }
        public async Task<Guid> HandleAsync(CreateDraftCommand command, CancellationToken cancellationToken = default)
        {
            var newId = Guid.NewGuid();
            var store = DocumentStore.For("connection-string");

            using var session = store.LightweightSession();

            session.Events.StartStream<Content>(new ContentCreated(newId, command.Request.Title, command.Request.Body));

            await session.SaveChangesAsync();

            return newId;
        }
    }
}
