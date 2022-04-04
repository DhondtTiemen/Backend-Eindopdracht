namespace Eindopdracht.Repositories;

public class ThemeRepository
{
    private readonly IMongoContext _context;

    public ThemeRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET THEMES
    public async Task<List<Theme>> GetAllThemes()
    {
        return await _context.ThemeCollection.Find(_ => true).ToListAsync();
    }

    //ADD THEME
    public async Task<Theme> AddTheme(Theme newTheme)
    {
        await _context.ThemeCollection.InsertOneAsync(newTheme);
        return newTheme;
    }
}