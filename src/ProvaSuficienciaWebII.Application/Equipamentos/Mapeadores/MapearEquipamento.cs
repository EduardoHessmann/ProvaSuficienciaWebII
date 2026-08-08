using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Mapeadores;

/// <summary>
/// Conversões da entidade <see cref="Equipamento"/> para os DTOs da API.
/// </summary>
public static class MapearEquipamento
{
    public static EquipamentoDto ParaDto(Equipamento equipamento)
    {
        return new EquipamentoDto
        {
            Id = equipamento.Id,
            Nome = equipamento.Nome,
            Tipo = new TipoEquipamentoDto
            {
                Id = equipamento.Tipo.Id,
                Nome = equipamento.Tipo.Nome
            }
        };
    }
}
