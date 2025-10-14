using EnvironmentOrderService.Application.Commands;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddEnvironmentOrder).Assembly);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/resources", async (AddEnvironmentOrder cmd, IMediator mediator, CancellationToken ct) =>
{
    await mediator.Send(cmd, ct);
    return Results.Accepted();
});

app.Run();