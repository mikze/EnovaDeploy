using System.Reflection.Metadata;

namespace HelmSonetaGenerator.Generators;

public interface IGenerator
{
    string Create(Document document);
    bool Upload(string content);
    bool Update(Document document);
    bool Delete(Guid id);
    bool Exists(Guid id);
}