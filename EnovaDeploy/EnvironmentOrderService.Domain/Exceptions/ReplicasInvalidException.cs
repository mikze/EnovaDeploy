namespace EnvironmentOrderService.Domain.Exceptions;

public class ReplicasException() : DomainException("Replicas must be >= 1.")
{
    public override string Code => "invalid_replicas";
}
