namespace HelmSonetaGenerator.Generators;

public interface IGitRepositoryClient
{
    // Upserts a file at repoPath (e.g., "apps/soneta2/application.yaml")
    Task UpsertFileAsync(string owner, string repo, string branch, string path, string content, string commitMessage, string patToken);
    Task DeleteFileAsync(string owner, string repo, string branch, string path, string commitMessage, string patToken);
    Task<HttpResponseMessage> FileExistsAsync(string owner, string repo, string branch, string path, string patToken);
}