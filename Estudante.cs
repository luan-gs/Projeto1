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
    public string Email {get; private set; }
    public bool Ativo {get; private set; }

    public Estudante(string nome, string cpf, string email)
    {
        if (string.IsNullOrEmpty(nome?.Trim()))
            throw new Exception("O nome do estudante é obrigatório.");
        if (string.IsNullOrEmpty(CPF?.Trim()))
            throw new Exception("O CPF do estudante é obrigatório.");
        if (string.IsNullOrEmpty(Email?.Trim()))
            throw new Exception("O e-mail do estudante é obrigatório.");
        string[] vetorEmail = email.Split("@");
        if (vetorEmail.Length != 2)
            throw new Exception("E-mail inválido.");
        
        if (string.IsNullOrEmpty(vetorEmail[0]?.Trim()))
            throw new Exception("A primeira parte do e-mail não foi informada.");

        if (!vetorEmail[1].Contains("."))
            throw new Exception("E-mail inválido.");
         
        string[] vetorEmail2 = vetorEmail[1].Split(".");
        if (vetorEmail2.Length < 2 || string.IsNullOrEmpty(vetorEmail2[0]) || string.IsNullOrEmpty(vetorEmail2[1]))
            throw new Exception("E-mail inválido.");
        
        if (string.IsNullOrEmpty(vetorEmail[1]?.Trim()))
            throw new Exception("A segunda parte do e-mail não foi informada.");
        
        Nome = nome;
        CPF = cpf;
        Email = email;
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