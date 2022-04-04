namespace Eindopdracht.GraphQL.Queries;

public class Queries
{
    public async Task<List<Set>> GetSets([Service] ILegoService legoService) => await legoService.GetAllSets();
    public async Task<List<Theme>> GetThemes([Service] ILegoService legoService) => await legoService.GetAllThemes();
}