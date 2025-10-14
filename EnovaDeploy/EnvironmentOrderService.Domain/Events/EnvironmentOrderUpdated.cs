using EnvironmentOrderService.Domain.ValueObjects;

namespace EnvironmentOrderService.Domain.Events;

public record EnvironmentOrderUpdated(Guid OrderId, DateTime UpdatedAt, IReadOnlyList<EnvironmentService> Services) : IDomainEvent;