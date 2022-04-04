namespace Eindopdracht.Services;

public interface ILegoService
{
    Task<Set> AddSet(Set newSet);
    Task<Theme> AddTheme(Theme newTheme);
    Task<List<Set>> GetAllSets();
    Task<List<Theme>> GetAllThemes();
    Task<Set> GetSet(string id);
}

public class LegoService : ILegoService
{
    public readonly ISetRepository _setRepository;
    public readonly IThemeRepository _themeRepository;

    public LegoService(ISetRepository setRepository, IThemeRepository themeRepository)
    {
        _setRepository = setRepository;
        _themeRepository = themeRepository;
    }

    //SET
    public async Task<List<Set>> GetAllSets() => await _setRepository.GetAllSets();
    public async Task<Set> GetSet(string id) => await _setRepository.GetSet(id);
    public async Task<Set> AddSet(Set newSet) => await _setRepository.AddSet(newSet);

    //THEME
    public async Task<List<Theme>> GetAllThemes() => await _themeRepository.GetAllThemes();
    public async Task<Theme> AddTheme(Theme newTheme) => await _themeRepository.AddTheme(newTheme);
}