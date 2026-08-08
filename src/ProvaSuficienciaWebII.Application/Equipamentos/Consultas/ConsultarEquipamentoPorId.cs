using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Consultas;

/// <summary>
/// Representa a consulta de um equipamento por ID.
/// Retorna nulo quando o equipamento não existe.
/// </summary>
public record ConsultarEquipamentoPorId(int Id) : IRequest<EquipamentoDto?>;
