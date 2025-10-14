using MediatR;

namespace EnvironmentOrderService.Application.Commands;

public record AddEnvironmentOrder(string Name, string Type, IDictionary<string,string> Attributes) : IRequest;