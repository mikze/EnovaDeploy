using EnvironmentOrderService.Domain.Entities;

namespace EnvironmentOrderService.Domain.Repositories;

public interface IEnvironmentOrderRepository
{
    Task<EnvironmentOrder> GetAsync(AggregateId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(EnvironmentOrder resource);
    Task UpdateAsync(EnvironmentOrder resource);
    Task DeleteAsync(AggregateId id);
}