namespace EnvironmentOrderService.Domain.Exceptions;

public class InvalidAggregateIdException(Guid id) : DomainException($"Invalid aggregate id: {id}")
{
    public override string Code { get; } = "invalid_aggregate_id";
    public Guid Id { get; } = id;
}