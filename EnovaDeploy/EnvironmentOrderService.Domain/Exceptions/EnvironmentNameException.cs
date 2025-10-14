namespace EnvironmentOrderService.Domain.Exceptions;

public class EnvironmentNameException() : DomainException("EnvironmentName is required.")
{
    public override string Code => "missing_or_wrong_environment_name";
}