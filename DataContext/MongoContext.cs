namespace Eindopdracht.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Set> SetCollection { get; }
    IMongoCollection<Theme> ThemeCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Set> SetCollection
    {
        get
        {
            return _database.GetCollection<Set>(_settings.SetCollection);
        }
    }

    public IMongoCollection<Theme> ThemeCollection
    {
        get
        {
            return _database.GetCollection<Theme>(_settings.ThemeCollection);
        }
    }
}