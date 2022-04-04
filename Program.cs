var builder = WebApplication.CreateBuilder(args);

//Inlezen database config info
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IThemeRepository, ThemeRepository>();
builder.Services.AddTransient<ILegoService, LegoService>();

var app = builder.Build();

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

app.MapPost("/sets", async (ILegoService legoService, Set set) =>
{
    var result = await legoService.AddSet(set);
    return Results.Created("", result);
});

//THEMES
app.MapGet("/themes", (ILegoService LegoService) => LegoService.GetAllThemes());

app.MapPost("/themes", async (ILegoService legoService, Theme theme) =>
{
    var result = await legoService.AddTheme(theme);
    return Results.Created("", result);
});


app.Run("http://localhost:3000");
