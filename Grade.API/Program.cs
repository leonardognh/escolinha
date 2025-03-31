using Grade.API.Consumers;
using Grade.Application.Commands;
using Grade.Domain.Interfaces;
using Grade.Infrastructure.Persistence;
using Grade.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL + EF Core
builder.Services.AddDbContext<GradeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProfessorCriadoConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("grade-professor-criado", e =>
        {
            e.ConfigureConsumer<ProfessorCriadoConsumer>(ctx);
        });
    });
});

// MediatR
builder.Services.AddMediatR(typeof(CreateGradeHorarioCommand));

// Repositórios
builder.Services.AddScoped<IGradeHorarioRepository, GradeHorarioRepository>();

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
