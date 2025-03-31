namespace IntegracaoMicroservicos.Contracts.Events.Alunos;

public record AlunoCriadoEvent(Guid Id, string Nome, Guid TurmaId);