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
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Set>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Theme>());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:3000",
        ValidAudience = "lego_api_users",
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("PdSgVkYp3s6v9y$B&E)H@McQfThWmZq4"))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lego API", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI();
app.MapGraphQL();
// app.UseAuthentication();
// app.UseAuthorization();

//AUTHENTICATION
#region AUTHENTICATION ENDPOINT
app.MapPost("/authenticate", async (IAuthenticationService authenticationService, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));

    var jwtSecurityToken = new JwtSecurityToken(
        "https://localhost:3000",
        "lego_api_users",
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        signingCredentials
    );
    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});
#endregion

//SETUP
#region SETUP ENDPOINTS
app.MapGet("/api/setup", (ILegoService legoService) => legoService.SetupDummyData());
#endregion

//SETS
#region SET ENDPOINTS
app.MapGet("/api/sets", async (ILegoService legoService) =>
{
    var results = await legoService.GetAllSets();
    return Results.Ok(results);
});

app.MapGet("/api/sets/{setNumber}", async (ILegoService legoService, int setNumber, bool? includeTheme) =>
{
    var result = await legoService.GetSetByNumber(setNumber);
    if (includeTheme == null || includeTheme == false)
    {
        result.Theme = null;
    }
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

app.MapPost("/api/sets", async (IValidator<Set> validator, ILegoService legoService, Set set) =>
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
#endregion

//THEMES
#region THEME ENDPOINTS
app.MapGet("/api/themes", async (ILegoService LegoService) =>
{
    var results = await LegoService.GetAllThemes();
    return Results.Ok(results);
});

app.MapGet("/api/themes/{themeId}", async (ILegoService legoService, string themeId) =>
{
    var result = await legoService.GetThemeById(themeId);
    return Results.Ok(result);
});

app.MapPost("/api/themes", async (IValidator<Theme> validator, ILegoService legoService, Theme theme) =>
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
#endregion

//CUSTOMERS
#region CUSTOMER ENDPOINTS
app.MapGet("/api/customers", async (ILegoService legoService) =>
{
    var results = await legoService.GetAllCustomers();
    return Results.Ok(results);
});

app.MapGet("/api/customers/id/{customerId}", async (ILegoService legoService, string customerId) =>
{
    var result = await legoService.GetCustomerById(customerId);
    return Results.Ok(result);
});

app.MapGet("/api/customers/email/{email}", async (ILegoService legoService, string email) =>
{
    var result = await legoService.GetCustomerByMail(email);
    return Results.Ok(result);
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

app.MapDelete("/api/customers/{customerId}", async (ILegoService legoService, string customerId) =>
{
    await legoService.DeleteCustomer(customerId);
    return Results.Ok("Deleted");
});
#endregion

//ORDERS
#region ORDER ENDPOINTS
app.MapGet("/api/orders", async (ILegoService legoService) =>
{
    var results = await legoService.GetAllOrders();
    return Results.Ok(results);
});

app.MapGet("/api/orders/id/{orderId}", async (ILegoService legoService, string orderId) =>
{
    var result = await legoService.GetOrderById(orderId);
    return Results.Ok(result);
});

app.MapGet("/api/orders/customerId/{customerId}", async (ILegoService legoService, string customerId) =>
{
    var result = await legoService.GetOrderByCustomerId(customerId);
    return Results.Ok(result);
});

app.MapGet("/api/orders/customerEmail/{customerEmail}", async (ILegoService legoService, string customerEmail) =>
{
    var result = await legoService.GetOrderByCustomerEmail(customerEmail);
    return Results.Ok(result);
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
#endregion


// app.Run("http://localhost:3000");

//Testing
app.Run();

//Mongo
// app.Run("http://0.0.0.0:3000");
public partial class Program { }