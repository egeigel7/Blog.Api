using Blog.Api.Domain;

namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public record class CreateDraftCommand: ICommand
    {
        public CreateDraftCommand(CreateDraftRequest request)
        {
            Request = request;
        }
        public CreateDraftRequest Request { get; set; }
    }
}
