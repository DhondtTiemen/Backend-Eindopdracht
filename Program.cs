var builder = WebApplication.CreateBuilder(args);

//Inlezen database config info
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IThemeRepository, ThemeRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
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
app.MapGet("/sets", (ILegoService legoService) => legoService.GetAllSets());

app.MapGet("/sets/{setNumber}", async (ILegoService legoService, int setNumber) =>
{
    var result = await legoService.GetSet(setNumber);
    return Results.Ok(result);
});

app.MapGet("/api/sets/theme/{theme}", async (ILegoService legoService, string theme) =>
{
    var result = await legoService.GetSetsByTheme(theme);
    return Results.Ok(result);
});

app.MapGet("/api/sets/age/{age}", async (ILegoService legoService, int age) =>
{
    var result = await legoService.GetSetsByAge(age);
    return Results.Ok(result);
});

app.MapGet("/api/sets/price/{price}", async (ILegoService legoService, double price) =>
{
    var result = await legoService.GetSetsByPrice(price);
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

app.MapPut("/api/sets", async (IValidator<Set> validator, ILegoService legoService, Set set) =>
{
    var result = validator.Validate(set);
    if (result.IsValid)
    {
        await legoService.UpdateSet(set);
        return Results.Created("", set);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapDelete("/api/sets/{setNumber}", async (ILegoService legoService, int setNumber) =>
{
    await legoService.DeleteSet(setNumber);
    return Results.Ok("Deleted");
});

//THEMES
app.MapGet("/themes", async (ILegoService LegoService) => await LegoService.GetAllThemes());

app.MapGet("/api/themes/{themeId}", async (ILegoService legoService, string themeId) =>
{
    var result = await legoService.GetTheme(themeId);
    return Results.Ok(result);
});

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

app.MapPut("/api/themes", async (IValidator<Theme> validator, ILegoService legoService, Theme theme) =>
{
    var result = validator.Validate(theme);
    if (result.IsValid)
    {
        await legoService.UpdateTheme(theme);
        return Results.Created("", theme);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapDelete("/api/themes/{themeId}", async (ILegoService legoService, string themeId) =>
{
    await legoService.DeleteTheme(themeId);
    return Results.Ok("Deleted");
});

//CUSTOMERS
app.MapGet("/api/customers", async (ILegoService legoService) =>
{
    return await legoService.GetAllCustomers();
});

app.MapGet("/api/customers/id/{customerId}", async (ILegoService legoService, string customerId) =>
{
    return await legoService.GetCustomerById(customerId);
});

app.MapGet("/api/customers/email/{email}", async (ILegoService legoService, string email) =>
{
    return await legoService.GetCustomerByMail(email);
});

app.MapPost("/api/customers", async (IValidator<Customer> validator, ILegoService legoService, Customer customer) =>
{
    var result = validator.Validate(customer);
    if (result.IsValid)
    {
        await legoService.AddCustomer(customer);
        return Results.Created("", customer);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapPut("/api/customers", async (IValidator<Customer> validator, ILegoService legoService, Customer customer) =>
{
    var result = validator.Validate(customer);
    if (result.IsValid)
    {
        await legoService.UpdateCustomer(customer);
        return Results.Created("", customer);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapDelete("/api/customers", async (ILegoService legoService, string customerId) =>
{
    await legoService.DeleteCustomer(customerId);
    return Results.Ok("Deleted");
});


//ORDERS
app.MapGet("/api/orders", async (ILegoService legoService) =>
{
    return await legoService.GetAllOrders();
});

app.MapGet("/api/orders/id/{orderId}", async (ILegoService legoService, string orderId) =>
{
    return await legoService.GetOrderById(orderId);
});

app.MapGet("/api/orders/customerId/{customerId}", async (ILegoService legoService, string customerId) =>
{
    return await legoService.GetOrderByCustomerId(customerId);
});

app.MapGet("/api/orders/customerEmail/{customerEmail}", async (ILegoService legoService, string customerEmail) =>
{
    return await legoService.GetCustomerByMail(customerEmail);
});

app.MapPost("/api/orders", async (IValidator<Order> validator, ILegoService legoService, Order order) =>
{
    var result = validator.Validate(order);
    if (result.IsValid)
    {
        await legoService.AddOrder(order);
        return Results.Created("", order);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapPut("/api/orders", async (IValidator<Order> validator, ILegoService legoService, Order order) =>
{
    var result = validator.Validate(order);
    if (result.IsValid)
    {
        await legoService.UpdateOrder(order);
        return Results.Created("", order);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapDelete("/api/orders/{orderId}", async (ILegoService legoService, string orderId) =>
{
    await legoService.DeleteOrder(orderId);
    return Results.Ok("Deleted");
});


app.Run("http://localhost:3000");

//Testing
// app.Run();
public partial class Program { }