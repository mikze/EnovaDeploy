using System.Security;

namespace HelmSonetaGeneratorApp.Services.RepoUrlProvider;

public interface IRepoUrlProvider
{
    string RepoUrl { get; }
    string RepoOwner { get; }
}

public class RepoUrlProvider(string repoUrl, string repoOwner) : IRepoUrlProvider
{
    public string RepoUrl { get; } = repoUrl;
    public string RepoOwner { get; } = repoOwner;
}