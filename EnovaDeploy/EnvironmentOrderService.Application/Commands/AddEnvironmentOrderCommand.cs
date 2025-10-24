using MediatR;

namespace EnvironmentOrderService.Application.Commands;

public record AddEnvironmentOrderCommand(
    string RequestedBy,
    string EnvironmentName,
    string EnvironmentVersion,
    IReadOnlyList<EnvironmentServiceDto> Services
) : IRequest;

public record EnvironmentServiceDto(
    string ServiceName,
    string ChartName,
    string ChartVersion,
    int Replicas,
    string Cpu,
    string Memory
);