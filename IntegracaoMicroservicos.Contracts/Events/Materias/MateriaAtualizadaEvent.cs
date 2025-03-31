namespace IntegracaoMicroservicos.Contracts.Events.Materias;
public record MateriaAtualizadaEvent(Guid Id, string Nome, string? Descricao, int? CargaHoraria);