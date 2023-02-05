using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.AdminEndpoints
{
    public static class RegisterEndpoints
    {
        public static IEndpointRouteBuilder MapAdminEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/createDraft", async(
                HttpContext context,
                CreateDraftRequest createDraftRequest,
                [FromServices] ICommandHandlerAsync<CreateDraftCommand, Guid> _handler,
                [FromServices] ILogger<Program> _logger,
                CancellationToken cancellation) =>
            {
                var contentId = _handler.HandleAsync(new CreateDraftCommand(createDraftRequest), cancellation);
                return contentId;
            })
            .WithName("CreateDraft");
            //endpoints.MapPost("/emails", async (
            //    HttpContext context,
            //    SendEmailRequest sendEmailRequest,
            //    [FromServices] ICommandHandlerAsync<SendEmailCommand, Guid> _handler,
            //    [FromServices] ILogger<Program> _logger,
            //    CancellationToken cancellation) =>
            //{
            //    _logger.EnteringAsyncMethod();

            //    var clientId = "SomeFakeClientId"; //TODO: Set ClientId to authenticated user id
            //    Guid? emailNotificationId = null;

            //    try
            //    {
            //        var correlationId = context.GetItemValueOrDefault<string>(ApiConstants.CorrelationIdHeaderKey);

            //        emailNotificationId = await _handler.HandleAsync(
            //            new SendEmailCommand(sendEmailRequest, clientId, correlationId),
            //            cancellation);
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex.ToString(), ex);

            //        _logger.ExitingAsyncMethod();

            //        return Results.Problem("500: Internal Server error");
            //    }

            //    _logger.ExitingAsyncMethod();

            //    return Results.Accepted(null, new SendEmailResponse(
            //        true,
            //        ((int)HttpStatusCode.Accepted).ToString(),
            //        emailNotificationId.Value,
            //        null,
            //        null));
            //})
            //.ProducesValidationProblem((int)HttpStatusCode.BadRequest)
            //.Produces((int)HttpStatusCode.Accepted, typeof(SendEmailResponse))
            //.WithTags("Emails")
            //.WithName("SendEmail");


            return endpoints;
        }
    }
}
