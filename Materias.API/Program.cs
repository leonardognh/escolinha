using MassTransit;
using Materias.Application.Commands;
using Materias.Domain.Interfaces;
using Materias.Infrastructure.Persistence;
using Materias.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// DbContext
builder.Services.AddDbContext<MateriasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ") ?? ""), "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

// MediatR
builder.Services.AddMediatR(typeof(CreateMateriaCommand));

// Repository
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MateriasDbContext>();
    db.Database.Migrate();
}

app.MapOpenApi();

app.MapScalarApiReference(o =>
{
    o.WithTheme(ScalarTheme.Moon);
});

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
