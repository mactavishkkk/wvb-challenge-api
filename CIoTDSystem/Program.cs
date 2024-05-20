global using CIoTDSystem.Models;
using CIoTDSystem.Data;
using CIoTDSystem.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CIoTDSystem.Services.Seedings;
using CIoTDSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add other services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<UserSeedingService>();
builder.Services.AddScoped<DeviceSeedingService>();
builder.Services.AddScoped<CommandSeedingService>();

var app = builder.Build();

// Seeding to database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userSeedingService = services.GetRequiredService<UserSeedingService>();
        var deviceSeedingService = services.GetRequiredService<DeviceSeedingService>();
        var commandSeedingService = services.GetRequiredService<CommandSeedingService>();

        var context = services.GetRequiredService<DataContext>();

        context.Database.Migrate();

        userSeedingService.Seed();
        deviceSeedingService.Seed();
        commandSeedingService.Seed();
    } catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Welcome
app.MapGet("/", () =>
{
    return "Bem-vindo ao CIoTDSystem api!";
})
.WithName("GetWelcomeRoute").WithOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
