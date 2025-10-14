namespace EnvironmentOrderService.Domain.Exceptions;

public class ChartVersionException() : DomainException("ChartVersion is required.")
{
    public override string Code => "missing_or_wrong_chart_version";
}
