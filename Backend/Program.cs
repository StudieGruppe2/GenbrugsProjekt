using Backend.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dependency Injection: .NET får besked på, hvilket repository
// den skal give til controllerne, når de beder om et interface.
//builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepositoryMemory>();
//builder.Services.AddSingleton<IAnnonceRepository, AnnonceRepositoryMemory>();
//builder.Services.AddSingleton<IMemberRepository, MemberRepositoryMemory>();

// Skift fra Memory til MongoDB, når databasen skal bruges
builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepositoryMongoDB>();
builder.Services.AddSingleton<IAnnonceRepository, AnnonceRepositoryMongoDB>();
builder.Services.AddSingleton<IMemberRepository, MemberRepositoryMongoDB>();

builder.Services.AddOpenApi();

// Giver frontend lov til at kalde backend
builder.Services.AddCors(OpsaetCors);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Skal stå før MapControllers
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void OpsaetCors(CorsOptions options)
{
    CorsPolicy policy = new CorsPolicyBuilder()
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();

    options.AddDefaultPolicy(policy);
}