using Blog.Api.Domain.Events;
using Blog.Api.Domain.ValueObjects;
using Marten.Events.Aggregation;

namespace Blog.Api.Domain.Entities.Aggregates
{
    public class Content: SingleStreamAggregation<Entities.Content>
    {
        public Content()
        {
        }

        internal Content(ContentCreated contentCreated)
                    {
            Title = contentCreated.Title;
            Status = ContentStatus.Unpublished;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public ContentStatus Status { get; set; }
        public int Version { get; set; }

        public void Apply(ContentSaved contentSaved)
        {
            Body = contentSaved.Body;
        }
    }

}
