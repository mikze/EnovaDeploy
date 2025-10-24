namespace Common;

public sealed class EnvironmentOrderDto
{
    public string RequestedBy { get; init; } = default!;
    public string EnvironmentName { get; init; } = default!;
    public string EnvironmentVersion { get; init; } = default!;
    public IReadOnlyList<EnvironmentServiceDto> Services { get; init; } = Array.Empty<EnvironmentServiceDto>();
}

public sealed class EnvironmentServiceDto
{
    public string ServiceName { get; init; } = default!;
    public string ChartName { get; init; } = default!;
    public string ChartVersion { get; init; } = default!;
    public int Replicas { get; init; }
    public string Cpu { get; init; } = default!;
    public string Memory { get; init; } = default!;
}

// Optional response DTO if the external service returns something
public sealed class EnvironmentOrderResponseDto
{
    public Guid OperationId { get; init; }
    public string Status { get; init; } = default!;
    public string? Message { get; init; }
}