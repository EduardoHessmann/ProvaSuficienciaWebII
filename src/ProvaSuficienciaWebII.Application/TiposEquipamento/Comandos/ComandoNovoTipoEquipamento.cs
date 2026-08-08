using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Dados para cadastrar um novo tipo de equipamento.
/// </summary>
public record ComandoNovoTipoEquipamento(RequisicaoNovoTipoEquipamento Dados) : IRequest<TipoEquipamentoDto>;
