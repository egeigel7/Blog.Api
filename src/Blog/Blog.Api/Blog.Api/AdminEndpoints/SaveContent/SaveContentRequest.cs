using FluentValidation;

namespace Blog.Api.AdminEndpoints.SaveContent
{
    public record class SaveContentRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
    }

    public class SaveContentRequestValidator : AbstractValidator<SaveContentRequest>
    {
        public SaveContentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
