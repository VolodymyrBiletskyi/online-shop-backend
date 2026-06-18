using api.Data;
using api.Modules.AuthModule;
using api.Modules.CartModule;
using api.Modules.CategoryModule;
using api.Modules.OrderModule;
using api.Modules.ProductModule;
using api.Modules.UserModule;
using api.Modules.AuthModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
var dsBuilder = new Npgsql.NpgsqlDataSourceBuilder(cs);
dsBuilder.EnableDynamicJson();
var dataSource = dsBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dataSource));

builder.Services.AddMemoryCache();

builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddUserModule();
builder.Services.AddProductModule();
builder.Services.AddCategoryModule();
builder.Services.AddCartModule();
builder.Services.AddOrderModule();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
            "Example: \"Bearer 12345qwerty\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<RoleMiddleware>();
app.UseAuthorization();

app.MapOpenApi();
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    foreach (var url in app.Urls)
    {
        var displayUrl = url.Replace("[::]", "localhost").Replace("+", "localhost");
        app.Logger.LogInformation("App running at: {Url}", displayUrl);
        app.Logger.LogInformation("Swagger available at: {SwaggerUrl}", $"{displayUrl}/swagger");
    }
});

app.Run();
