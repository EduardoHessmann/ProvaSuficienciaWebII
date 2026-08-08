using MediatR;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Dados para cadastrar um novo equipamento.
/// </summary>
public record ComandoNovoEquipamento(RequisicaoNovoEquipamento Dados) : IRequest<EquipamentoDto>;
