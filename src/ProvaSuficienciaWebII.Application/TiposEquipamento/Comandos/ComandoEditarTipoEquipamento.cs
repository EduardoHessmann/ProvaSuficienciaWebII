using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Dados para edição parcial de um tipo de equipamento: somente os campos
/// enviados são alterados. Retorna nulo quando o tipo não existe.
/// </summary>
public record ComandoEditarTipoEquipamento(int Id, RequisicaoEditarTipoEquipamento Dados) : IRequest<TipoEquipamentoDto?>;
