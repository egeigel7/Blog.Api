using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Blog.Api.Domain.Events;
using Marten.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Api.AdminEndpoints.SaveContent
{
    public class SaveContentCommandHandler: ICommandHandlerAsync<SaveContentCommand>
    {
        private readonly IContentRepository _repository;
        public SaveContentCommandHandler(IContentRepository repository) { _repository = repository; }
        public async Task HandleAsync(SaveContentCommand command, CancellationToken cancellationToken = default)
        {
            var content = await _repository.GetByIdAsync(command.Request.Id, cancellationToken);
            if (content == null)
            {
                throw new InvalidStreamOperationException("Content not found");
            }
            if (content.Status != Domain.ValueObjects.ContentStatus.Unpublished)
            {
                throw new InvalidCastException("Invalid Status");
            }
            if (content.Body.Equals(command.Request.Content))
            {
                throw new InvalidStreamOperationException("Content is the same as the current body");
            }
            content.Apply(new ContentSaved(command.Request.Id, command.Request.Content));
            await _repository.SaveAsync(content, cancellationToken);
        }
    }
}
