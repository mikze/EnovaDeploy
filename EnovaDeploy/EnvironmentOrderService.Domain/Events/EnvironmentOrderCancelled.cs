namespace EnvironmentOrderService.Domain.Events;

public record EnvironmentOrderCancelled(Guid OrderId, DateTime CancelledAt, string Reason) : IDomainEvent;