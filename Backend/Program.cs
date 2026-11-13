using Backend.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Gør det muligt at bruge Swagger til at teste API'et
builder.Services.AddSwaggerGen();

// Dependency Injection: .NET får besked på, hvilket repository
// den skal give til controllerne, når de beder om et interface

// Memory-repositories (bruges ikke lige nu)
//builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepositoryMemory>();
//builder.Services.AddSingleton<IAnnonceRepository, AnnonceRepositoryMemory>();
//builder.Services.AddSingleton<IMemberRepository, MemberRepositoryMemory>();

// MongoDB-repositories (bruges lige nu)
builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepositoryMongoDB>();
builder.Services.AddSingleton<IAnnonceRepository, AnnonceRepositoryMongoDB>();
builder.Services.AddSingleton<IMemberRepository, MemberRepositoryMongoDB>();

// Giver frontend lov til at kalde backend
builder.Services.AddCors(OpsaetCors);

var app = builder.Build();

// Swagger aktiveres, når backend kører i Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Skal stå før MapControllers
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


// ---------- CORS ----------

// Tillader frontend at sende HTTP-requests til backend
static void OpsaetCors(CorsOptions options)
{
    CorsPolicy policy = new CorsPolicyBuilder()
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();

    options.AddDefaultPolicy(policy);
}