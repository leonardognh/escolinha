﻿using Grade.Domain.Entities;
using Grade.Infrastructure.Persistence;
using IntegracaoMicroservicos.Contracts.Events.Professores;
using MassTransit;

namespace Grade.API.Consumers.Professores;

public class ProfessorCriadoConsumer : IConsumer<ProfessorCriadoEvent>
{
    private readonly GradeDbContext _db;

    public ProfessorCriadoConsumer(GradeDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<ProfessorCriadoEvent> context)
    {
        var e = context.Message;

        var exists = await _db.ProfessoresProjecao.FindAsync(e.Id);
        if (exists is not null) return;

        var proj = new ProfessorProjecao
        {
            Id = e.Id,
            Nome = e.Nome,
            Email = e.Email
        };

        _db.ProfessoresProjecao.Add(proj);
        await _db.SaveChangesAsync();
    }
}
