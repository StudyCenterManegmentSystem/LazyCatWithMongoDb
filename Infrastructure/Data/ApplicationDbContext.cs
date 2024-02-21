

using Domain.Entities.Entity.Fans;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Rooms;
using Domain.Entities.Entity.Students;

namespace Infrastructure.Data;

public class ApplicationDbContext
{
    private readonly IMongoDatabase _database;

    public ApplicationDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<ApplicationUser> Users =>
        _database.GetCollection<ApplicationUser>("users");
    public IMongoCollection<ApplicationRole> Roles =>
        _database.GetCollection<ApplicationRole>("roles");

    public IMongoCollection<Fan> Fans =>
        _database.GetCollection<Fan>("fanlar");
    public IMongoCollection<Group> Groups =>
        _database.GetCollection<Group>("grouplar");
    public IMongoCollection<Payment> Payments =>
        _database.GetCollection<Payment>("tolovlar");
    public IMongoCollection<Room> Rooms =>
        _database.GetCollection<Room>("xonalar");
    public IMongoCollection<Student> Students =>
        _database.GetCollection<Student>("talabalar");

}
