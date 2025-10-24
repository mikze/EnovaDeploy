using System.Net;
using Common;
using HelmSonetaGenerator;
using HelmSonetaGenerator.Entities;
using HelmSonetaGenerator.Generators;
using HelmSonetaGeneratorAPI.GitRepo;
using HelmSonetaGeneratorApp.Services.RepoUrlProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepoUrlProvider>(sp =>
    new RepoUrlProvider("https://github.com/mikze/helm-charts.git", "mikze"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Handl());
app.MapPost("/upload", new GitHandler().CreateAndUpload);
app.MapGet("/check", (string path) => new GitHandler().CheckFile(path));
app.MapDelete("/delete", (string path) => new GitHandler().DeleteFile(path));

app.Run();

string Handl()
{
    return "pol";
}