namespace EnvironmentOrderService.Domain.Exceptions;

public class ChartNameException() : DomainException("ChartName is required.")
{
    public override string Code => "missing_or_wrong_chart_name";
}
