namespace EnvironmentOrderService.Domain.Exceptions;

public class ServicesEmptyException() : DomainException("At least one service is required.")
{
    public override string Code => "empty_services_collection";
}
