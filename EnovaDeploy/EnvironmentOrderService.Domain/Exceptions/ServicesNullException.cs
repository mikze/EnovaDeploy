namespace EnvironmentOrderService.Domain.Exceptions;

public class ServicesNullException() : DomainException("Services collection is required.")
{
    public override string Code => "missing_services_collection";
}
