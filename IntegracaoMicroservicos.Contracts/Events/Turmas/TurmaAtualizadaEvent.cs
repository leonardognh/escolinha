﻿namespace IntegracaoMicroservicos.Contracts.Events.Turmas;
public record TurmaAtualizadaEvent(Guid Id, string Nome, int Ano, string? Turno);
