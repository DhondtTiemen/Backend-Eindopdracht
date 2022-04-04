namespace Eindopdracht.Repositories;

public interface ISetRepository
{
    Task<Set> AddSet(Set newSet);
    Task<List<Set>> GetAllSets();
    Task<Set> GetSet(int id);
}

//GET SETS BY THEME

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

    //GET SETS BY ID
    public async Task<Set> GetSet(int number)
    {
        return await _context.SetCollection.Find<Set>(s => s.SetNumber == number).FirstOrDefaultAsync();
    }


    //ADD SET
    public async Task<Set> AddSet(Set newSet)
    {
        await _context.SetCollection.InsertOneAsync(newSet);
        return newSet;
    }
}