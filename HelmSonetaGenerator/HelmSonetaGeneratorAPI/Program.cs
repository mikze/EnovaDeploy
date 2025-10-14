using System.Net;
using HelmSonetaGenerator;
using HelmSonetaGenerator.Entities;
using HelmSonetaGenerator.Generators;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Handl());
app.MapGet("/upload", () => CreateAndUpload());
app.MapGet("/check", () => CheckFile());

app.Run();


string Handl()
{
    return "pol";
}

async Task<bool> CheckFile()
{
    bool toReturn;
    using (var httpClient = new HttpClient())
    {
        var gitClient = new GitRepositoryClient(httpClient);
        toReturn = await gitClient.FileExistsAsync("mikze", "argocd-demo", "main", "envs/default/apps/soneta.yaml", Cred.GitHubPat);
    }

    return toReturn;
}
object CreateAndUpload()
{
    var file =  new HelmYamlGenerator().GenerateApplicationYaml(new Document()
    {
        AppName = "soneta33",
        Namespace = "soneta33",
        RepoUrl = "https://github.com/mikze/helm-charts.git",
        Path = "charts/soneta",
        TargetRevision = "main"
    });
    object toReturn = null;
    using (var httpClient = new HttpClient())
    {
        var gitClient = new GitRepositoryClient(httpClient);
        toReturn = gitClient.UpsertFileAsync("mikze", "argocd-demo", "main", "envs/default/apps/ApiTest.yaml", file,"TEST COMMIT VIA API", Cred.GitHubPat);
    }

    return new HttpResponseMessage(HttpStatusCode.Created);
    //UpsertFileAsync
}