using FluentValidation;

namespace Blog.Api.AdminEndpoints.CreateDraft
{
    public record class CreateDraftRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class CreateDraftRequestValidator : AbstractValidator<CreateDraftRequest>
    {
        public CreateDraftRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required.");
        }
    }
}
