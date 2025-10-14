namespace EnvironmentOrderService.Domain.ValueObjects;

public record EnvironmentService(
    string ServiceName,
    string ChartName,
    string ChartVersion,
    int Replicas,
    string Cpu,
    string Memory,
    IReadOnlyDictionary<string, string> ConfigOverrides
);