using Blog.Api.Domain.ValueObjects;

namespace Blog.Api.Domain.Aggregates
{
    internal class Content
    {
        Guid Id { get; set; }
        string Body { get; set; }
        ContentStatus Status { get; set; }
    }
}
