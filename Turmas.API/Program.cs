using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Turmas.Application.Commands;
using Turmas.Domain.Interfaces;
using Turmas.Infrastructure.Persistence;
using Turmas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// EF Core com PostgreSQL
builder.Services.AddDbContext<TurmasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(typeof(CreateTurmaCommand));

// Repository
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
