var builder = WebApplication.CreateBuilder(args);

// Configuración mínima
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro directo del contexto MongoDB (sin MongoDBSettings)
builder.Services.AddSingleton<ApplicationDbContext>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new ApplicationDbContext(config);
});

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();