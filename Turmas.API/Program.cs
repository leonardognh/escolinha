using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Turmas.Application.Commands;
using Turmas.Domain.Interfaces;
using Turmas.Infrastructure.Persistence;
using Turmas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

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

builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TurmasDbContext>();
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
