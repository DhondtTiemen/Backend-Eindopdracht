var builder = WebApplication.CreateBuilder(args);

//Inlezen database config info
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IThemeRepository, ThemeRepository>();
builder.Services.AddTransient<ILegoService, LegoService>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Set>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Theme>());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();

var app = builder.Build();
app.MapGraphQL();

// app.MapGet("/", () => "Hello World!");

//SETUP
app.MapGet("/setup", (ILegoService legoService) => legoService.SetupDummyData());

//SETS
app.MapGet("/sets", (ILegoService LegoService) => LegoService.GetAllSets());

app.MapGet("/sets/{setNumber}", async (ILegoService legoService, int setNumber) =>
{
    var result = await legoService.GetSet(setNumber);
    return Results.Ok(result);
});

app.MapPost("/sets", async (IValidator<Set> validator, ILegoService legoService, Set set) =>
{
    var result = validator.Validate(set);
    if (result.IsValid)
    {
        await legoService.AddSet(set);
        return Results.Created("", set);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

//THEMES
app.MapGet("/themes", (ILegoService LegoService) => LegoService.GetAllThemes());

app.MapPost("/themes", async (IValidator<Theme> validator, ILegoService legoService, Theme theme) =>
{
    var result = validator.Validate(theme);
    if (result.IsValid)
    {
        await legoService.AddTheme(theme);
        return Results.Created("", theme);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});


app.Run("http://localhost:3000");
