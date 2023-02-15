using Blog.Api.AdminEndpoints;
using Blog.Api.AdminEndpoints.CreateDraft;
using Blog.Api.AdminEndpoints.SaveContent;
using Blog.Api.Domain;
using Blog.Api.Domain.Entities.Aggregates;
using Marten;
using Microsoft.Extensions.Options;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICommandHandlerAsync<CreateDraftCommand, Guid>, CreateDraftCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<SaveContentCommand>, SaveContentCommandHandler>();

builder.Services.AddMarten(opts =>
{
    var connString = builder.Configuration.GetConnectionString("marten");

    opts.Connection(connString);
    opts.Projections.SelfAggregate<Content>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    //If not using self-aggregates
    //opts.Projections.Add<ContentAggregation>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    // If using Environment.IsDevelopment()
    // opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/getAllContent", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateTime.Now.AddDays(index),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetAllContent");

RegisterEndpoints.MapAdminEndpoints(app);


app.Run();