namespace Eindopdracht.GraphQL.Mutations;

public class Mutation
{
    public async Task<AddSetPayload> AddSet([Service] ILegoService legoService, AddSetInput input)
    {
        var newSet = new Set()
        {
            SetNumber = input.number,
            Name = input.name,
            MinimalAge = input.minimalAge,
            Pieces = input.pieces,
            Price = input.price,
            Theme = input.Theme,
        };

        var created = await legoService.AddSet(newSet);
        return new AddSetPayload(created);
    }

    public async Task<AddThemePayload> AddTheme([Service] ILegoService legoService, AddThemeInput input)
    {
        var newTheme = new Theme()
        {
            Name = input.name
        };

        var created = await legoService.AddTheme(newTheme);
        return new AddThemePayload(created);
    }
}