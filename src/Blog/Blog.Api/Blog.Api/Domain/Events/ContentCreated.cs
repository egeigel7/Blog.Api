namespace Blog.Api.Domain.Events
{
    public record ContentCreated
    {
        public ContentCreated(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        Guid Id { get; set; }
        public string Title { get; set; }
    }
}
