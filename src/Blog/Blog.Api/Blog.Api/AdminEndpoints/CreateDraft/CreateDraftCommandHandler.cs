using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Blog.Api.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public class CreateDraftCommandHandler: ICommandHandlerAsync<CreateDraftCommand, Guid>
    {
        private readonly IContentRepository _repository;
        public CreateDraftCommandHandler(IContentRepository repository) { _repository = repository; }
        public async Task<Guid> HandleAsync(CreateDraftCommand command, CancellationToken cancellationToken = default)
        {
            var newId = Guid.NewGuid();
            var content = new Content(newId, command.Request.Title, command.Request.Body, Domain.ValueObjects.ContentStatus.Unpublished);
            content.Apply(new ContentCreated(newId, command.Request.Title));
            content.Apply(new ContentSaved(newId, command.Request.Body));
            await _repository.SaveAsync(content, cancellationToken);
            return newId;
        }
    }
}
