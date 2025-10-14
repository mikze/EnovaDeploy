using MediatR;

namespace EnvironmentOrderService.Application.Commands.Handlers;

public sealed class AddEnvironmentOrderHandler : IRequestHandler<AddEnvironmentOrder>
{
    public async Task Handle(AddEnvironmentOrder request, CancellationToken cancellationToken)
    {
        Console.WriteLine("AddResourceHandler");
        await Task.CompletedTask;
    }
}