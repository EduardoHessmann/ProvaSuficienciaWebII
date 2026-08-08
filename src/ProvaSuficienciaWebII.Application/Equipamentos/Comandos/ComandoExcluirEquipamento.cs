using MediatR;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Comando para excluir um equipamento.
/// Retorna falso quando o equipamento não existe.
/// </summary>
public record ComandoExcluirEquipamento(int Id) : IRequest<bool>;
