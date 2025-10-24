using MediatR;

namespace EnvironmentOrderService.Application.Commands.Handlers;

public sealed class AddEnvironmentOrderHandler : IRequestHandler<AddEnvironmentOrderCommand>
{
    public async Task Handle(AddEnvironmentOrderCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("AddResourceHandler");
        await Task.CompletedTask;
    }
}