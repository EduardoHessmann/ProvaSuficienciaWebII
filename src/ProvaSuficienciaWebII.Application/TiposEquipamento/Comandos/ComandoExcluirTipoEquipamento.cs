using MediatR;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Comando para excluir um tipo de equipamento. Retorna falso quando o tipo
/// não existe e lança <see cref="Comum.Excecoes.ExcecaoTipoEquipamentoEmUso"/>
/// quando há equipamentos associados a ele.
/// </summary>
public record ComandoExcluirTipoEquipamento(int Id) : IRequest<bool>;
