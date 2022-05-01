namespace Eindopdracht.Repositories;

public interface ISetRepository
{
    Task<List<Set>> GetAllSets();
    Task<Set> GetSetByNumber(int number);
    Task<List<Set>> GetSetsByTheme(string theme);
    Task<List<Set>> GetSetsByAge(int age);
    Task<List<Set>> GetSetsByPrice(double price);
    Task<Set> AddSet(Set newSet);
    Task<Set> UpdateSet(Set set);
    Task<Set> DeleteSet(int setNumber);
}

public class SetRepository : ISetRepository
{
    private readonly IMongoContext _context;

    public SetRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET SETS
    public async Task<List<Set>> GetAllSets()
    {
        return await _context.SetCollection.Find(_ => true).ToListAsync();
    }

    //GET SETS BY NUMBER
    public async Task<Set> GetSetByNumber(int number)
    {
        return await _context.SetCollection.Find<Set>(s => s.SetNumber == number).FirstOrDefaultAsync();
    }

    //GET SETS BY THEME
    public async Task<List<Set>> GetSetsByTheme(string theme)
    {
        return await _context.SetCollection.Find(s => s.Theme.Name == theme).ToListAsync();
    }

    //GET SETS BY MINIMAL AGE
    public async Task<List<Set>> GetSetsByAge(int age)
    {
        return await _context.SetCollection.Find(s => s.MinimalAge <= age).ToListAsync();
    }

    //GET SETS BY MINIMAL PRICE
    public async Task<List<Set>> GetSetsByPrice(double price)
    {
        return await _context.SetCollection.Find(s => s.Price <= price).ToListAsync();
    }

    //ADD SET
    public async Task<Set> AddSet(Set newSet)
    {
        try
        {
            await _context.SetCollection.InsertOneAsync(newSet);
            return newSet;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //UPDATE SET
    public async Task<Set> UpdateSet(Set set)
    {
        try
        {
            var filter = Builders<Set>.Filter.Eq("SetNumber", set.SetNumber);
            var update = Builders<Set>.Update.Set("Name", set.Name);
            update = Builders<Set>.Update.Set("MinimalAge", set.MinimalAge);
            update = Builders<Set>.Update.Set("Pieces", set.Pieces);
            update = Builders<Set>.Update.Set("Price", set.Price);
            var result = await _context.SetCollection.UpdateOneAsync(filter, update);
            return await GetSetByNumber(set.SetNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE SET
    public async Task<Set> DeleteSet(int setNumber)
    {
        try
        {
            var filter = Builders<Set>.Filter.Eq("SetNumber", setNumber);
            var result = await _context.SetCollection.DeleteOneAsync(filter);
            return await GetSetByNumber(setNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}