using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepositoryMemory>();
builder.Services.AddSingleton<IAnnonceRepository, AnnonceRepositoryMemory>();
builder.Services.AddSingleton<IMemberRepository, MemberRepositoryMemory>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
