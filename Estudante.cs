using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ApiCrud.Estudantes;

public class Estudante
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id {get; init; }
    public string Nome { get; private set; }
    [BsonRequired]
    public string CPF {get; private set; }
    public bool Ativo {get; private set; }

    public Estudante(string nome)
    {
        if (string.IsNullOrEmpty(nome?.Trim()))
            throw new Exception("O nome do estudante é obrigatório.");
        if (string.IsNullOrEmpty(CPF?.Trim()))
            throw new Exception("O CPF do estudante é obrigatório.");
        Nome = nome;
        CPF = CPF;
        Id = Guid.NewGuid();
        Ativo = true;
    }

    public void AtualizarNome(string nome)
    {
        Nome = nome;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}