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
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), "/", h =>
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProfessoresDbContext>();
    db.Database.Migrate();
}

app.MapOpenApi();

app.MapScalarApiReference(o =>
{
    o.WithTheme(ScalarTheme.Moon);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
