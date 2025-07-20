using System;
using Blog.Api.AdminEndpoints.SaveContent;
using Xunit;

namespace Blog.Api.Tests
{
    public class SaveContentRequestValidatorTests
    {
        private readonly SaveContentRequestValidator _validator = new SaveContentRequestValidator();

        [Fact]
        public void ValidRequest_PassesValidation()
        {
            var request = new SaveContentRequest { Id = Guid.NewGuid(), Content = "Valid Content" };
            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EmptyId_FailsValidation()
        {
            var request = new SaveContentRequest { Id = Guid.Empty, Content = "Valid Content" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id");
        }

        [Fact]
        public void EmptyContent_FailsValidation()
        {
            var request = new SaveContentRequest { Id = Guid.NewGuid(), Content = "" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Content");
        }
    }
} 