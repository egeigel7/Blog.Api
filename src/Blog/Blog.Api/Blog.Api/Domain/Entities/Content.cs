using Blog.Api.Domain.ValueObjects;

namespace Blog.Api.Domain.Entities
{
    public class Content
    {
        Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        ContentStatus Status { get; set; }
    }
}
