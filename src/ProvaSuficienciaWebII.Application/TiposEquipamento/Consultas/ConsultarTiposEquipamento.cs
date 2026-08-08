using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Consultas;

/// <summary>
/// Representa a consulta de todos os tipos de equipamento cadastrados.
/// </summary>
public record ConsultarTiposEquipamento : IRequest<List<TipoEquipamentoDto>>;
