namespace HelmSonetaGenerator.Entities;

public class Document
{
    // ArgoCD Application metadata
    public string AppName { get; set; } = "soneta2";
    public string Namespace { get; set; } = "soneta2";

    // ArgoCD source
    public string RepoUrl { get; set; } = "https://github.com/mikze/helm-charts.git";
    public string Path { get; set; } = "charts/soneta";
    public string TargetRevision { get; set; } = "main";

    // Image settings
    public string Product { get; set; } = "standard";
    public string Tag { get; set; } = "2506.1.1";
    public string WebTagPostfix { get; set; } = "-alpine";
    public string ServerTagPostfix { get; set; } = "-alpine";
    public string Repository { get; set; } = "";
    public bool Scheduler { get; set; } = false;
    public bool WebApi { get; set; } = false;
    public bool WebWcf { get; set; } = false;

    // Ingress
    public bool IngressEnabled { get; set; } = true;
    public string IngressClass { get; set; } = "nginx";
    public string IngressHost { get; set; } = "helloworld-v2.example.com";
    public string? IngressTlsSecretName { get; set; }

    // Database (MsSql)
    public string DbServer { get; set; } = "51.83.187.245,1433\\ENOVA";
    public string DbName { get; set; } = "bpx";
    public string DbUser { get; set; } = "sa";
    public string DbPassword { get; set; } = "=9A721079B885A991E053ABD30242168603";
    public bool DbTrusted { get; set; } = false;
    public bool DbTrustServerCertificate { get; set; } = true;

    // Other
    public bool AdminMode { get; set; } = false;
    public int ReplicaCount { get; set; } = 1;
    public string InitializationCommand { get; set; } =
        "curl -L http://51.83.187.245:8088/Samples.dll -o /dodatki/Samples.dll && chmod 644 /dodatki/Samples.dll && curl -L http://51.83.187.245:8088/Soneta.e-Sklepy.dll -o /dodatki/Soneta.e-Sklepy.dll && chmod 644 /dodatki/Soneta.e-Sklepy.dll && curl -L http://51.83.187.245:8088/Soneta.Integrator.dll -o /dodatki/Soneta.Integrator.dll && chmod 644 /dodatki/Soneta.Integrator.dll";
}