using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Professores.Application.Commands;
using Professores.Domain.Interfaces;
using Professores.Infrastructure.Persistence;
using Professores.Infrastructure.Repositories;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Adiciona EF Core + PostgreSQL
builder.Services.AddDbContext<ProfessoresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

// Adiciona MediatR e Handlers
builder.Services.AddMediatR(typeof(CreateProfessorCommand));

// Repositórios
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

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
