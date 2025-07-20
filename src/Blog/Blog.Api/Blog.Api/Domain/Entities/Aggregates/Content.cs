using Blog.Api.Domain.Events;
using Blog.Api.Domain.ValueObjects;

namespace Blog.Api.Domain.Entities.Aggregates
{
    public class Content
    {
        // Parameterless constructor for serialization/framework
        public Content() { }

        // Constructor for easy creation
        public Content(Guid id, string title, string body, ContentStatus status)
        {
            Id = id;
            Title = title;
            Body = body;
            Status = status;
        }

        // Apply methods for event sourcing
        public void Apply(ContentCreated contentCreated)
        {
            Title = contentCreated.Title;
            Status = ContentStatus.Unpublished;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public ContentStatus Status { get; set; }
        public int Version { get; set; }

        public void Apply(ContentSaved contentSaved)
        {
            Body = contentSaved.Body;
        }
    }

}
