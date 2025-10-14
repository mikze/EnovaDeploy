using HelmSonetaGenerator.Entities;

namespace HelmSonetaGenerator.Generators;

public interface IHelmYamlGenerator
{
    string GenerateApplicationYaml(Document document);
}