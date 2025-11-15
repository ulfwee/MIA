using MongoDB.Driver;

namespace SortTest.Test;

public class MobgoDBClient
{
    private static IMongoDatabase _db;
    private static MobgoDBClient _instance;

    public static MobgoDBClient Instance
    {
        get => _instance ?? new MobgoDBClient();
    }

    private MobgoDBClient()
    {
        var connectionString = "mongodb+srv://ulfwee:ulka99@masterbooking.dvpcegv.mongodb.net/?appName=Masterbooking"; 
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase("mastersBooking");

    }

    public IMongoCollection<T> GetCollection<T>(string name) => _db.GetCollection<T>(name);
}
