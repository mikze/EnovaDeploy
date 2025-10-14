namespace EnvironmentOrderService.Domain.Exceptions;

public class RequestedByException() : DomainException("RequestedBy is required.")
{
    public override string Code => "missing_or_wrong_request_author";
}