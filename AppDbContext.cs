using ApiCrud.Estudantes;
using MongoDB.Driver;

namespace ApiCrud.Data;

public class AppDbContext
{
    private readonly IMongoDatabase _database;
    
    public AppDbContext()
    {
        var connectionString = "mongodb://localhost:5182";
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("ApiCrudDB");

        var collection = _database.GetCollection<Estudante>("Estudantes");
        var indexKeys = Builders<Estudante>.IndexKeys.Ascending(e => e.CPF);
        var indexOptions = new CreateIndexOptions { Unique = true };
        collection.Indexes.CreateOne(new CreateIndexModel<Estudante>(indexKeys, indexOptions));
    }

    public IMongoCollection<Estudante> Estudantes => _database.GetCollection<Estudante>("Estudantes");
}