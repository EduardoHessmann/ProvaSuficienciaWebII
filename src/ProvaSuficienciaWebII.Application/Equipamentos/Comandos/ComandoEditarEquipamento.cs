using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Dados para edição parcial de um equipamento: somente os campos enviados
/// são alterados. Retorna nulo quando o equipamento não existe.
/// </summary>
public record ComandoEditarEquipamento(int Id, RequisicaoEditarEquipamento Dados) : IRequest<EquipamentoDto?>;
