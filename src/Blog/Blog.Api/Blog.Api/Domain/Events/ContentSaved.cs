namespace Blog.Api.Domain.Events
{
    public class ContentSaved
    {
        public ContentSaved(Guid id, string body)
        {
            Id = id;
            Body = body;
        }
        Guid Id { get; set; }
        public string Body { get; set; }
    }
}
