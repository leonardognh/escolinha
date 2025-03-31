namespace IntegracaoMicroservicos.Contracts.Events.Alunos;

public record AlunoTransferidoEvent(Guid AlunoId, Guid TurmaAnteriorId, Guid TurmaNovaId);
