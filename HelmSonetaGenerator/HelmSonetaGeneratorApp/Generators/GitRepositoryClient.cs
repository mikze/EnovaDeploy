using System.Net.Http.Headers;
using System.Text;

namespace HelmSonetaGenerator.Generators;

public class GitRepositoryClient : IGitRepositoryClient
{
    public async Task<HttpResponseMessage> FileExistsAsync(string owner, string repo, string branch, string path, string patToken)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{owner}/{repo}/contents/{Uri.EscapeDataString(path)}?ref={Uri.EscapeDataString(branch)}");
        AddAuth(req, patToken);
        var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("HelmSonetaGeneratorAPI/1.0");
        return await client.SendAsync(req);
    }

    public async Task UpsertFileAsync(string owner, string repo, string branch, string path, string content, string commitMessage, string patToken)
    {
        string? existingSha = await GetFileShaOrNull(owner, repo, branch, path, patToken);
        var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("HelmSonetaGeneratorAPI/1.0");
        var payload = new
        {
            message = commitMessage,
            content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            branch,
            sha = existingSha
        };

        using var req = new HttpRequestMessage(HttpMethod.Put, $"https://api.github.com/repos/{owner}/{repo}/contents/{Uri.EscapeDataString(path)}")
        {
            Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        AddAuth(req, patToken);

        using var resp = await client.SendAsync(req);
        resp.EnsureSuccessStatusCode();
    }

    public async Task DeleteFileAsync(string owner, string repo, string branch, string path, string commitMessage, string patToken)
    {
        string? existingSha = await GetFileShaOrNull(owner, repo, branch, path, patToken);
        if (existingSha is null) return;

        var payload = new
        {
            message = commitMessage,
            branch,
            sha = existingSha
        };

        using var req = new HttpRequestMessage(HttpMethod.Delete, $"https://api.github.com/repos/{owner}/{repo}/contents/{Uri.EscapeDataString(path)}")
        {
            Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        AddAuth(req, patToken);

        //using var resp = await _http.SendAsync(req);
        //resp.EnsureSuccessStatusCode();
    }

    private async Task<string?> GetFileShaOrNull(string owner, string repo, string branch, string path, string patToken)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("HelmSonetaGeneratorAPI/1.0");
        using var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{owner}/{repo}/contents/{Uri.EscapeDataString(path)}?ref={Uri.EscapeDataString(branch)}");
        AddAuth(req, patToken);
        using var resp = await client.SendAsync(req);
        if (!resp.IsSuccessStatusCode) return null;
        var json = await resp.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        if (doc.RootElement.TryGetProperty("sha", out var shaProp))
            return shaProp.GetString();
        return null;
    }

    private static void AddAuth(HttpRequestMessage req, string patToken)
    {
        if (!string.IsNullOrWhiteSpace(patToken))
        {
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", patToken);
        }
    }
}