using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Blog.Api.Domain.Events;
using Marten;
using Marten.Exceptions;

namespace Blog.Api.AdminEndpoints.SaveContent
{
    public class SaveContentCommandHandler: ICommandHandlerAsync<SaveContentCommand>
    {
        IDocumentSession _session;
        public SaveContentCommandHandler(IDocumentSession session) { _session = session; }
        public async Task HandleAsync(SaveContentCommand command, CancellationToken cancellationToken = default)
        {
            // Grab current state

            // Use FetchForWriting for built-in optimistic concurrency checks
            var stream = await _session.Events.FetchForWriting<Content>(command.Request.Id);
            // Use AggregateStreamAsync if you want to do live aggregation
            //var content = await _session.Events.AggregateStreamAsync<Content>(command.Request.Id);
            var content = stream.Aggregate;


            // Validation
            if (content.Status != Domain.ValueObjects.ContentStatus.Unpublished)
            {
                throw new InvalidCastException("Invalid Status");
            }

            if (content.Body.Equals(command.Request.Content))
            {
                throw new InvalidStreamOperationException("Content is the same as the current body");
            }


            // Generate events to add
            ContentSaved saveEvent = new ContentSaved(command.Request.Id, command.Request.Content);


            // Append events to stream
            stream.AppendOne(saveEvent);

            await _session.SaveChangesAsync();
        }
    }
}
