using System.Reflection.Metadata;

namespace HelmSonetaGenerator.Generators;

public class Generator : IGenerator
{
    public string Create(Document document)
    {
        throw new NotImplementedException();
    }

    public bool Upload(string content)
    {
        throw new NotImplementedException();
    }

    public bool Update(Document document)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Guid id)
    {
        throw new NotImplementedException();
    }
}