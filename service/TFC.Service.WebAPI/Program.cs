using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TFC.Infraestructure.Persistence.Context;
using TFC.Infrastructure.Persistence.Dependencies;
using TFC.Transversal.Logs;

var builder = WebApplication.CreateBuilder(args);

Log.SetLogFileName(builder.Configuration["Options:LogFile"]);

// Configuración mínima
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración CORS (¡Nuevo!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
        .WithOrigins(
            "http://192.168.18.144:8000",
            "http://localhost:8000",
            "http://192.168.1.25",
            "http://192.168.1.25:80",  // Add this
            "http://192.168.1.25:5122",
            "http://localhost:5122"    // Add this for local testing
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Configuración de Mails
MailUtils.Initialize(builder.Configuration);

// Configuración de PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// Registrar servicios de infraestructura
builder.Services.AddInfrastructureServices();

// Configuración JWT
var jwtSettings = builder.Configuration.GetSection("JWT");
var keyString = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key no está configurado");
Console.WriteLine($"JWT Key: {keyString}"); // Debug
var key = Encoding.ASCII.GetBytes(keyString);
TFC.Service.WebApi.JwtUtils.Initialize(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS (¡Importante que esté antes de UseAuthentication/UseAuthorization!)
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
