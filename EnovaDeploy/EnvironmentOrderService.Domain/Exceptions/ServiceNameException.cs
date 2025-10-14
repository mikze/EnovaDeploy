namespace EnvironmentOrderService.Domain.Exceptions;

public class ServiceNameException() : DomainException("ServiceName is required.")
{
    public override string Code => "missing_or_wrong_service_name";
}
