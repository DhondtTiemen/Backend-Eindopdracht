namespace Eindopdracht.Services;

public interface ILegoService
{
    Task<Set> AddSet(Set newSet);
    Task<Theme> AddTheme(Theme newTheme);
    Task<List<Set>> GetAllSets();
    Task<List<Theme>> GetAllThemes();
    Task<Set> GetSet(int number);
    Task SetupDummyData();
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
    public async Task<Set> GetSet(int number) => await _setRepository.GetSet(number);
    public async Task<Set> AddSet(Set newSet) => await _setRepository.AddSet(newSet);

    //THEME
    public async Task<List<Theme>> GetAllThemes() => await _themeRepository.GetAllThemes();
    public async Task<Theme> AddTheme(Theme newTheme) => await _themeRepository.AddTheme(newTheme);

    //ADD DUMMY DATA
    public async Task SetupDummyData()
    {
        if (!(await _themeRepository.GetAllThemes()).Any())
        {
            var themes = new List<Theme>()
            {
                new Theme() {
                    Name = "Architecture"
                },
                new Theme() {
                    Name = "Batman"
                },
                new Theme() {
                    Name = "Boost"
                },
                new Theme() {
                    Name = "BrickHeadz"
                },
                new Theme() {
                    Name = "Brick Sketches"
                },
                new Theme() {
                    Name = "City"
                },
            };

            foreach (var theme in themes)
                await _themeRepository.AddTheme(theme);
        }

        if (!(await _setRepository.GetAllSets()).Any())
        {
            var themes = await _themeRepository.GetAllThemes();
            var sets = new List<Set>()
            {
                new Set(){
                    SetNumber = 21056,
                    Name = "Taj Mahal",
                    MinimalAge = 18,
                    Pieces = 2022,
                    Price = 119.99,
                    Theme = themes[0]
                },
                new Set(){
                    SetNumber = 21042,
                    Name = "Statue of Liberty",
                    MinimalAge = 16,
                    Pieces = 1685,
                    Price = 99.99,
                    Theme = themes[0]
                },
            };

            foreach (var set in sets)
                await _setRepository.AddSet(set);
        }
    }
}