namespace Blog.Api.AdminEndpoints.SaveContent
{
    public record class SaveContentRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
