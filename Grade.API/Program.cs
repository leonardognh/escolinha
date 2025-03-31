using Grade.API.Consumers.Alunos;
using Grade.API.Consumers.Materias;
using Grade.API.Consumers.Professores;
using Grade.API.Consumers.Turmas;
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
    x.AddConsumer<ProfessorAtualizadoConsumer>();

    x.AddConsumer<AlunoCriadoConsumer>();
    x.AddConsumer<AlunoTransferidoConsumer>();

    x.AddConsumer<TurmaCriadaConsumer>();
    x.AddConsumer<TurmaAtualizadaConsumer>();

    x.AddConsumer<MateriaCriadaConsumer>();
    x.AddConsumer<MateriaAtualizadaConsumer>();
    x.AddConsumer<MateriaRemovidaConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("grade-professores", e =>
        {
            e.ConfigureConsumer<ProfessorCriadoConsumer>(ctx);
            e.ConfigureConsumer<ProfessorAtualizadoConsumer>(ctx);
        });

        cfg.ReceiveEndpoint("grade-alunos", e =>
        {
            e.ConfigureConsumer<AlunoCriadoConsumer>(ctx);
            e.ConfigureConsumer<AlunoTransferidoConsumer>(ctx);
        });
        cfg.ReceiveEndpoint("grade-turmas", e =>
        {
            e.ConfigureConsumer<TurmaCriadaConsumer>(ctx);
            e.ConfigureConsumer<TurmaAtualizadaConsumer>(ctx);
        });
        cfg.ReceiveEndpoint("grade-materias", e =>
        {
            e.ConfigureConsumer<MateriaCriadaConsumer>(ctx);
            e.ConfigureConsumer<MateriaAtualizadaConsumer>(ctx);
            e.ConfigureConsumer<MateriaRemovidaConsumer>(ctx);
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
