namespace EnvironmentOrderService.Domain.Events;

public record EnvironmentOrderPublished(Guid OrderId, DateTime PublishedAt) : IDomainEvent;