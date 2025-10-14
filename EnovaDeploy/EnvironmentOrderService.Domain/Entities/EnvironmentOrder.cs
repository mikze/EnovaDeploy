using EnvironmentOrderService.Domain.Enums;
using EnvironmentOrderService.Domain.Events;
using EnvironmentOrderService.Domain.Exceptions;
using EnvironmentOrderService.Domain.ValueObjects;

namespace EnvironmentOrderService.Domain.Entities;

public class EnvironmentOrder : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string RequestedBy { get; private set; }
    public string EnvironmentName { get; private set; }
    public string EnvironmentVersion { get; private set; }
    public List<EnvironmentService> Services { get; private set; }
    public EnvironmentOrderStatus Status { get; private set; }

    public EnvironmentOrder(string requestedBy, string environmentName, string environmentVersion, List<EnvironmentService> services) 
    {
        Validate(requestedBy, environmentName, environmentVersion, services);
        OrderId = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        RequestedBy = requestedBy;
        EnvironmentName = environmentName;
        EnvironmentVersion = environmentVersion;
        Services = services;
    }
    
    public void Publish()
    {
        if (Status == EnvironmentOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot publish a cancelled order.");
        if (Status == EnvironmentOrderStatus.Published)
            return;

        Status = EnvironmentOrderStatus.Published;
        AddEvent(new EnvironmentOrderPublished(OrderId, DateTime.UtcNow));
    }
    
    public void Cancel(string reason)
    {
        if (Status == EnvironmentOrderStatus.Cancelled)
            return;

        Status = EnvironmentOrderStatus.Cancelled;
        AddEvent(new EnvironmentOrderCancelled(OrderId, DateTime.UtcNow, reason));
    }
    
    public void UpdateServices(IEnumerable<EnvironmentService> services)
    {
        Validate(RequestedBy, EnvironmentName, EnvironmentVersion, services);
        Services = services.ToList();
        AddEvent(new EnvironmentOrderUpdated(OrderId, DateTime.UtcNow, Services));
    }
    
    private static void Validate(
        string requestedBy,
        string environmentName,
        string version,
        IEnumerable<EnvironmentService> services)
    {
        if (string.IsNullOrWhiteSpace(requestedBy))
            throw new RequestedByException();
        if (string.IsNullOrWhiteSpace(environmentName))
            throw new EnvironmentNameException();
        if (string.IsNullOrWhiteSpace(version))
            throw new VersionException();
        if (services is null)
            throw new ServicesNullException();

        var list = services.ToList();
        if (list.Count == 0)
            throw new ServicesEmptyException();

        foreach (var s in list)
        {
            if (string.IsNullOrWhiteSpace(s.ServiceName))
                throw new ServiceNameException();
            if (string.IsNullOrWhiteSpace(s.ChartName))
                throw new ChartNameException();
            if (string.IsNullOrWhiteSpace(s.ChartVersion))
                throw new ChartVersionException();
            if (s.Replicas < 1)
                throw new ReplicasException();
            if (string.IsNullOrWhiteSpace(s.Cpu))
                throw new CpuException();
            if (string.IsNullOrWhiteSpace(s.Memory))
                throw new MemoryException();
        }
    }
}