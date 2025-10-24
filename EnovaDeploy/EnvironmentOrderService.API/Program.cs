using EnvironmentOrderService.Application.Commands;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddEnvironmentOrderCommand).Assembly);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/resources", async (AddEnvironmentOrderCommand cmd, IMediator mediator, CancellationToken ct) =>
{
    await mediator.Send(cmd, ct);
    return Results.Accepted();
});

app.Run();