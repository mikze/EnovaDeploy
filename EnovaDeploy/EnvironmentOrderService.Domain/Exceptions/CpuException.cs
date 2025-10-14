namespace EnvironmentOrderService.Domain.Exceptions;

public class CpuException() : DomainException("Cpu is required.")
{
    public override string Code => "missing_or_wrong_cpu";
}
