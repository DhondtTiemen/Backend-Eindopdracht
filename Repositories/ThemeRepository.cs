namespace Eindopdracht.Repositories;

public interface IThemeRepository
{
    Task<List<Theme>> GetAllThemes();
    Task<Theme> GetThemeById(string themeId);
    Task<Theme> AddTheme(Theme newTheme);
    Task<Theme> UpdateTheme(Theme theme);
    Task<Theme> DeleteTheme(string themeId);
}

public class ThemeRepository : IThemeRepository
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

    //GET THEME
    public async Task<Theme> GetThemeById(string themeId)
    {
        return await _context.ThemeCollection.Find<Theme>(t => t.ThemeId == themeId).FirstOrDefaultAsync();
    }

    //ADD THEME
    public async Task<Theme> AddTheme(Theme newTheme)
    {
        await _context.ThemeCollection.InsertOneAsync(newTheme);
        return newTheme;
    }

    //UPDATE THEME
    public async Task<Theme> UpdateTheme(Theme theme)
    {
        try
        {
            var filter = Builders<Theme>.Filter.Eq("ThemeId", theme.ThemeId);
            var update = Builders<Theme>.Update.Set("Name", theme.Name);
            var result = await _context.ThemeCollection.UpdateOneAsync(filter, update);
            return await GetThemeById(theme.ThemeId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE THEME
    public async Task<Theme> DeleteTheme(string themeId)
    {
        try
        {
            var filter = Builders<Theme>.Filter.Eq("ThemeId", themeId);
            var result = await _context.ThemeCollection.DeleteOneAsync(filter);
            return await GetThemeById(themeId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}