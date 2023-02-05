namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public record class CreateDraftRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
