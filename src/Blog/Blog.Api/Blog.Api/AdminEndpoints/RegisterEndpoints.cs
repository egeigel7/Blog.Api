using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.AdminEndpoints.SaveContent;
using Blog.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.AdminEndpoints
{
    public static class RegisterEndpoints
    {
        public static IEndpointRouteBuilder MapAdminEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/createDraft", async (
                HttpContext context,
                CreateDraftRequest createDraftRequest,
                [FromServices] ICommandHandlerAsync<CreateDraftCommand, Guid> _handler,
                [FromServices] ILogger<Program> _logger,
                CancellationToken cancellation) =>
            {
                var contentId = await _handler.HandleAsync(new CreateDraftCommand(createDraftRequest), cancellation);
                return contentId;
            })
            .WithName("CreateDraft");

            endpoints.MapPost("/saveContent", async (
            HttpContext context,
            SaveContentRequest saveContentRequest,
            [FromServices] ICommandHandlerAsync<SaveContentCommand> _handler,
            [FromServices] ILogger<Program> _logger,
            CancellationToken cancellation) =>
            {
                await _handler.HandleAsync(new SaveContentCommand(saveContentRequest), cancellation);
            })
            .WithName("saveContent");

            return endpoints;
        }
    }
}
