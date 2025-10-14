using System.Net.Http.Headers;
using System.Text;

namespace HelmSonetaGenerator.Generators;

public class GitRepositoryClient : IGitRepositoryClient
{
    private readonly HttpClient _http;

    public GitRepositoryClient(HttpClient httpClient)
    {
        _http = httpClient;
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("HelmSonetaGeneratorAPI/1.0");
    }

    public async Task<bool> FileExistsAsync(string owner, string repo, string branch, string path, string patToken)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{owner}/{repo}/contents/{Uri.EscapeDataString(path)}?ref={Uri.EscapeDataString(branch)}");
        AddAuth(req, patToken);
        using var resp = await _http.SendAsync(req);
        return resp.IsSuccessStatusCode;
    }

    public async Task UpsertFileAsync(string owner, string repo, string branch, string path, string content, string commitMessage, string patToken)
    {
        // Need existing file SHA to update; omit SHA to create.
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
    
    public async Task UpsertFileAsync2(string owner, string repo, string branch, string path, string _content,
        string commitMessage, string patToken)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, "https://api.github.com/repos/mikze/argocd-demo/contents/envs/default/apps/apitest2.yaml");
        AddAuth(request, patToken);
        var content = new StringContent("{\n    \"message\": \"Add empty test.yaml file\",\n    \"content\": \"\",\n    \"branch\": \"main\"\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
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

        using var resp = await _http.SendAsync(req);
        resp.EnsureSuccessStatusCode();
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