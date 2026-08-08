using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Consultas;

/// <summary>
/// Representa a consulta de todos os equipamentos cadastrados.
/// </summary>
public record ConsultarEquipamentos : IRequest<RespostaListaEquipamentos>;
