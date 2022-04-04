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

app.MapGet("/setup", (ILegoService legoService) => legoService.SetupDummyData());

app.Run("http://localhost:3000");
