namespace EnvironmentOrderService.Domain.Exceptions;

public class MemoryException() : DomainException("Memory is required.")
{
    public override string Code => "missing_or_wrong_memory";
}
