using Blog.Api.AdminEndpoints.CreateDraft;
using Xunit;

namespace Blog.Api.Tests
{
    public class CreateDraftRequestValidatorTests
    {
        private readonly CreateDraftRequestValidator _validator = new CreateDraftRequestValidator();

        [Fact]
        public void ValidRequest_PassesValidation()
        {
            var request = new CreateDraftRequest { Title = "Valid Title", Body = "Valid Body" };
            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EmptyTitle_FailsValidation()
        {
            var request = new CreateDraftRequest { Title = "", Body = "Valid Body" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Title");
        }

        [Fact]
        public void TitleTooLong_FailsValidation()
        {
            var request = new CreateDraftRequest { Title = new string('a', 201), Body = "Valid Body" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Title");
        }

        [Fact]
        public void EmptyBody_FailsValidation()
        {
            var request = new CreateDraftRequest { Title = "Valid Title", Body = "" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Body");
        }
    }
} 