using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TFC.Domain.Model.Entity;

public class ApplicationDbContext
{
    private readonly IMongoDatabase _database;

    public ApplicationDbContext(IConfiguration config)
    {
        // Acceso directo a appsettings.json
        var connectionString = config["MongoDBSettings:ConnectionString"]!;
        var dbName = config["MongoDBSettings:DatabaseName"]!;

        _database = new MongoClient(connectionString).GetDatabase(dbName);
    }

    // Ejemplo de propiedad tipo DbSet (opcional)
    public IMongoCollection<User> Users => _database.GetCollection<User>("user");
    public IMongoCollection<Routine> Routines => _database.GetCollection<Routine>("routine");

    // Método genérico para otras colecciones
    public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
}