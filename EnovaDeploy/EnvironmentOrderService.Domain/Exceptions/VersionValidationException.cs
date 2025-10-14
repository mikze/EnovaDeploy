namespace EnvironmentOrderService.Domain.Exceptions;

public class VersionException() : DomainException("Version is required.")
{
    public override string Code => "missing_or_wrong_version";
}
