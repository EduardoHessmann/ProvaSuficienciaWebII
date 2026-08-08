using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Consultas;

/// <summary>
/// Representa a consulta de um tipo de equipamento por ID.
/// Retorna nulo quando o tipo não existe.
/// </summary>
public record ConsultarTipoEquipamentoPorId(int Id) : IRequest<TipoEquipamentoDto?>;
