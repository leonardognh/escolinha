using Alunos.API.Consumers.Turmas;
using Alunos.Application.Commands;
using Alunos.Domain.Interfaces;
using Alunos.Infrastructure.Persistence;
using Alunos.Infrastructure.Repositories;
using MassTransit;
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

// PostgreSQL + EF Core
builder.Services.AddDbContext<AlunosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TurmaCriadaConsumer>();
    x.AddConsumer<TurmaAtualizadaConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"), "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("alunos-turmas", e =>
        {
            e.ConfigureConsumer<TurmaCriadaConsumer>(ctx);
            e.ConfigureConsumer<TurmaAtualizadaConsumer>(ctx);
        });
    });
});


// MediatR
builder.Services.AddMediatR(typeof(CreateAlunoCommand));

// Repositórios
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AlunosDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.WithTheme(ScalarTheme.Moon);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
