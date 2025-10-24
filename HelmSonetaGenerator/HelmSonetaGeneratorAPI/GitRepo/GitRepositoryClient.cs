using System.Net;
using Common;
using HelmSonetaGenerator;
using HelmSonetaGenerator.Entities;
using HelmSonetaGenerator.Generators;
using HelmSonetaGeneratorApp.Services.RepoUrlProvider;

namespace HelmSonetaGeneratorAPI.GitRepo;

public class GitHandler
{
    public async Task<object> CreateAndUpload(EnvironmentOrderDto dto, IRepoUrlProvider repoUrlProvider)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));
        if (dto.Services is null || dto.Services.Count == 0)
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("No services provided.")
            };

        var gitClient = new GitRepositoryClient();
        var generator = new HelmYamlGenerator();

        foreach (var service in dto.Services)
        {
            var document = new Document
            {
                AppName = service.ServiceName,
                Namespace = dto.EnvironmentName,
                RepoUrl = repoUrlProvider.RepoUrl,
                Path = $"charts/{service.ChartName}",
                TargetRevision = service.ChartVersion
            };

            var yaml = generator.GenerateApplicationYaml(document);

            var destinationPath = $"envs/{dto.EnvironmentName}/apps/{service.ServiceName}.yaml";
            var commitMessage =
                $"Provision {service.ServiceName} for {dto.EnvironmentName} v{dto.EnvironmentVersion} by {dto.RequestedBy}";

            await gitClient.UpsertFileAsync(owner: repoUrlProvider.RepoOwner, repo: "argocd-demo", branch: "main", path: destinationPath,
                content: yaml, commitMessage: commitMessage, patToken: Cred.GitHubPat);
        }

        return new HttpResponseMessage(HttpStatusCode.Created);
    }
    
    public async Task<HttpResponseMessage> CheckFile(string path)
    => await new GitRepositoryClient().FileExistsAsync("mikze", "argocd-demo", "main", path,
            Cred.GitHubPat);
        
    
    
    public async Task<HttpResponseMessage> DeleteFile(string path)
        => await new GitRepositoryClient().FileExistsAsync("mikze", "argocd-demo", "main", path,
            Cred.GitHubPat);
        
    
}