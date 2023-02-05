namespace Blog.Api.Domain.Events
{
    internal record ContentCreated
    {
        public ContentCreated(Guid id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }
        Guid Id { get; set; }
        internal string Title { get; set; }
        internal string Body { get; set; }
    }
}
